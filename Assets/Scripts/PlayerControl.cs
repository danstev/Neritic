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
    private GUIStyle style;


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
        //style = new GUIStyle();
        //style.alignment = TextAnchor.UpperCenter;
    }

    void FixedUpdate()
    {
        if (tick % 60 == 0)
        {
            if(stats.curMana < stats.maxMana)
            stats.curMana++;
        }
        tick++;
    }

    void OnGUI()
    {
        if (GUION)
        {

        }
        else
        {
            for (int x = 0; x < 20; x++)
            {
                if(x > 8)
                {
                    if(x > 17)
                    {
                        GUI.Box(new Rect(70 * (x % 9) + 30, 170, 50, 50), "hello" + x);
                    }
                    else
                    {
                        GUI.Box(new Rect(70 * (x % 9) + 30, 100, 50, 50), "hello" + x);
                    }
                }
                else
                {
                    GUI.Box(new Rect(70 * (x % 9) + 30, 30, 50, 50), "hello" + x); //draw buttons here
                }
                
            }

        }
    }

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
            
        }
    }

    public void refreshWeapon(GameObject w) //Refreshes what gameObject to use for the animator.
    {
        Animator a = w.GetComponent<Animator>();
        anim = a;
    }

    public void refreshStats() //I think this is it for player controller;
    {
        speed = (stats.actualAgility / 2) + 1;
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
