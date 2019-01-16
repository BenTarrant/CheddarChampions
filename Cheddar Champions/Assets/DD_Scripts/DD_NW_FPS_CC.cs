using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DD_NW_FPS_CC : NetworkBehaviour
{
    // ----------------------------------------------------------------------
    // Movement
    public float fl_speed = 3;
    private float fl_initial_speed;
    private CharacterController cc_PC;
    private Vector3 v3_move_direction;
    public float fl_jump_force = 12;
    public float fl_gravity = 20;
    private bool bl_climbing;

    // Cam Postion
    public float fl_cam_rot_rate = 45;
    public float fl_mouse_turn_rate = 120;
    public GameObject go_PC_camera;


    // ----------------------------------------------------------------------
    void Start()
    {
        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Find the childed PC Camera
        go_PC_camera = transform.Find("PC_Cam").gameObject;       

        if (!isLocalPlayer) // if we are not a local player
        {
            // Remove the PC cameras from other NW PLayers
            go_PC_camera.SetActive(false);           

            // Colour other Network Players Red
            transform.Find("Pawn").gameObject.GetComponent<Renderer>().material.color = Color.red;
            return;
        }

        // Get Local reference and set walkingspeed
        cc_PC = GetComponent<CharacterController>();
        fl_initial_speed = fl_speed;
    }//-----

    // ----------------------------------------------------------------------
    void Update()
    {
        // Lock Cursor
        if (Input.GetKeyDown("o")) Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown("i")) Cursor.lockState = CursorLockMode.None;

        // These functions are only called on the local player
        if (isLocalPlayer)
        {
            MovePC();
            MouseLook();
        }           
    }//-----    


    // ----------------------------------------------------------------------
    void MovePC()
    {
        // Run with Shift
        if (Input.GetKey(KeyCode.LeftShift)) fl_speed = fl_initial_speed * 2; else fl_speed = fl_initial_speed;

        // Rotate PC with Mouse 
        transform.Rotate(0, fl_mouse_turn_rate * Time.deltaTime * Input.GetAxis("Mouse X"), 0);

        //  PC Ground Movement
        if (cc_PC.isGrounded)
        {
            // Add X & Z movement to the direction vector based input axes (W,S or Cursor) 
            v3_move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Convert world coordinates to local for the PC and multiply by speed
            v3_move_direction = fl_speed * transform.TransformDirection(v3_move_direction);

            // if the jump key is pressed add jump force to the direction vector
            if (Input.GetButtonDown("Jump")) v3_move_direction.y = fl_jump_force;
        }


        // PC Climb Movement
        if (bl_climbing)
        {
            // Add Y movement to the direction vector based on Vertical input (W,S or Cursor U,D) 
            v3_move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            v3_move_direction = fl_speed / 2 * transform.TransformDirection(v3_move_direction);
            if (Input.GetButton("Jump")) v3_move_direction.y = fl_speed;
        }
        else
        {  // Add fl_gravity to the direction vector
            v3_move_direction.y -= fl_gravity * Time.deltaTime;
        }

        // Move the character controller with the direction vector
        cc_PC.Move(v3_move_direction * Time.deltaTime);
    }//-----
    

    // Mouse Look --------------------------------------------------------
    void MouseLook()
    {

        // Move Cam up and Down
        if (Input.GetAxis("Mouse Y") > 0) go_PC_camera.transform.Rotate(-fl_cam_rot_rate * Time.deltaTime, 0, 0);
        if (Input.GetAxis("Mouse Y") < 0) go_PC_camera.transform.Rotate(fl_cam_rot_rate * Time.deltaTime, 0, 0);

        // Rotate Gun
        // transform.Find("Gun").transform.rotation = go_PC_camera.transform.rotation;
    }//-----


    //-------------------------------------------------------------------------
    // When PC enters a trigger
    void OnTriggerStay(Collider cl_trigger_collider)
    {
        if (cl_trigger_collider.gameObject.tag == "Moving") transform.parent = cl_trigger_collider.transform;
        if (cl_trigger_collider.gameObject.tag == "ClimbZone") bl_climbing = true;
    }//-----


    //-------------------------------------------------------------------------
    // PC Leaving the Trigger
    void OnTriggerExit(Collider cl_trigger_collider)
    {
        if (cl_trigger_collider.gameObject.tag == "Moving") transform.parent = null;
        if (cl_trigger_collider.gameObject.tag == "ClimbZone") bl_climbing = false;
    }//-----

}//==========
