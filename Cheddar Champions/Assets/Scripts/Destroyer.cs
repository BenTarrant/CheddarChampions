using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        print("Removing duplicate door");
        Destroy(gameObject);
    }
}
