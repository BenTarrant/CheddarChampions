using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DD_NW_PC_Control : NetworkBehaviour
{
    // ----------------------------------------------------------------------
    // Movement
    public float fl_speed = 3;
    private float fl_initial_speed;
    private CharacterController CC_PC;
    private Vector3 V3_moveDirection;
    public float fl_jump_force = 8;
    public float fl_gravity = 20;
    private bool bl_climbing;

    // Combat
    public GameObject go_projectile;
    public float fl_cool_down = 0.3f;
    private float fl_next_shot_time;

    // ----------------------------------------------------------------------
    void Start()
    {
        if (!isLocalPlayer) // if we are not a local player
        {
            // Remove the PC cameras from other NW PLayers
            Destroy(transform.Find("PC_Cam").gameObject);

            // Colour other Network Players Red
            transform.Find("Pawn").gameObject.GetComponent<Renderer>().material.color = Color.red;
            return;
        }

        // Get Local reference and set walkingspeed
        CC_PC = GetComponent<CharacterController>();
        fl_initial_speed = fl_speed;
    }//-----

    // ----------------------------------------------------------------------
    void Update()
    {
        // These functions are only called on the local player
        if (isLocalPlayer)
        {
            MovePC();
            MouseLook();

            if (Input.GetButtonDown("Fire1") && Time.time > fl_next_shot_time)
            {
                UpdateNextShotTime();
                CmdFireBullet();
            }
        }
    }//-----


    // ----------------------------------------------------------------------
    [Command] // Send Commands to the server objects
    void CmdFireBullet()
    {
        // Create a bullet on the server and reset the shot timer
        var _bullet = Instantiate(go_projectile, transform.position + 
            transform.TransformDirection(new Vector3(0, 0.5F, 1.5F)), transform.rotation);
        
        NetworkServer.Spawn(_bullet); // The object to spawn must be specified in the NW Manager
    }//-----


    // ----------------------------------------------------------------------
    void UpdateNextShotTime()
    {
        fl_next_shot_time = Time.time + fl_cool_down;
    }//-----


    // ----------------------------------------------------------------------
    void MovePC()
    {
        // If the run key pressed double the speed
        if (Input.GetKey(KeyCode.LeftShift)) fl_speed = fl_initial_speed * 2; else fl_speed = fl_initial_speed;

        //  PC Ground Movement
        if (CC_PC.isGrounded)
        {
            // Add X & Z movement to the direction vector based on Vertical input (W,S or Cursor U,D) 
            V3_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Convert world coordinates to local for the PC and multiple by speed
            V3_moveDirection = fl_speed * transform.TransformDirection(V3_moveDirection);

            // if the jump key is pressed add jump force to the direction vector
            if (Input.GetButton("Jump")) V3_moveDirection.y = fl_jump_force;
        }       
            // Add fl_gravity to the direction vector
            V3_moveDirection.y -= fl_gravity * Time.deltaTime;     

        // Move the character controller with the direction vector
        CC_PC.Move(V3_moveDirection * Time.deltaTime);
    }//-----


    // ----------------------------------------------------------------------
    // Cam Postion
    public float fl_min_cam_height = -1F;
    public float fl_max_cam_height = 3F;
    public float fl_cam_distance = 2.5F;
    public float fl_mouse_turn_rate = 90;
    public float fl_cam_speed = 2;
    public GameObject go_cam;

    // Mouse Look ==================================================================================
    void MouseLook()
    {    
        // Zoom in and out with Mouse Scroll
        if (Input.mouseScrollDelta.y > 0 && fl_cam_distance > 0.5F) fl_cam_distance -= 0.2F;
        if (Input.mouseScrollDelta.y < 0 && fl_cam_distance < 3) fl_cam_distance += 0.2F;

        // Mouse Rotate
        transform.Rotate(0, 3 * fl_mouse_turn_rate * Time.deltaTime * Input.GetAxis("Mouse X"), 0);

        // look at PC Object
        go_cam.transform.LookAt(transform.position + new Vector3(0, 1, 0));

        // Move the Camera
        go_cam.transform.localPosition = new Vector3(0, go_cam.transform.localPosition.y, -fl_cam_distance);

        if (Input.GetAxis("Mouse Y") > 0 && go_cam.transform.localPosition.y > fl_min_cam_height) go_cam.transform.Translate(0, -fl_cam_speed * Time.deltaTime, 0);
        if (Input.GetAxis("Mouse Y") < 0 && go_cam.transform.localPosition.y < fl_max_cam_height) go_cam.transform.Translate(0, fl_cam_speed * Time.deltaTime, 0);

    }//-----

}//==========