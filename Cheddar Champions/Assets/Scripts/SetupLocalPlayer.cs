using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

    [SyncVar]
    public string playerName = "player";


    void OnGUI()
    {
        if (isLocalPlayer)
        {
            //playerName = GUI.TextField (new)
        }
    }

    // Use this for initialization
    void Start ()
    {
		if (isLocalPlayer)
        {
            GetComponent<MousePlayerController>().enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
