using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {

    //3d stuff
    public Transform cam;
    //public Transform everythingElse; //???

    //Mouse handling variavles
    private float yRotation;
    private float xRotation;
    public float lookSensitivity = 5;
    private float currentXRotation;
    private float currentYRotation;
    private float yRotationV;
    private float xRotationV;
    public float lookSmoothnes = 0.1f;
    public float bottom = 20f;
    public float top = -40f;

    //Movement
    public float speed = 6.0F;
    private float tempSpeed;
    public float jumpSpeed = 8.0F;
    private float tempJump;
    public float gravity = 20.0F;
    private float tempGrav;
    private Vector3 moveDirection = Vector3.zero;
    delegate void movementChange();
    delegate void movementStatus();
    private RaycastHit hit;
    public string floor;

    //Lava Variables
    public int lavaDamage;

    // Use this for initialization
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;

        //floorType status effects
        Physics.Raycast(transform.position, Vector3.down, out hit, 20.0f);
        floor = hit.collider.name;
        movementChange MovementChange;
        movementStatus MovementStatus;

        switch (floor)
        {
            case "Lava":
                MovementChange = LavaGround;
                MovementStatus = LavaGroundStatus;
                break;
            case "Water":
                MovementChange = WaterGround;
                MovementStatus = WaterGroundStatus;
                break;
            default:
                MovementChange = Ground;
                MovementStatus = GroundStatus;
                break;
        }

        MovementChange();
        MovementStatus();

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

        //MovementHandler
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= tempSpeed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = tempJump;

        }
        //Applying gravity to the controller
        moveDirection.y -= tempGrav * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);



    }





    void LavaGround()
    {
        this.tempGrav = gravity * 1;
        this.tempJump = jumpSpeed * 1;
        this.tempSpeed = speed * 1;
    }

    void LavaGroundStatus()
    {
        //Health health = GetComponent<Health>();
        //health.health -= lavaDamage * Time.deltaTime;
    }
    void WaterGround()
    {
        this.tempGrav = gravity * 0.5f;
        this.tempJump = jumpSpeed * 0.5f;
        this.tempSpeed = speed * 0.75f;
    }

    void WaterGroundStatus()
    {

    }
    void Ground()
    {
        this.tempGrav = gravity * 1;
        this.tempJump = jumpSpeed * 1;
        this.tempSpeed = speed * 1;
    }

    void GroundStatus()
    {

    }
}