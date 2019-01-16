using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheeseSpawner : NetworkBehaviour {


    public GameObject BigCheese;
    public int numberOfCheese;
    public Transform[] spawnPoints;

    public void Start()
    {


    }
    public override void OnStartServer()
    {
        base.OnStartServer();

        for (int i = 0; i < numberOfCheese; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            GameObject cheese = (GameObject)Instantiate(BigCheese, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            NetworkServer.Spawn(cheese);

        }
        
    }

    // Update is called once per frame
    void Update () {

		
	}
}
