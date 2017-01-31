using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Statistics : MonoBehaviour {

    public bool damageable = false;
    public string nameOfMob;
    public int curHealth;
    public int maxHealth;
    public int mana;

    public int armour;
    public int attack;
    public float meleeReach;
    public float attackTime = 1f;

    public int magicAttack;
    public float magicSpeed;
    public GameObject magicSpell;

    public float momvementSpeedMod = 1f;

    //MOnster stats
    public int strength; //Weapon damage
    public int agility; //Speed, maybe bow damage?
    public int intellect; //Magic spells etc;

    public Inventory inv;
    public GameObject[] equipment = new GameObject[10];

    public int level; //Exp for level = level * 100 * (level * 0.25)
    public int exp;
    public float expForLevel;
    public int expGranted;

    //Get component
    private Rigidbody r;
    private CharacterController c;
    private Animator a;
    public Text healthUI;
    private float playerKnockback;
    private Vector3 playerKnockbackV;

    //Audio
    public AudioSource audioPlayer;
    public AudioClip hurtSound;

    //Main hand
    //Offhand
    //Helmet
    //Chestpiece
    //Leggings
    //Gloves
    //Feet
    //Neck
    //ring
    //ring


    // Use this for initialization
    void Start() {
        r = GetComponent<Rigidbody>();
        c = GetComponent<CharacterController>();
        a = GetComponent<Animator>();
        setExpForLevel();
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

        if (a != null)
            a.speed = momvementSpeedMod;

        if (curHealth <= 0)
        {
            Collider[] detectColliders = Physics.OverlapSphere(transform.position, 100);
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
            Destroy(gameObject);
        }

        if (gameObject.tag == "Player")
        {
            healthUI.text = "" + curHealth;
        }

        

    }

    public void takeDamage(float[] dam)
    {
        audioPlayer.PlayOneShot(hurtSound);

        if (damageable)
            curHealth -= (int)dam[1];

        Vector3 p = new Vector3(dam[2], dam[3], dam[4]);
        p = transform.position - p;
        p = p.normalized;
        p.y = p.y + 1;
        if (transform.tag != "Player")
        {
            r.AddForce(p * 125);
            //spawn x blood
            for (int x = 0; x < Random.Range(1, 8); x++)
            {
                GameObject g;
                Vector3 v = gameObject.transform.position;
                Quaternion q = gameObject.transform.rotation;
                g = Instantiate(Resources.Load("Prefabs/Blood"), v, q) as GameObject;
                Rigidbody b = g.GetComponent<Rigidbody>();
                b.AddForce(p * 250);
            }
        }
        else
        {
            //c.AddForce(p * 250); Work out how to do character controller knockback easily here
            playerKnockback = 0.25f;
            playerKnockbackV = p;
        }
    }

    private void levelUp()
    {
        //More stat stuff to do here, gotta get it written down.
        strength++;
        agility++;
        intellect++;

        attack += 10;
        maxHealth += 10;
        curHealth = maxHealth;
        mana = intellect * 10;
        level++;
        expForLevel = level * 100 * (int)(level * 0.25);
        refreshWeaponDamage();
        PlayerControl p = GetComponent<PlayerControl>();
        p.refreshStats();
        setExpForLevel();
    }

    void refreshWeaponDamage()
    {
        //Weapon
        //TODO weapon class here 
        //Magic
        int x = magicSpell.GetComponent<Spell>().magicAttack;
        magicAttack = x + (int)(x * 0.5);
    }

    void setExpForLevel()
    {
        expForLevel = level * 100 * (level * 0.25f);
        print(expForLevel);
    }
}
