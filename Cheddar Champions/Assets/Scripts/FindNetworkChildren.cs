using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FindNetworkChildren : NetworkBehaviour
{
    private NetworkTransformChild LeftSpawn;
    private NetworkTransformChild RightSpawn;
    private NetworkTransformChild BottomSpawn;
    private NetworkTransformChild TopSpawn;

    NetworkTransformChild[] allMyNTCs;
    Transform[] allMyTransformsChildren;

    List<Transform> allMySpawnPoints = new List<Transform>();

    public void Start()
    {
        allMyNTCs = GetComponents<NetworkTransformChild>();
        allMyTransformsChildren = transform.GetComponentsInChildren<Transform>();

        foreach (Transform myTransform in allMyTransformsChildren)
        {
            if (myTransform.name == "BottomSpawnPoint" || myTransform.name == "TopSpawnPoint" || myTransform.name == "LeftSpawnPoint" || myTransform.name == "RightSpawnPoint")
            {
                allMySpawnPoints.Add(myTransform);
            }
        }

        for (int i = 0; i < allMyNTCs.Length; i++)
        {
            if (allMyNTCs[0].target == null)
            {
                allMyNTCs[0].target = allMySpawnPoints[0];
            }

            if (allMyNTCs[1].target == null)
            {
                allMyNTCs[1].target = allMySpawnPoints[1];
            }

            if (allMyNTCs[2].target == null)
            {
                allMyNTCs[2].target = allMySpawnPoints[2];
            }

            if (allMyNTCs[3].target == null)
            {
                allMyNTCs[3].target = allMySpawnPoints[3];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (allMyNTCs[0].target == null || allMyNTCs[1].target == null || allMyNTCs[2].target == null || allMyNTCs[3].target == null)
        {
            for (int i = 0; i < allMyNTCs.Length; i++)
            {
                if (allMyNTCs[0].target == null)
                {
                    allMyNTCs[0].target = allMySpawnPoints[0];
                }

                if (allMyNTCs[1].target == null)
                {
                    allMyNTCs[1].target = allMySpawnPoints[1];
                }

                if (allMyNTCs[2].target == null)
                {
                    allMyNTCs[2].target = allMySpawnPoints[2];
                }

                if (allMyNTCs[3].target == null)
                {
                    allMyNTCs[3].target = allMySpawnPoints[3];
                }
            } 
        }
    }
}
