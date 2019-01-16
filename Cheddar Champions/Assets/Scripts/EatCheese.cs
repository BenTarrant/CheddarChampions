using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EatCheese : NetworkBehaviour
{
    [SyncVar]
    public float Health = 5f;
    public MousePlayerController PC;
    public GameObject CheeseExplosion;

    // ----------------------------------------------------------------------
    private void Update()
    {
        if (Health < 2) GetComponent<Renderer>().material.color = Color.red;
        if (Health < 1) NetworkServer.Destroy(gameObject);
    }//-----

    // ----------------------------------------------------------------------
    public void Damage(int _in_damage_taken)
    {
        if (!isServer) return;
       Health -= _in_damage_taken;

    }
       

    }//-----
