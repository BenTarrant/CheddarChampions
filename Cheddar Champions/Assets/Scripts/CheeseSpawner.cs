using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheeseSpawner : NetworkBehaviour {


    public GameObject BigCheese;
    public int numberOfCheese;

    public void Start()
    {


    }
    public override void OnStartServer()
    {
        base.OnStartServer();

        for (int i = 0; i < numberOfCheese; i++)
        {
            //Vector3 spawnPosition = gameObject.transform.position;

            GameObject cheese = (GameObject)Instantiate(BigCheese, transform.position, transform.rotation);

            NetworkServer.Spawn(cheese);

        }
        
    }

    // Update is called once per frame
    void Update () {

		
	}
}
