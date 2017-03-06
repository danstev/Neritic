using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

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

    //Movement
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private RaycastHit hit;
    public string floor;

    //animstuff
    public Animator anim;
    private float attackTimeCD = 0f;
    private float magicTimeCD = 0f;

    //Inv stuff
    private Statistics stats;
    private Inventory inv;
    private string tab;
    private bool GUION = false;
    private GameObject inventory;
    private GameObject equipment;
    private GameObject spells;
    private GameObject statisticsPage;

    //Audio stuff
    public AudioSource audioPlayer;
    public AudioClip weaponAttack;

    private delegate void move();
    move movement;

    // Use this for initialization
    void Start () {
        refreshWeapon();
        stats = GetComponent<Statistics>();
        inv = GetComponent<Inventory>();
        refreshStats();
        Cursor.lockState = CursorLockMode.Locked;
        movement = mouseController;
        movement += movementController;
    }
	
	// Update is called once per frame
	void Update () {

        movement();

        //Attack 
        if (Input.GetMouseButtonDown(0))
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
            invOn(tab);
        }

        if (Input.GetKeyDown("i"))
        {
            invOn("inventory");
        }

        if (Input.GetKeyDown("u"))
        {
            invOn("equipment");
        }

        if (Input.GetKeyDown("y"))
        {
            invOn("spells");
        }

        if (Input.GetKeyDown("t"))
        {
            invOn("statisticsPage");
        }

    }

    public void refreshWeapon() //Refreshes what gameObject to use for the animator.
    {
        Animator[] anims = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator a in anims)
        {
            if (a.gameObject.tag == "Weapon")
            {
                anim = a;
            }
        }
    }

    public void refreshStats() //I think this is it for player controller;
    {
        speed = (stats.agility / 2) + 1;
    }

    private void invOn()
    {
        if (tab == null)
        {
            invOn(tab);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            inventory.SetActive(true);
            equipment.SetActive(false);
            spells.SetActive(false);
            statisticsPage.SetActive(false);
            GUION = true;
            movement -= mouseController;
        }
    }

    private void invOn(string toDoTab)
    {
        if (GUION == false)
        {
            Cursor.lockState = CursorLockMode.None;
            if (toDoTab == "inventory")
            {
                inventory.SetActive(true);
                equipment.SetActive(false);
                spells.SetActive(false);
                statisticsPage.SetActive(false);
                GUION = true;
                movement -= mouseController;
            }
            else if (toDoTab == "equipment")
            {
                equipment.SetActive(true);
                inventory.SetActive(false);
                spells.SetActive(false);
                statisticsPage.SetActive(false);
                GUION = true;
            }
            else if (toDoTab == "spells")
            {
                spells.SetActive(true);
                inventory.SetActive(false);
                equipment.SetActive(false);
                statisticsPage.SetActive(false);
                GUION = true;
            }
            else if (toDoTab == "statisticsPage")
            {
                statisticsPage.SetActive(true);
                inventory.SetActive(false);
                equipment.SetActive(false);
                spells.SetActive(false);
                GUION = true;
            }
        }
        else if(GUION)
        {
            inventory.SetActive(false);
            equipment.SetActive(false);
            spells.SetActive(false);
            statisticsPage.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            GUION = false;
        }
    }

    void castSpell()
    {
        if (magicTimeCD <= 0)
        {
            magicTimeCD = stats.magicTime;
            GameObject spell;
            spell = Instantiate(stats.magicSpell, transform.position + transform.forward * 1, cam.transform.rotation) as GameObject;
            Rigidbody spellR = spell.GetComponent<Rigidbody>();
            spellR.AddForce(spell.transform.forward * stats.magicSpeed);
            Spell spellA = spell.GetComponent<Spell>();
            spellA.setMagicAttack((int)stats.magicAttack);
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
            anim.SetTrigger("swing");
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
}
