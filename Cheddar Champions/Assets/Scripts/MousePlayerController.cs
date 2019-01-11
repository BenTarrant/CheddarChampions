using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MousePlayerController : NetworkBehaviour {

    public float moveSpeed = 4f;
    Vector3 forward, right;

    public float gravitymulti;

    Animator PlayerAnim;

    public bool _isPlayerWithinZone;
    public bool _isPlayerEating = false;
    public LayerMask m_LayerMask;
    private float currentCheese;

    //private Vector3 size;
    //private float cheeseScale = 1.0f;
    public Joystick joystick;

    public bool isGrounded = false;
    private CharacterController _controller;

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
        _controller = GetComponent<CharacterController>();
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

    }

    // Update is called once per frame
    void Update ()
    {

        if (isLocalPlayer)
        {
            PlayerAnim.SetBool("bl_walk", false);
            GroundCheck();
            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero && isGrounded == true)
            {

                PlayerAnim.SetBool("bl_walk", true);
                transform.rotation = Quaternion.LookRotation(moveVector);
                _controller.Move(moveVector * moveSpeed * Time.deltaTime);
            }

            if (isGrounded == false)
            {
                moveVector += Physics.gravity * Time.deltaTime;
            }

            
        }

    }

    public void Consume()
    {
        if (_isPlayerWithinZone)
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.yellow);
                Debug.Log("Did Hit");

                GameManager.sGM.score++;
                currentCheese = hit.transform.gameObject.GetComponent<EatCheese>().Health;
                currentCheese--;
                //PlayerAnim.SetBool("bl_eating", true);
                print("Eating");
            }
        }

        else
        {
            _isPlayerEating = false;
            print("not able to eat");
        }


    }

    //isGrounded--------------------------------------------------------------------------------------------------------------------------
    void GroundCheck()
    {
        RaycastHit hit;
        float distance = 4f;
        Vector3 dir = new Vector3(0, -1);
        Debug.DrawRay(transform.position, dir, Color.red);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }
    }


    // PLAYER NEAR/ EATING CHEESE

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cheese") // if the player triggers
        {
            
            _isPlayerWithinZone = true; // set boolean to true
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
