using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseProjectile : MonoBehaviour {

    public float range = 10;
    public float speed = 20;
    public float damage = 1;
    private Rigidbody rb_bullet;

	// Use this for initialization
	void Start () {

        Destroy(gameObject, range / speed);

        rb_bullet = GetComponent<Rigidbody>();
        rb_bullet.velocity = speed * transform.TransformDirection(Vector3.forward);
		
	}

    void OnTriggerEnter(Collider other)
    {

        print("hit cheese");
        other.gameObject.SendMessage("BeingEaten", damage, SendMessageOptions.DontRequireReceiver);

        Destroy(gameObject);
    }
}
