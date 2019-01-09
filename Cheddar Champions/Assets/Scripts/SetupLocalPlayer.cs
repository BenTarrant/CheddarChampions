using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

    [SyncVar]
    public string pname = "player";

    [SyncVar]
    public Color playerColor = Color.white;


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

        this.transform.position = new Vector3(Random.Range(5, -5) , Random.Range(0,0), Random.Range(5, -5));

        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
            r.material.color = playerColor;
    }
	
	// Update is called once per frame
	void Update () {

        this.GetComponentInChildren<TextMesh>().text = pname;
		
	}
}
