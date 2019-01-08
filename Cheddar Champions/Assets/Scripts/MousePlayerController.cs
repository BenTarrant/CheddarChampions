using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MousePlayerController : NetworkBehaviour {

    public float moveSpeed = 4f;
    Vector3 forward, right;


    Animator PlayerAnim;
    public static bool _isPlayerWithinZone;
    //private Vector3 size;
    //private float cheeseScale = 1.0f;
    public Joystick joystick;



    //Variables for extrusion
    public Material material;
    [Header("Extrusion Level")]
    [Range(0, 0.08f)]
    public float maxExtrusionAmount = 0.08f;

    public override void OnStartLocalPlayer()
    {
        
        _isPlayerWithinZone = false;

        Camera.main.GetComponentInParent<CameraFollow>().setTarget(gameObject.transform);
        PlayerAnim = GetComponent<Animator>();
        gameObject.GetComponent <NetworkAnimator> ().SetParameterAutoSend(0, true);
    }

    // Use this for initialization
    void Start () {


        if (!isLocalPlayer)
        {
            print("not local Player");
            return;
        }

        GameObject Fixedjoystick = GameObject.FindGameObjectWithTag("Joystick") as GameObject;
        joystick = FindObjectOfType<Joystick>();

        //BASIC PC MOVEMENT CONTROLS FOR DEGUGGING AND BUILD TESTING ------ TO BE REMOVED
        forward = Camera.main.transform.forward; // set forward vector to equal camera's forward vector
        forward.y = 0; //ensure our y value always set to 0
        forward = Vector3.Normalize(forward); //making sure the vector is set to 1 for motion
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // creates a rotation for our right vector, rotated 90 degrees around the x axis.


    }

    // Update is called once per frame
    void Update ()
    {

        if (isLocalPlayer)
        {
            PlayerAnim.SetBool("bl_walk", false);

            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero)
            {
                
                PlayerAnim.SetBool("bl_walk", true);
                transform.rotation = Quaternion.LookRotation(moveVector);
                transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
            }

            //BASIC PC MOVEMENT CONTROLS FOR DEGUGGING AND BUILD TESTING ------ TO BE REMOVED

            if (Input.GetAxis("HorizontalKey") != 0)
            {
                Move();
                PlayerAnim.SetBool("bl_walk", true);
            }

            if (Input.GetAxis("VerticalKey") != 0)
            {
                Move();
                PlayerAnim.SetBool("bl_walk", true);
            }
        }

    }

    public void Consume()
    {
        if (_isPlayerWithinZone)
        {
            GameManager.sGM.score++;
            //PlayerAnim.SetBool("bl_eating", true);
            print("Eating");
        }

        else
        {
            print("not able to eat");
        }


    }

    //BASIC PC MOVEMENT CONTROLS FOR DEGUGGING AND BUILD TESTING ------ TO BE REMOVED
    void Move()
    {
        //Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey")); // new direction equals values specified in input manager
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;
        transform.position += rightMovement; // sets the left/right movement of the player to be where the input Vector3 dictates
        transform.position += upMovement; // sets the up/down movement of the player to be where the input Vector3 dictates

    }



    // PLAYER NEAR/ EATING CHEESE

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cheese") // if the player triggers
        {
            _isPlayerWithinZone = true; // set boolean to true
            //print(other.gameObject.name);
        }

    }

    void OnTriggerExit(Collider other) // when trigger leaves collision
    {
        if (other.tag == "Cheese") // ensure it's the player leaving
        {
            _isPlayerWithinZone = false; // set boolean to false

        }

    }

}
