using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EatCheese : NetworkBehaviour
{
    public float Health = 5f;
    public GameObject CheeseExplosion;
    public MousePlayerController PC;

    public void Start()
    {

    }

    public void BeingEaten(int _damage_amount)
    {
        Health -= _damage_amount;
        GameManager.sGM.score++;

        if (Health <= 0) // if the health drops below 0
        {
            Instantiate(CheeseExplosion, transform.position, Quaternion.identity); // create explosion
            transform.position = new Vector3(0, -15, 0);

            NetworkServer.Destroy(gameObject);
        }
    }

    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();


    }
}
