using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform playerTransform;


   void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}

