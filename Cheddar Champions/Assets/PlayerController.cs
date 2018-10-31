using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour {

    public Camera IsoCam;
    public NavMeshAgent Agent;

    void Start()
    {
        //if (!isLocalPlayer)
        {
            transform.Find("Capsule").gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update () {
		
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = IsoCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Move the Player

                Agent.SetDestination(hit.point);
            }
        }
	}
}
