using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MousePlayerController : NetworkBehaviour {

    public float moveSpeed = 4f;
    Vector3 forward, right;

   Animator PlayerAnim;
    //bool bl_walk = false;

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponentInParent<CameraFollow>().setTarget(gameObject.transform);
        PlayerAnim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {

            if (!isLocalPlayer)
            {
<<<<<<< HEAD:Cheddar Champions/Assets/Scripts/MousePlayerController.cs
                //Destroy(GameObject.Find("Canvas").gameObject);
                //transform.Find("MouseLOD0").gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
=======
                //Destroy(transform.Find("CameraTarget").gameObject);
                //transform.Find("MouseLOD0").gameObject.GetComponent<Renderer>().material.color = Color.red;
>>>>>>> parent of db5a7a9... Initial Mobile Support:Cheddar Champions/Assets/MousePlayerController.cs

                return;
            }


        forward = Camera.main.transform.forward; // set forward vector to equal camera's forward vector
        forward.y = 0; //ensure our y value always set to 0
        forward = Vector3.Normalize(forward); //making sure the vector is set to 1 for motion
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // creates a rotation for our right vector, rotated 90 degrees around the x axis.

    }
	
	// Update is called once per frame
	void Update () {
        if(isLocalPlayer)
        {
            PlayerAnim.SetBool("bl_walk", false);

            if (Input.GetAxis("HorizontalKey") != 0 || (Input.GetAxis("VerticalKey") != 0))
            {
                Move();
                PlayerAnim.SetBool("bl_walk", true);
            }
        }

    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey")); // new direction equals values specified in input manager
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;
        transform.position += rightMovement; // sets the left/right movement of the player to be where the input Vector3 dictates
        transform.position += upMovement; // sets the up/down movement of the player to be where the input Vector3 dictates

    }
}
