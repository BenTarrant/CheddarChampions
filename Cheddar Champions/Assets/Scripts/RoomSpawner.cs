using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomSpawner : NetworkBehaviour
{
    public int openingDirection;
    // 1 = needs a bottom door
    // 2 = needs a top door
    // 3 = needs a left door
    // 4 = needs a right door

    private RoomTemplates templates;
    private int random;
    private bool spawned;
    GameObject SpawnRoom;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("NetworkSpawn", 0.1f);    
    }

    void NetworkSpawn()
    {
        if (isServer)
        {
            if (spawned == false)
            {
                if (openingDirection == 1)
                {
                    //Spawn room with BOTTOM door
                    random = Random.Range(0, templates.bottomRooms.Length);
                    GameObject roomSpawn = Instantiate(templates.bottomRooms[random], transform.position, Quaternion.identity);
                    NetworkServer.Spawn(roomSpawn);
                }

                else if (openingDirection == 2)
                {
                    //Spawn room with TOP door
                    random = Random.Range(0, templates.topRooms.Length);
                    GameObject roomSpawn = Instantiate(templates.topRooms[random], transform.position, Quaternion.identity);
                    NetworkServer.Spawn(roomSpawn);
                }

                else if (openingDirection == 3)
                {
                    //Spawn room with LEFT door
                    random = Random.Range(0, templates.leftRooms.Length);
                    GameObject roomSpawn = Instantiate(templates.leftRooms[random], transform.position, Quaternion.identity);
                    NetworkServer.Spawn(roomSpawn);
                }

                else if (openingDirection == 4)
                {
                    //Spawn room with RIGHT door
                    random = Random.Range(0, templates.rightRooms.Length);
                    GameObject roomSpawn = Instantiate(templates.rightRooms[random], transform.position, Quaternion.identity);
                    NetworkServer.Spawn(roomSpawn);
                }

                spawned = true;
                //Destroy(gameObject);
                //NetworkServer.Destroy(gameObject);
            }
        }
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                //Spawn room with BOTTOM door
                random = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[random], transform.position, Quaternion.identity);
            }

            else if (openingDirection == 2)
            {
                //Spawn room with TOP door
                random = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[random], transform.position, Quaternion.identity);
            }

            else if (openingDirection == 3)
            {
                //Spawn room with LEFT door
                random = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[random], transform.position, Quaternion.identity);

            }

            else if (openingDirection == 4)
            {
                //Spawn room with RIGHT door
                random = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[random], transform.position, Quaternion.identity);
            }

            spawned = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                //Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            spawned = true;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
