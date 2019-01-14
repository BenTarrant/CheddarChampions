using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MousePlayerController : NetworkBehaviour
{


    // PLAYER MOVEMENT VARIABLES ------------
    public float moveSpeed = 4f;
    public Joystick joystick;
    public bool isGrounded = false;
    private CharacterController _controller;
    Animator PlayerAnim;

    // CHEESE EATING VARIABLES --------------
    public bool _isPlayerWithinZone;
    public bool _isPlayerEating = false;
    public GameObject cheeseBullet;
    public float coolDown = 1;
    private float eat_cooldown;
    //private GameObject cheeseSpawn;

    // Variables for extrusion ------------
    public Material material;
    [Header("Extrusion Level")]
    [Range(0, 0.08f)]
    public float maxExtrusionAmount = 0.08f;

    public override void OnStartLocalPlayer()
    {
        _isPlayerWithinZone = false;
        Camera.main.GetComponentInParent<CameraFollow>().setTarget(gameObject.transform);
        PlayerAnim = GetComponent<Animator>();
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
        _controller = GetComponent<CharacterController>();

        eat_cooldown = coolDown;
    }

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            print("not local Player");
            return;
        }

        joystick = FindObjectOfType<Joystick>();

        //cheeseSpawn = transform.Find("CheeseSpawn").gameObject;
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

            if (eat_cooldown > 0)
            {
                eat_cooldown -= Time.deltaTime;
            }

            if (PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("Eating"))
            {
                print("Imma Eating");

                if (PlayerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    PlayerAnim.SetBool("bl_eating", false);
                }
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

    // PLAYER NEAR/ EATING CHEESE
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cheese") // if the player triggers
        {

            _isPlayerWithinZone = true; // set boolean to true
        }

        if (other.tag == "Trap")
        {
            print("Hit Trap");
            TakeDamage();
        }
    }

    void OnTriggerExit(Collider other) // when trigger leaves collision
    {
        if (other.tag == "Cheese") // ensure it's the player leaving
        {
            _isPlayerWithinZone = false; // set boolean to false
        }
    }

    void TakeDamage()
    {

    }

    [Command]
    public void CmdFireCheese()
    {
        if (true)//(eat_cooldown < Time.deltaTime)
        {
            PlayerAnim.SetBool("bl_eating", true);

            print("firing cheese bullet");
            var _bullet = Instantiate(cheeseBullet, transform.position, transform.rotation);

            eat_cooldown = coolDown;

            NetworkServer.Spawn(_bullet);
        }
    }
}
