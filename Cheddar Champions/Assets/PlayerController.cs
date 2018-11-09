using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]

    float moveSpeed = 4f;

    Vector3 forward, right;


	// Use this for initialization
	void Start () {

        forward = Camera.main.transform.forward; // set forward vector to equal camera's forward vector
        forward.y = 0; //ensure our y value always set to 0
        forward = Vector3.Normalize(forward); //making sure the vector is set to 1 for motion
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKey) // although any key declared, only WASD assigned the values necessary
        {
            Move(); // move the player
        }
		
	}

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // new direction equals values specified in input manager
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement); //combines and normalises above movment to allow player to have a heading during movement

        transform.forward = heading; // sets the rotation of the player to be where the input Vector3 dictates
        transform.position += rightMovement; // sets the left/right movement of the player to be where the input Vector3 dictates
        transform.position += upMovement; // sets the up/down movement of the player to be where the input Vector3 dictates

    }
}
