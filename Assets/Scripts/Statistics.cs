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

    public float momvementSpeedMod = 1f;

    public int strength; //Weapon damage
    public int agility; //Speed, maybe bow damage?
    public int intellect; //Magic spells etc;

    public int actualstrength; //Weapon damage
    public int actualAgility; //Speed, maybe bow damage?
    public int actualIntellect; //Magic spells etc;

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
        text += actualstrength;
        text += "\nAgility: ";
        text += actualAgility;
        text += "\nIntellect: ";
        text += actualIntellect;
        text += "\nExperience to next level: ";
        text += expForLevel;
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
            a.speed = momvementSpeedMod;

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
                    //GameObject player = Resources.Load("Prefabs/Player/Player") as GameObject;
                    Destroy(gameObject);
                    //Instantiate(player);
                    SceneManager.LoadScene("death");
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

    public void levelUp()
    {
        //More stat stuff to do here, gotta get it written down.
        strength++;
        agility++;
        intellect++;
        attack += 5;
        maxHealth += 10;
        curHealth = maxHealth;
        maxMana += 5;
        curMana = maxMana;
        level++;
        expForLevel = level * 100 * (int)(level * 0.25);
        //refreshWeaponDamage();
        PlayerControl p = GetComponent<PlayerControl>();
        p.refreshStats();
        setExpForLevel();
    }

    void refreshWeaponDamage()
    {
        int atk = (int)baseAttack;
        foreach(GameObject g in inv.equipped)
        {
            Equipment w = GetComponent<Equipment>();
            atk += w.attack;
        }

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
        /*if(player)
        {
            GUI.Box( new Rect(Screen.height / 12, Screen.height - (Screen.height / 12), 200, 50), "hi");
        }
        */
    }
}
