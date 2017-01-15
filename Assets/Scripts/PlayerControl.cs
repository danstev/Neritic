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

    //Stat stuff
    private Statistics stats;
    private Inventory inv;

    //Audio stuff
    public AudioSource audioPlayer;
    public AudioClip weaponAttack;

    // Use this for initialization
    void Start () {
        refreshWeapon();
        stats = GetComponent<Statistics>();
        inv = GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update () {

        Cursor.lockState = CursorLockMode.Locked;

        //Mouse handler
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 100);
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothnes);
        if(currentXRotation > bottom)
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


        //Attack 
        if (Input.GetMouseButtonDown(0))
        {
            if (attackTimeCD <= 0)
            {
                attackTimeCD = stats.attackTime;
                anim.SetTrigger("swing");
                audioPlayer.PlayOneShot(weaponAttack);
                RaycastHit melee = new RaycastHit();
                if (Physics.Raycast(transform.position, transform.forward, out melee, stats.meleeReach))
                {
                    Debug.DrawLine(transform.position, melee.transform.position, Color.cyan, 10f);
                    float[] v = new float[6];
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
        if (attackTimeCD > 0)
        {
            attackTimeCD -= Time.deltaTime;
        }

        //use 
        if (Input.GetKeyDown("e"))
        {
            
            RaycastHit use = new RaycastHit();

            if(Physics.Raycast(transform.position, transform.forward, out use, 2f))
            {
                Debug.DrawLine(transform.position, use.transform.position, Color.cyan, 10f);
                use.transform.SendMessage(("worldUse"), inv, SendMessageOptions.DontRequireReceiver);
            }
       }

        //spell f 
        if (Input.GetKeyDown("f"))
        {
            GameObject spell;
            spell = Instantiate(stats.magicSpell, transform.position, cam.transform.rotation) as GameObject;
            Rigidbody spellR = spell.GetComponent<Rigidbody>();
            spellR.AddForce(spell.transform.forward * stats.magicSpeed);
        }

    }

    void refreshWeapon() //Refreshes what gameObject to use for the animator.
    {
        Animator[] anims = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator a in anims)
        {
            if (a.gameObject.name == "PlayerWeapon")
            {
                anim = a;
            }
        }
    }
}
