using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EatCheese : NetworkBehaviour
{
    [SyncVar]
    public float Health = 5f;

    public GameObject[] PCs;
    MousePlayerController singlePC;
    public GameObject CheeseExplosion;


    private void Start()
    {
        
    }
    // ----------------------------------------------------------------------
    private void Update()
    {
        if (Health < 2) GetComponent<Renderer>().material.color = Color.red;
        if (Health < 1)
        {
            GameObject[] players = (GameObject[])GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                singlePC = GameObject.FindObjectOfType(typeof(MousePlayerController)) as MousePlayerController;
                singlePC._isPlayerWithinZone = false;
            }
            
            NetworkServer.Destroy(gameObject);
        }
    }//-----

    // ----------------------------------------------------------------------
    public void Damage(int _in_damage_taken)
    {
        if (!isServer) return;
        singlePC = GameObject.FindObjectOfType(typeof(MousePlayerController)) as MousePlayerController;
        singlePC.AddScore();
       Health -= _in_damage_taken;

    }
       

    }//-----
