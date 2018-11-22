using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    //public int depth = -20;

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

