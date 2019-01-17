using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FloatingText : NetworkBehaviour {

    Rigidbody mRB;
    Text mText;

    [SyncVar]
    public  string mString;

    [SyncVar]
    public Vector3 mForce;

	private void Start() {
        GetComponent<Rigidbody>().AddForce(mForce, ForceMode.Impulse);
        GetComponentInChildren<Text>().text = mString;
	}
}
