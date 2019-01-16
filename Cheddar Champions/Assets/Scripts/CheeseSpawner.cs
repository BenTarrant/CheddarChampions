using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheeseSpawner : NetworkBehaviour
{


    public GameObject BigCheese; // Flag
    public int numberOfCheese;
    public Transform spawnPoint; // Where you want it to spawn (transform)

    public override void OnStartServer()
    {
        base.OnStartServer();

        GameObject cheese = (GameObject)Instantiate(BigCheese, spawnPoint.position, spawnPoint.rotation);

        NetworkServer.Spawn(cheese);

    }
}
        
    
