using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EatCheese : NetworkBehaviour
{
    public float Health = 5f;
    public GameObject CheeseExplosion;
    bool m_Started;

    public NetworkIdentity[] Players;
    public GameObject Client;
    public GameObject Host;

    public void Start()
    {
        m_Started = true;
    }

    //void FixedUpdate()
    //{
    //    StartCoroutine("CheeseCol");
    //}

    public void Update()
    {
        if (Players.Length == 0 || Players == null)
        {
            Players = FindObjectsOfType<NetworkIdentity>();

            foreach (NetworkIdentity item in Players)
            {
                // Check if There is a MousePlayerController Script Attached to the Go -------------------------------------------------------------
                if (item.isServer)
                {
                    Host = item.gameObject;
                }

                else
                {
                    Client = item.gameObject;
                }
            }
        }
    }

    public void BeingEaten(int _damage_amount)
    {
        Health -= _damage_amount;
        GameManager.sGM.score++;

        if (Health <= 0) // if the health drops below 0
        {
            Instantiate(CheeseExplosion, transform.position, Quaternion.identity); // create explosion

            if (!isServer)
            {
                Client.GetComponent<MousePlayerController>().CmdDeleteFromScene(gameObject);
                print("Imma on Client");
            }

            else
            {
                Destroy(gameObject);
                print("Imma on Server");
            }

            // destory self
        }
    }

    //IEnumerator CheeseCol()
    //{
    //    Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);
    //    int i = 0;
    //    while (i < hitColliders.Length)
    //    {
    //        hitColliders[i].SendMessage("InZone");
    //        i++;
    //    }

    //    yield return new WaitForSeconds(10f);
    //}

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
    //    if (m_Started)
    //        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
    //        Gizmos.DrawWireCube(transform.position, transform.localScale);
    //}
}
