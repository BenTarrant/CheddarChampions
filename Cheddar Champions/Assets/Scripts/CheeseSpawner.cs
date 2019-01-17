using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheeseSpawner : NetworkBehaviour
{


    public GameObject[] CheeseTypes;
    public Transform spawnPoint; // Where you want it to spawn (transform)

    public override void OnStartServer()
    {
        base.OnStartServer();

        GameObject cheese = (GameObject)Instantiate(CheeseTypes[Random.Range(0, CheeseTypes.Length)], spawnPoint.position, spawnPoint.rotation);

        NetworkServer.Spawn(cheese);

    }
}
        
    
