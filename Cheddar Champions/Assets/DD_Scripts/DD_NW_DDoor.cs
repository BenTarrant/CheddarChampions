using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_NW_DDoor : MonoBehaviour
{

    // ----------------------------------------------------------------------
    public string st_anim = "in_state";
    private Animator animator_attached;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        animator_attached = GetComponentInParent<Animator>();
    }//------

    // ----------------------------------------------------------------------
    public void Damage(float fl_damage)
    {
        if (animator_attached.GetInteger(st_anim) != 1)
            animator_attached.SetInteger(st_anim, 1);
        else
            animator_attached.SetInteger(st_anim, 2);
    }//-----

}//==========