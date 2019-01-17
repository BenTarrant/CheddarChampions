using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheeseProjectile : NetworkBehaviour {

    public float range = 10;
    public float speed = 20;
    public float damage = 1;
    private Rigidbody rb_bullet;

    [SyncVar]
    public NetworkInstanceId Shooter;

    // Use this for initialization
    void Start()
    {

        Destroy(gameObject, range / speed);

        rb_bullet = GetComponent<Rigidbody>();
        rb_bullet.velocity = speed * transform.TransformDirection(Vector3.forward);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cheese")
        {
            print("hit cheese");
            other.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
}

