using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class Statistics : NetworkBehaviour{

    public bool damageable = false;
    public string nameOfMob;
    [SyncVar] public int curHealth;
    public int maxHealth;
    public int curMana;
    public int maxMana;
    public string status = "alive";

    public int armour; //Not implemented yet
    public float attack;
    public float baseAttack = 0;
    public float meleeReach;
    public float attackTime = 1f;
    public float invulvnerableTime = 0.75f;
    public float invulvnerableTimeTimer = 0f;

    public float magicAttack;
    public float magicSpeed;
    public GameObject magicSpell;
    public float magicTime = 1f;

    public float movementSpeedMod = 1f;

    public int strength; //Weapon damage
    public int agility; //Speed, maybe bow damage?
    public int intellect; //Magic spells etc;

    public int equippedStrength; //Weapon damage
    public int equippedAgility; //Speed, maybe bow damage?
    public int equippedIntellect; //Magic spells etc;

    public Inventory inv;

    public int level; //Exp for level = level * 100 * (level * 0.25)
    public int exp;
    public float expForLevel;
    

    //Get component
    private Rigidbody r;
    private CharacterController c;
    private Animator a;
    public Text healthUI;
    public Text manaUI;
    private float playerKnockback;
    private Vector3 playerKnockbackV;

    //Audio
    public AudioSource audioPlayer;
    public AudioClip hurtSound;
    public AudioClip music;
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    private float musicLength = 1f;
    private float musicLengthTemp = 0f;

    public bool play = false;

    

    // Use this for initialization
    void Start() {
        r = GetComponent<Rigidbody>();
        c = GetComponent<CharacterController>();
        a = GetComponent<Animator>();
        setExpForLevel();

        if (gameObject.tag == "Player")
        {
            resetMusic();
        }
    }

    public void setMusic(int x)
    {
        if(x == 1)
        {
            music = music1;
        }
        else if(x == 2)
        {
            music = music2;
        }
        else if (x == 3)
        {
            music = music3;
        }
        else if (x == 4)
        {
            music = music4;
        }
        resetMusic();
    }

    public void resetMusic()
    {
        play = false;
        musicLength = music.length;
        musicLengthTemp = musicLength;
    }

    public string updateStatPage()
    {
        string text;
        text = "\nLevel: ";
        text += level;
        text += "\nMax health: ";
        text += maxHealth;
        text += "\nMax mana: ";
        text += maxMana;
        text += "\nAttack damage: ";
        text += attack;
        text += "\nSpell damage: ";
        text += magicAttack;
        text += "\nStrength: ";
        text += equippedStrength + strength;
        text += "\nAgility: ";
        text += equippedAgility + agility;
        text += "\nIntellect: ";
        text += equippedIntellect + intellect;
        text += "\nExperience to next level: ";
        text += expForLevel - exp;
        return text;
    }

    void fixedUpdate()
    {
        if (playerKnockback <= 0)
        {
            c.Move(playerKnockbackV * Time.deltaTime);
            playerKnockback -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update() {

        if(play)
        {
            if(musicLengthTemp <= 0)
            {
                audioPlayer.PlayOneShot(music, 0.1f);
                musicLengthTemp = musicLength;
            }

            musicLengthTemp -= Time.deltaTime;
        }
        else
        {
                audioPlayer.Stop();
                audioPlayer.PlayOneShot(music, 0.1f);
                play = true;
        }

        if (a != null)
            a.speed = movementSpeedMod;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (curMana > maxMana)
            curMana = maxMana;

        if (curHealth <= 0)
        {

            if (gameObject.tag == "Player")
            {
                if (isLocalPlayer)
                {
                    PlayerControl p = GetComponent<PlayerControl>();
                    p.enabled = false;

                    status = "death";
                }
            }
        }

        if (gameObject.tag == "Player")
        {
            healthUI.text = "Health: " + curHealth;
            manaUI.text = "Mana: " + curMana;
        }

        if(invulvnerableTimeTimer > 0)
        {
            damageable = false;
            invulvnerableTimeTimer -= Time.deltaTime;
        }
        else
        {
            damageable = true;
        }
        

    }

    private void Die()
    {
        //GameObject player = Resources.Load("Prefabs/Player/Player") as GameObject;
        //Instantiate(player);
        PlayerControl l = GetComponent<PlayerControl>();

        l.enabled = true;
        string deathType = l.deathSetting;
        
        if (deathType == "hardcore")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("death");
        }
        else
        {
            curHealth = maxHealth;
            gameObject.transform.position = new Vector3(l.startX, l.startY, l.startZ);

            if (level > 1)
                levelUp(-1);
        }
    }

    public void takeDamage(float[] dam)
    {
        

        if (damageable)
        {
            curHealth -= (int)dam[1];
            Vector3 p = new Vector3(dam[2], dam[3], dam[4]);
            p = transform.position - p;
            p = p.normalized;
            p.y = p.y + 1;
            audioPlayer.PlayOneShot(hurtSound);
            if (transform.tag != "Player")
            {
                GameObject b = Resources.Load("Prefabs/Blood") as GameObject;
                r.AddForce(p * 125);
                
                for (int x = 0; x < Random.Range(1, 4); x++)
                {
                    GameObject g;
                    Vector3 v = gameObject.transform.position;
                    Quaternion q = gameObject.transform.rotation;
                    g = Instantiate(b, v, q) as GameObject;
                    Rigidbody brdi = g.GetComponent<Rigidbody>();
                    brdi.AddForce(p * 250);
                }
            }
            else
            {
                //c.SimpleMove(p * Time.deltaTime * 1000);
                invulvnerableTimeTimer = invulvnerableTime;
            }
        }
    }

    public void levelUp(int x)
    {
        //More stat stuff to do here, gotta get it written down.
        strength = strength + x;
        agility = agility + x;
        intellect = intellect + x;
        attack = attack + (5 * x);
        maxHealth = maxHealth + (5 * x);
        curHealth = maxHealth;
        maxMana = maxMana + (5 * x);
        curMana = maxMana;
        level = level + x;
        expForLevel = level * 100 * (int)(level * 0.25);
        //refreshWeaponDamage();
        PlayerControl p = GetComponent<PlayerControl>();
        p.refreshStats();
        setExpForLevel();
    }

    void refreshWeaponDamage()
    {
        Weapon weapon = inv.equipped[0].GetComponent<Weapon>();
        int atk = weapon.damage;
        attackTime = weapon.swingTimer;
        
        foreach(GameObject g in inv.equipped)
        {
            Equipment w = GetComponent<Equipment>();
            atk += w.attack;
        }

        atk += 5 * level;
        attack = atk;

        
        /*
        int x = magicSpell.GetComponent<Spell>().magicAttack;
        magicAttack = x + (int)(x * 0.5);*/
    }

    void setExpForLevel()
    {
        exp = 0;
        expForLevel = level * 100 * (level * 0.25f);
    }

    void OnGUI ()
    {
        int w = Screen.width / 12;
        int h = Screen.height / 12;

        if (status == "death")
        {
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h * 2), 200, 50), "Controls"))
            {
                Die();
                status = "alive";
            }
        }
    }
}
