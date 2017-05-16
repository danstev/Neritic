using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Statistics : MonoBehaviour {

    public bool damageable = false;
    public string nameOfMob;
    public int curHealth;
    public int maxHealth;
    public int curMana;
    public int maxMana;

    public int armour;
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
    public int expGranted;

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
    private float musicLength = 1f;
    private float musicLengthTemp = 0f;

    private bool play = false;

    //Enemy Drops
    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public GameObject rareDrop;
    public GameObject veryRareDrop;

    // Use this for initialization
    void Start() {
        r = GetComponent<Rigidbody>();
        c = GetComponent<CharacterController>();
        a = GetComponent<Animator>();
        setExpForLevel();

        if (gameObject.tag == "Player")
        {
            play = true;
            musicLength = music.length;
        }
    }

    public string updateStatPage()
    {
        string text;
        text = "\nMax health: ";
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
                audioPlayer.PlayOneShot(music);
                musicLengthTemp = musicLength;
            }

            musicLengthTemp -= Time.deltaTime;
        }

        if (a != null)
            a.speed = momvementSpeedMod;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (curMana > maxMana)
            curMana = maxMana;

        if (curHealth <= 0)
        {

            if( tag == "Player")
            {
                GameObject player = Resources.Load("Prefabs/Player/Player") as GameObject;
                Destroy(gameObject);
                Instantiate(player);
                SceneManager.LoadScene("forest");
            }

            Collider[] detectColliders = Physics.OverlapSphere(transform.position, 25);
            for (int i = 0; i < detectColliders.Length; i++)
            {
                if (detectColliders[i].tag == "Player")
                {
                    Statistics s = detectColliders[i].GetComponent<Statistics>();
                    s.exp += expGranted;
                    if (s.exp > s.expForLevel)
                    {
                        s.levelUp();
                    }
                }
            }

            if(tag =="Enemy")
            {
                float rate = Random.Range(0f, 1f);
                if (rate > 0.75f)
                {
                    int item = Random.Range(0, 4);
                    if (item == 1)
                    {
                        Instantiate(drop1, transform.position, Quaternion.identity);
                    }
                    else if (item == 2)
                    {
                        Instantiate(drop2, transform.position, Quaternion.identity);
                    }
                    else if (item == 3)
                    {
                        Instantiate(drop3, transform.position, Quaternion.identity);
                    }
                }
                else if (rate > 0.95f)
                {
                    Instantiate(rareDrop, transform.position, Quaternion.identity);
                }
                else if (rate > 0.98f)
                {
                    Instantiate(veryRareDrop, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
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
                c.SimpleMove(p * Time.deltaTime * 1000);
                invulvnerableTimeTimer = invulvnerableTime;
            }
        }
    }

    private void levelUp()
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
