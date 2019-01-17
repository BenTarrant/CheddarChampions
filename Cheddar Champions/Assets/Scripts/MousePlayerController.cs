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

    //public GameObject go_projectile;
    public float fl_cool_down = 0.3f;
    private float fl_next_shot_time;

    protected JoyButton joyButton;
    public GameObject SpawnPoint;

    // COOLDOWN -----------------------------


    // PLAYER FREEZING ----------------------
    public GameObject slowZone;

    // PLAYER SCORE -------------------------
    public int score;
    public Text ScoreText; // reference the UI text

    public Text Timertext; // reference the UI text
    public Text BestScore; // reference for highscore in UI

    public int highScore = 0; // interger reference for highscore
    string highScoreKey = "Best Score: "; // reference for string kept for player prefs

    //TIMER ---------------------------------
    [SyncVar]
    public float timeLeft = 60f; // reference for the amount of time that's passed

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

        score = 0;
        highScore = PlayerPrefs.GetInt(highScoreKey);  //Get the highScore from player prefs if it is there, 0 otherwise.
        BestScore.text = "Best: " + (highScore); // set the best time text to read as the highest score

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
            joyButton = FindObjectOfType<JoyButton>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isLocalPlayer)
        {

            if (ScoreText != null)
            {
                ScoreText.text = "Score: " + score.ToString();

            }

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
                PlayerAnim.SetBool("bl_eating", false);
                transform.rotation = Quaternion.LookRotation(moveVector);
                _controller.Move(moveVector * moveSpeed * Time.deltaTime);
            }


            if (isGrounded == false)
            {
                moveVector += Physics.gravity * Time.deltaTime;
            }

            if (ScoreText == null)
            {
                Text theplayerScore = GameObject.Find("ScoreText").GetComponent<Text>();
                ScoreText = theplayerScore;
            }

            if (ScoreText != null)
            {
                ScoreText.text = "Score: " + score.ToString();
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

        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage();
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
    public void OnCollisionEnter(Collision col)
    {

    }



    void TakeDamage()
    {
        GameObject SlowZone = (GameObject)Instantiate(slowZone, transform.position, transform.rotation);

        NetworkServer.Spawn(SlowZone);

    }

    public void AddScore()
    {
        if (isLocalPlayer)
        {
            score++;
        }

    }

    public void UpdateHighScore() // update high score function (called by the player when they enter the teleport)
    {
        if (score > highScore) // if the score int is higher than the currently stored high score int
        {
            PlayerPrefs.SetInt(highScoreKey, score); // set the high score key as the current score
            PlayerPrefs.Save(); // ensure the player prefs are saved to be retrieved on reload
        }

        else
        {

        }
    }

}
