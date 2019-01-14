using UnityEngine;
using System.Collections;

public class EatCheese : MonoBehaviour
{

    public float Health = 5f;
    public GameObject CheeseExplosion;

    public void BeingEaten(int _damage_amount)
    {
        Health -= _damage_amount;
        GameManager.sGM.score++;

        if (Health <= 0) // if the health drops below 0
        {
            Instantiate(CheeseExplosion, transform.position, Quaternion.identity); // create explosion as reference in GM
            Destroy(gameObject); // destory the enemy
        }
    }



}
