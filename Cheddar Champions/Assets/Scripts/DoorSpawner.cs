using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour {

    public int doorDirection;
    // 1 = needs a bottom door
    // 2 = needs a top door
    // 3 = needs a left door
    // 4 = needs a right door

    public GameObject bottomDoor, topDoor, leftDoor, rightDoor;
    private bool spawnedDoor;

    void Start()
    {

        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {

        if (spawnedDoor == false)
        {
            if (doorDirection == 1)
            {
                //Spawn BOTTOM door
                Instantiate(bottomDoor, transform.position, transform.rotation);
            }

            else if (doorDirection == 2)
            {
                //Spawn TOP door
                Instantiate(topDoor, transform.position, transform.rotation);
            }

            else if (doorDirection == 3)
            {
                //Spawn LEFT door
                Instantiate(leftDoor, transform.position, transform.rotation);
            }

            else if (doorDirection == 4)
            {
                //Spawn RIGHT door
                Instantiate(rightDoor, transform.position, transform.rotation);
            }

            spawnedDoor = true;
            Destroy(gameObject);

        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorSpawnPoint"))
        {
            if (spawnedDoor == false && spawnedDoor == false)
            {
                //Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            spawnedDoor = true;
        }

        else
        {
            Destroy(gameObject);
        }

    }
}
