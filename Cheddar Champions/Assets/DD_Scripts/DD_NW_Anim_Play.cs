using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DD_NW_Anim_Play : MonoBehaviour
{
    // ----------------------------------------------------------------------
    public string st_anim = "bl_open";
    private Animator animator_attached;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        animator_attached = GetComponent<Animator>();
    }//------

    // ----------------------------------------------------------------------
    public void Damage(float fl_damage)
    {
        animator_attached.SetBool(st_anim, true);
    }//-----

}//==========
