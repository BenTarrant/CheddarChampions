using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    public int typeOfTrap;

    // 1 = CUTTER
    // 2 = FIRE
    // 3 = NEEDLE


    void OnCollisionEnter(Collision col)
    {
        print("collsion");

        if (col.gameObject.CompareTag("Player"))
        {
            if(typeOfTrap == 1)
            {
                print("CUTTER");
            }

            if (typeOfTrap == 2)
            {
                print("FIRE");
            }
        }

        if (typeOfTrap == 3)
        {
            print("NEEDLE");
        }
    }
}
