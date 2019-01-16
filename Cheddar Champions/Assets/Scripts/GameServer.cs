using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameServer : NetworkManager {      //We inherit from Network Manager, and take over some of the functions

    public  int Players;    //Players can check this to get current player number

    //Called when We have a client connect
    public override void OnServerConnect(NetworkConnection conn) {
        base.OnServerConnect(conn);     //call base class
        Players++;                      //Added a player
        Debug.Log("OnServerConnect");
    }

	public override void OnServerDisconnect(NetworkConnection conn) {
		base.OnServerDisconnect(conn);
        Players--;                  //Removed a player
        Debug.Log("OnServerDisconnect");
	}
}
