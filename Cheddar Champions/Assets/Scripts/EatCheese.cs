using UnityEngine;
using System.Collections;

public class EatCheese : MonoBehaviour
{


    public void EatingCheese()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }




}
