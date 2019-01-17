using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboarding : MonoBehaviour {


	// Update is called once per frame
	void Update ()
    {
        if(Camera.main == null)
        {
            this.enabled = false;
        }

        if (Camera.main != null)
        {
            this.transform.LookAt(Camera.main.transform.position);
            this.transform.Rotate(new Vector3(0, 180, 0));
        }

	}
}
