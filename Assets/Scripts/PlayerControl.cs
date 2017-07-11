﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class PlayerControl : NetworkBehaviour{

    //3d stuff
    public Transform cam;

    //Mouse handling variavles
    private float yRotation;
    private float xRotation;
    public float lookSensitivity = 5;
    private float currentXRotation;
    private float currentYRotation;
    private float yRotationV;
    private float xRotationV;
    public float lookSmoothnes = 0.1f;
    public float bottom = 60F;
    public float top = -60f;
    public float startX;
    public float startY;
    public float startZ;
    public string deathSetting;

    //Movement
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private RaycastHit hit;
    public string floor;

    //animstuff
    public Animator anim;
    private AnimationState swingState;
    private float attackTimeCD = 0f;
    private float magicTimeCD = 0f;

    //Inv stuff
    private Statistics stats;
    private Inventory inv;
    private string tab;
    private string GUION = "basic";
    private GUIStyle style;

    //Silly gui stuff, but efficient
    public Texture2D[] invTextures = new Texture2D[20];
    public Texture2D[] equipTextures = new Texture2D[10];
    public Texture2D weapon;
    public Texture2D spell;
    private string statsPage;
    public Texture2D esc;
    public Texture2D u;
    public Texture2D statsImage;
    public Texture2D tabImage;

    //Audio stuff
    public AudioSource audioPlayer;
    public AudioClip weaponAttack;

    private delegate void move();
    move movement;

    //Tick system?
    int tick;

    void Start () {

        stats = GetComponent<Statistics>();
        inv = GetComponent<Inventory>();
        refreshStats();
        Cursor.lockState = CursorLockMode.Locked;
        movement = mouseController;
        movement += movementController;
        tick = 0;
        statsPage = stats.updateStatPage();
        print(equipTextures.Length);

        //if (!isLocalPlayer)
        //    disableCamHud();

        //style = new GUIStyle();
        //style.alignment = TextAnchor.UpperCenter;
    }

    void disableCamHud()
    {
        GameObject cam = transform.Find("Camera").gameObject;
        GameObject hud = transform.Find("HUD").gameObject;
        GameObject Rain = transform.Find("Rain").gameObject;
        cam.SetActive(false);
        hud.SetActive(false);
        Rain.SetActive(false);
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        
        if (isLocalPlayer)
        {
            uiStuff();
        }
        
        if( isServer )
        {
            disableCamHud();
        }
    }

    void FixedUpdate()
    {
        if (tick % 60 == 0)
        {
            if(stats.curMana < stats.maxMana)
            stats.curMana++;
            uiStuff();
            statsPage = stats.updateStatPage();
        }
        tick++;
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        int gh = Screen.width / 12;
        int hg = Screen.height / 12;
        if (GUION == "basic")
        {

            //Just top ui
            for (int x = 0; x < 6; x++)
            {
                if (inv.slots[x] == null)
                {
                    GUI.Box(new Rect((gh * x + (gh * 2.5f)) + (gh/2), (hg * 11) - (hg /2), gh/2, gh / 2), (x + 1).ToString()); //draw buttons here
                }
                else
                {
                    GUI.Box(new Rect((gh * x + (gh * 2.5f)) + (gh / 2), (hg * 11) - (hg / 2), gh / 2, gh / 2), (x + 1).ToString()); //draw buttons here
                    GUI.Box(new Rect((gh * x + (gh * 2.5f)) + (gh / 2), (hg * 11) - (hg / 2), gh / 2, gh / 2), invTextures[x]);
                }
            }

            //Weapon with RMB
            GUI.backgroundColor = Color.black;
            GUI.Box(new Rect(gh * 10, hg * 10, gh / 2, gh / 2), "RMB");
            GUI.backgroundColor = Color.clear;
            GUI.Box(new Rect(gh * 10, hg * 10 + 15, gh / 2, gh / 2), weapon);
            //Spell with F
            GUI.backgroundColor = Color.black;
            GUI.Box(new Rect(gh * 11, hg * 10, gh / 2, gh / 2), "F");
            GUI.backgroundColor = Color.clear;
            GUI.Box(new Rect(gh * 11, hg * 10 + 15, gh / 2, gh / 2), spell);

            GUI.backgroundColor = Color.black;
            GUI.Box(new Rect(gh /2, hg * 3, gh / 2, gh / 2), "ESC");
            GUI.Box(new Rect(gh /2, hg * 4, gh / 2, gh / 2), "U");
            GUI.Box(new Rect(gh /2, hg * 5, gh / 2, gh / 2), "Y");
            GUI.Box(new Rect(gh /2, hg * 6, gh / 2, gh / 2), "TAB");
            GUI.backgroundColor = Color.clear;
            GUI.Box(new Rect(gh / 2, hg * 3, gh / 2, gh / 2), esc);
            GUI.Box(new Rect(gh / 2, hg * 4, gh / 2, gh / 2), u);
            GUI.Box(new Rect(gh / 2, hg * 5, gh / 2, gh / 2), statsImage);
            GUI.Box(new Rect(gh / 2, hg * 6, gh / 2, gh / 2), tabImage);

        }
        else if (GUION == "inv")
        {
            for (int x = 0; x < 20; x++)
            {
                if (inv.slots[x] == null)
                {
                    GUI.backgroundColor = Color.blue;
                    GUI.Box(new Rect((gh * 3) + (gh * (x%6)), (hg * 3) + (hg * (x / 6)) , gh / 2, gh / 2), (x + 1).ToString()); //draw buttons here
                }
                else
                {
                    if (GUI.Button(new Rect((gh * 3) + (gh * (x % 6)), (hg * 3) + (hg * (x / 6)), gh / 2, gh / 2), invTextures[x]))
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            Item i = inv.slots[x].GetComponent<Item>();
                            i.use(stats, inv);
                        }
                        else if (Input.GetMouseButtonUp(1))
                        {
                            inv.dropItem(inv.slots[x]);
                        }
                        
                    }
                }
            }
        }
        else if (GUION == "equipment")
        {
            //GUI.color = Color.clear;
            GUI.Box(new Rect(gh * 5, hg * 4, gh, gh), "weapon");
            GUI.Box(new Rect(gh * 6, hg * 4, gh, gh), "helmet");
            GUI.Box(new Rect(gh * 6, hg * 6, gh, gh), "body");
            GUI.Box(new Rect(gh * 6, hg * 8, gh, gh), "legs");
            GUI.Box(new Rect(gh * 5, hg * 6, gh, gh), "gloves");
            GUI.Box(new Rect(gh * 5, hg * 8, gh, gh), "feet");
            GUI.Box(new Rect(gh * 7, hg * 6, gh, gh), "neck");
            GUI.Box(new Rect(gh * 7, hg * 4, gh, gh), "ring");
            //GUI.color = Color.magenta;
            if (inv.equipped[0] != null)
            {
                if (GUI.Button(new Rect(gh * 5, hg * 4, gh, gh), equipTextures[0]))
                {
                    inv.unequipItem(inv.equipped[0]);
                }
            }

            if (inv.equipped[2] != null)
            {
                if (GUI.Button(new Rect(gh * 6, hg * 4, gh, gh), equipTextures[2]))
                {
                    inv.unequipItem(inv.equipped[2]);
                }
                
            }

            if (inv.equipped[3] != null)
            {
                if (GUI.Button(new Rect(gh * 6, hg * 6, gh, gh), equipTextures[3]))
                {
                    inv.unequipItem(inv.equipped[3]);
                }
            }

            if (inv.equipped[4] != null)
            {
                if (GUI.Button(new Rect(gh * 6, hg * 8, gh, gh), equipTextures[4]))
                {
                    inv.unequipItem(inv.equipped[4]);
                }
            }

            if (inv.equipped[5] != null)
            {
                if (GUI.Button(new Rect(gh * 5, hg * 6, gh, gh), equipTextures[5]))
                {
                    inv.unequipItem(inv.equipped[5]);
                }
            }

            if (inv.equipped[6] != null)
            {
                if (GUI.Button(new Rect(gh * 5, hg * 8, gh, gh), equipTextures[6]))
                {
                    inv.unequipItem(inv.equipped[6]);
                }
            }

            if (inv.equipped[7] != null)
            {
                if (GUI.Button(new Rect(gh * 7, hg * 6, gh, gh), equipTextures[7]))
                {
                    inv.unequipItem(inv.equipped[7]);
                }
            }

            if (inv.equipped[8] != null)
            {
                if (GUI.Button(new Rect(gh * 7, hg * 4, gh, gh), equipTextures[8]))
                {
                    inv.unequipItem(inv.equipped[8]);
                }
            }

        }
        else if(GUION == "menu")
        {
            //Start game
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Home"))
            {
                SceneManager.LoadScene("home");
            }
            //Help
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 25, 100, 50), "Controls"))
            {
                GUION = "help";
            }

            //exit
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 125, 100, 50), "Exit Game"))
            {
                Application.Quit();
            }
        }
        else if(GUION == "help")
        {
            int w = Screen.width / 12;
            int h = Screen.height / 12;
            GUI.Box(new Rect(w * 6 - (w / 2), h * 6 - (h * 4), w, h), "Controls");
            GUI.backgroundColor = Color.blue;
            GUI.Box(new Rect(w * 6 - (w), h * 6 - (h * 3), w * 2, h), "Use WASD or the arrow \nkeys to move around and the mouse to aim.");
            GUI.Box(new Rect(w * 6 - (w), h * 6 - (h * 2), w * 2, h), "Clicking swings your \nsword, \"F\" fires a fire spell.");
            GUI.Box(new Rect(w * 6 - (w), h * 6 - (h * 1), w * 2, h), "Use tab to open inventory, \n\"U\" to open equipment and \n\"Escape\" to open the \nmain menu.");
        }
        else if(GUION == "stats")
        {
            int w = Screen.width / 12;
            int h = Screen.height / 12;
            GUI.Box(new Rect(w * 5, h * 4, w * 2, h * 3), statsPage);
        }
       
    }

	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        movement();

        //Attack 
        if (Input.GetMouseButtonDown(0) && GUION == "basic")
        {
            if (inv.weaponEquippedCheck())
            {
                attack();
            }
        }

        if (attackTimeCD > 0)
        {
            attackTimeCD -= Time.deltaTime;
        }

        if (magicTimeCD > 0)
        {
            magicTimeCD -= Time.deltaTime;
        }

        //use 
        if (Input.GetKeyDown("e"))
        {
            worldUse();
        }

        if (Input.GetKeyDown("f"))
        {
            castSpell();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GUION == "basic")
            {
                Cursor.lockState = CursorLockMode.None;
                GUION = "inv";
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                GUION = "basic";
            }
        }

        if (Input.GetKeyDown("u"))
        {
            if (GUION == "basic")
            {
                Cursor.lockState = CursorLockMode.None;
                GUION = "equipment";
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                GUION = "basic";
            }
        }

        if (Input.GetKeyDown("y"))
        {
            if (GUION == "basic")
            {
                GUION = "stats";
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                GUION = "basic";
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            if (GUION == "basic")
            {
                GUION = "menu";
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                GUION = "basic";
            }
        }

        if (Input.GetKeyDown("1"))
        {
            Item i = inv.slots[0].GetComponent<Item>();
            i.use(stats, inv);
        }

        if (Input.GetKeyDown("2"))
        {
            Item i = inv.slots[1].GetComponent<Item>();
            i.use(stats, inv);
        }
        if (Input.GetKeyDown("3"))
        {
            Item i = inv.slots[2].GetComponent<Item>();
            i.use(stats, inv);
        }
        if (Input.GetKeyDown("4"))
        {
            Item i = inv.slots[3].GetComponent<Item>();
            i.use(stats, inv);
        }
        if (Input.GetKeyDown("5"))
        {
            Item i = inv.slots[4].GetComponent<Item>();
            i.use(stats, inv);
        }
        if (Input.GetKeyDown("6"))
        {
            Item i = inv.slots[5].GetComponent<Item>();
            i.use(stats, inv);
        }
    }

    public void refreshWeapon(GameObject w) //Refreshes what gameObject to use for the animator.
    {
        Animator a = w.GetComponent<Animator>();
        anim = a;
    }

    public void refreshStats() //I think this is it for player controller;
    {
        int ag = stats.equippedAgility + stats.agility;

        if(ag < 30)
        {
            speed = (ag / 3) + 1;
        }
        else if(ag > 30)
        {
            speed = 10;
        }
          
    }

    void castSpell()
    {
        if (magicTimeCD <= 0)
        {
            if(stats.curMana >= stats.magicSpell.GetComponent<Spell>().manaCost)
            {
                magicTimeCD = stats.magicTime;
                GameObject spell;
                spell = Instantiate(stats.magicSpell, transform.position + transform.forward * 1, cam.transform.rotation) as GameObject;
                Rigidbody spellR = spell.GetComponent<Rigidbody>();
                spellR.AddForce(spell.transform.forward * stats.magicSpeed);
                Spell spellA = spell.GetComponent<Spell>();
                spellA.setMagicAttack((int)stats.magicAttack);
                //Mana cost -
                stats.curMana -= spellA.manaCost;
            }
            else
            {
                //error message?
            }
            
        }
        else if (magicTimeCD < 0)
        {
            
        }
    }

    void worldUse()
    {
        RaycastHit use = new RaycastHit();
        if (Physics.Raycast(transform.position, cam.transform.forward, out use, 3f))
        {
            use.transform.SendMessage(("worldUse"), inv, SendMessageOptions.DontRequireReceiver);
        }
    }

    void mouseController()
    {
        //Mouse handler
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 100);
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothnes);
        if (currentXRotation > bottom)
        {
            currentXRotation = bottom;
            xRotation = bottom;
        }

        if (currentXRotation < top)
        {
            currentXRotation = top;
            xRotation = top;
        }

        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        cam.transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
    }

    void movementController()
    {
        //MovementHandler
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }

    void attack()
    {
        if (attackTimeCD <= 0)
        {
            attackTimeCD = stats.attackTime;
            //anim.SetTrigger("swing");
            anim.Play("swingSword");
            anim.speed = 1 / stats.attackTime;
            audioPlayer.PlayOneShot(weaponAttack);
            RaycastHit melee = new RaycastHit();
            if (Physics.Raycast(transform.position, cam.transform.forward, out melee, stats.meleeReach))
            {
                Debug.DrawLine(transform.position, melee.transform.position, Color.cyan, 10f);
                float[] v = new float[6]; //Redo this so only sends attack, x,y,z
                v[0] = transform.eulerAngles.y;
                v[1] = stats.attack;
                v[2] = transform.position.x;
                v[3] = transform.position.y;
                v[4] = transform.position.z;
                melee.transform.SendMessage(("takeDamage"), v, SendMessageOptions.DontRequireReceiver);
            }
        }
        else if (attackTimeCD < 0)
        {
            //Do nothing, do time trigger for health stuff before here
        }
    }

    void uiStuff()
    {
        GameObject spellF = stats.magicSpell;
        spell = spellF.GetComponent<SpriteRenderer>().sprite.texture;

        if (inv.equipped[0] != null)
        weapon = inv.equipped[0].GetComponent<SpriteRenderer>().sprite.texture;
    }
}
