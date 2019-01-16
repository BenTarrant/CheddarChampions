using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MousePlayerController : NetworkBehaviour
{
    // PLAYER MOVEMENT VARIABLES ------------
    public float moveSpeed = 4f;
    private Joystick joystick;
    public bool isGrounded = false;
    private CharacterController _controller;
    Animator PlayerAnim;

    // CHEESE EATING VARIABLES --------------
    public bool _isPlayerWithinZone;
    public bool _isPlayerEating = false;
    public GameObject cheeseBullet;
    protected JoyButton joyButton;

    // COOLDOWN -----------------------------


    // PLAYER FREEZING ----------------------
    public GameObject slowZone;

    // PLAYER SCORE -------------------------
    //public Text personalScore;


    // Variables for extrusion --------------
    public Material material;
    [Header("Extrusion Level")]
    [Range(0, 0.08f)]
    public float maxExtrusionAmount = 0.08f;

    public override void OnStartLocalPlayer()
    {


    }

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            print("not local Player");
            transform.Find("MOUSE_SKIN").gameObject.GetComponent<Renderer>().material.color = Color.red;
            return;
        }

            _isPlayerWithinZone = false;
            Camera.main.GetComponentInParent<CameraFollow>().setTarget(gameObject.transform);
            PlayerAnim = GetComponent<Animator>();
            gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
            _controller = GetComponent<CharacterController>();
            joystick = FindObjectOfType<Joystick>();



    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (PlayerAnim == null)
            {
                PlayerAnim = GetComponent<Animator>();

                if (PlayerAnim == null)
                {
                    print("PC Animator not found");
                }
            }

            //personalScore.text = "" + GameManager.sGM.score.ToString();

            PlayerAnim.SetBool("bl_walk", false);
            GroundCheck();
            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero && isGrounded == true)
            {
                PlayerAnim.SetBool("bl_walk", true);
                PlayerAnim.SetBool("bl_eating", false);
                transform.rotation = Quaternion.LookRotation(moveVector);
                _controller.Move(moveVector * moveSpeed * Time.deltaTime);
            }

            if (isGrounded == false)
            {
                moveVector += Physics.gravity * Time.deltaTime;
            }

            //if (joyButton.Pressed) //&& _isPlayerWithinZone)
            //{

            //    print("firing bullet");

            //    print("Am I a Server: " + isServer + " Am I a Client: " + isClient + " Do I have Authority: " + hasAuthority + " Am I a Local Player: " + isLocalPlayer);
            //}

            //if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    CmdFireCheese();
            //    print("firing bullet");

            //    print("Am I a Server: " + isServer + " Am I a Client: " + isClient + " Do I have Authority: " + hasAuthority + " Am I a Local Player: " + isLocalPlayer);
            //}

            if (PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("Eating"))
            {

                if (PlayerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    PlayerAnim.SetBool("bl_eating", false);
                }
            }

            if (_isPlayerWithinZone == true)
            {
            }
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


    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Cheese") // if the player triggers
        {
            _isPlayerWithinZone = true; // set boolean to true
        }
    }

    void OnTriggerStay(Collider other)
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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "CheeseBullet(Clone)")
        {
            print("Hit");
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        GameObject SlowZone = (GameObject)Instantiate(slowZone, transform.position, transform.rotation);

        NetworkServer.Spawn(SlowZone);

    }

    void InZone()
    {
        _isPlayerWithinZone = true;
    }

}
