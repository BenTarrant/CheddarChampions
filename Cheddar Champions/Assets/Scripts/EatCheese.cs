using UnityEngine;
using System.Collections;

public class EatCheese : MonoBehaviour
{

    public float Health = 5f;
    public GameObject CheeseExplosion;

    public void BeingEaten()
    {
        Health -= 1f;

        if (Health <= 0) // if the health drops below 0
        {
            Instantiate(CheeseExplosion, transform.position, transform.rotation); // create explosion as reference in GM
            Destroy(gameObject); // destory the enemy
        }
    }



}
