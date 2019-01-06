using UnityEngine;
using System.Collections;

public class PressurePads : MonoBehaviour
{
    void caster(Vector2 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "CertainTag")
            {
                print("one player");
            }
            i++;
        }
    }
}