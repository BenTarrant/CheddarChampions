using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cheese_Ranged : NetworkBehaviour {

    public GameObject go_projectile;
    public float fl_cool_down = 0.3f;
    private float fl_next_shot_time;

    protected JoyButton joyButton;
    public GameObject SpawnPoint;

    // Use this for initialization
    void Start () {

        joyButton = FindObjectOfType<JoyButton>();

    }
    void Update()
    {
        // These functions are only called on the local player
        if (isLocalPlayer)
        {
            Attack();
        }
    }//-----

    // ----------------------------------------------------------------------
    private void Attack()
    {

        if (joyButton.Pressed && Time.time > fl_next_shot_time)
        {
            UpdateNextShotTime();
            CmdFireBullet(gameObject.transform.rotation);
        }
    }//-----


    // ----------------------------------------------------------------------
    [Command] // Send Commands to the server objects
    void CmdFireBullet(Quaternion _qt_rotation)
    {
        // Create a bullet on the server and reset the shot timer
        var _bullet = Instantiate(go_projectile, SpawnPoint.transform.position, _qt_rotation);

        NetworkServer.Spawn(_bullet); // The object to spawn must be specified in the NW Manager
    }//-----


    // ----------------------------------------------------------------------
    void UpdateNextShotTime()
    {
        if (isLocalPlayer)
            fl_next_shot_time = Time.time + fl_cool_down;
    }//-----

}//========

