using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DD_NW_Range_Attack : NetworkBehaviour
{
    // ----------------------------------------------------------------------
    // Combat
    public GameObject go_projectile;
    public float fl_cool_down = 0.3f;
    private float fl_next_shot_time;
    private Camera go_PC_camera;
    private GameObject go_PC_gun;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        go_PC_gun = transform.Find("Gun").gameObject;      

        if (isLocalPlayer)
        {
            go_PC_camera = GetComponentInChildren<Camera>();           
        }
        else
        {
            go_PC_gun.SetActive(false);     
        }
    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
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
        // Rotate Gun with Cam
        go_PC_gun.transform.rotation = go_PC_camera.transform.rotation;

        if (Input.GetButtonDown("Fire1") && Time.time > fl_next_shot_time)
        {
            UpdateNextShotTime();
            CmdFireBullet(go_PC_camera.transform.rotation);
        }
    }//-----


    // ----------------------------------------------------------------------
    [Command] // Send Commands to the server objects
    void CmdFireBullet(Quaternion _qt_rotation)
    {
        // Create a bullet on the server and reset the shot timer
        var _bullet = Instantiate(go_projectile, go_PC_gun.transform.position, _qt_rotation);

        NetworkServer.Spawn(_bullet); // The object to spawn must be specified in the NW Manager
    }//-----


    // ----------------------------------------------------------------------
    void UpdateNextShotTime()
    {
        if (isLocalPlayer)
            fl_next_shot_time = Time.time + fl_cool_down;
    }//-----

}//========
