using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] private float moveSpeed = 4f;
    Vector3 forward, right;

    public float VerticalSpeed = 10f;
    public float HorizontalSpeed = 60f;
    public float rotationSpeed = 5f;
    private Transform myTransform;

    private CharacterController Cc;

    public override void OnStartLocalPlayer()
    {
        //Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
    }

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(transform.Find("Camera Target").gameObject);
            transform.Find("MouseLOD0").gameObject.GetComponent<Renderer>().material.color = Color.red;

            return;
        }

       Cc = GetComponent<CharacterController>();
        myTransform = transform;

        //forward = Camera.main.transform.forward; // set forward vector to equal camera's forward vector
        //forward.y = 0; //ensure our y value always set to 0
        //forward = Vector3.Normalize(forward); //making sure the vector is set to 1 for motion
        //right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Move();
        }
  
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        //Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement); //combines and normalises above movment to allow player to have a heading during movement

        //Cc.Move(heading * Time.deltaTime * moveSpeed);
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * HorizontalSpeed;
        float vertical = Input.GetAxis("Vertical") * VerticalSpeed;
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        Quaternion newRotation = Quaternion.LookRotation(moveDirection);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, newRotation, rotationSpeed * Time.deltaTime);

        // CharacterController.Move to move the player in target direction
        Cc.Move(moveDirection * Time.deltaTime);



        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // new direction equals values specified in input manager
        //THIS CODE EXISTS TO ENABLE MOBILE INPUT COMMANDS




        //GetComponent<CharacterController>().SimpleMove(heading * moveSpeed);

        //transform.forward = heading; // sets the rotation of the player to be where the input Vector3 dictates
        //transform.position += rightMovement; // sets the left/right movement of the player to be where the input Vector3 dictates
        //transform.position += upMovement; // sets the up/down movement of the player to be where the input Vector3 dictates
    }
}
