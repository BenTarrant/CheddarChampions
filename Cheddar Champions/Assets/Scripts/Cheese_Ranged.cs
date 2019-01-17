using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Cheese_Ranged : NetworkBehaviour {

    public GameObject go_projectile;
    public float fl_cool_down = 0.5f;
    private float fl_next_shot_time;

    protected JoyButton joyButton;
    public GameObject SpawnPoint;
    public MousePlayerController PC;
    Animator PlayerAnim;


    // Use this for initialization
    void Start () {

        joyButton = FindObjectOfType<JoyButton>();
        PlayerAnim = GetComponent<Animator>();
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);

    }
    void Update()
    {
        // These functions are only called on the local player
        if (isLocalPlayer)
        {
            Attack();

            if (PlayerAnim == null)
            {
                PlayerAnim = GetComponent<Animator>();

                if (PlayerAnim == null)
                {
                    print("PC Animator not found");
                }
            }


            
            if (PC._isPlayerWithinZone)
            {
                joyButton.GetComponent<Image>().color = Color.yellow;
            }

            if (!PC._isPlayerWithinZone)
            {
                joyButton.GetComponent<Image>().color = Color.white;
            }


        }
    }//-----

    // ----------------------------------------------------------------------
    private void Attack()
    {

        if (joyButton.Pressed && Time.time > fl_next_shot_time) //&& PC._isPlayerWithinZone)
        {
            UpdateNextShotTime();
            PlayerAnim.SetBool("bl_eating", true);
            CmdFireBullet(gameObject.transform.rotation);
            
        }
    }//-----


    // ----------------------------------------------------------------------
    [Command] // Send Commands to the server objects
    void CmdFireBullet(Quaternion _qt_rotation)
    {
        // Create a bullet on the server and reset the shot timer
        var _bullet = Instantiate(go_projectile, SpawnPoint.transform.position, _qt_rotation);
        var tShooter = _bullet.GetComponent<CheeseProjectile>().Shooter = netId;
        NetworkServer.Spawn(_bullet); // The object to spawn must be specified in the NW Manager
        print("NetworkID = " + tShooter);
    }//-----

    void UpdateNextShotTime()
    {
        if (isLocalPlayer)
            fl_next_shot_time = Time.time + fl_cool_down;
    }//-----
     // ----------------------------------------------------------------------


}//========

