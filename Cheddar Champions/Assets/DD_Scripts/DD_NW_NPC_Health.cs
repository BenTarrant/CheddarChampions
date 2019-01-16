using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DD_NW_NPC_Health : NetworkBehaviour {

    // ----------------------------------------------------------------------
    [SyncVar]
    public int in_HP = 30;

    // ----------------------------------------------------------------------
    private void Update()
    {        
        if (in_HP <= 10) GetComponent<Renderer>().material.color = Color.red;
        if (in_HP < 1) NetworkServer.Destroy(gameObject);
    }//-----

    // ----------------------------------------------------------------------
    public void Damage(int _in_damage_taken)
    {
        if (!isServer) return;
        in_HP -= _in_damage_taken;
    }//-----

}//==========
