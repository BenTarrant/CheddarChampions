using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class DD_NW_NPC_NM : NetworkBehaviour
{
    // ----------------------------------------------------------------------
    public enum en_states { Idle, Attack, Roam, Flee };
    public en_states NPC_state = en_states.Idle;

    private Transform tx_target;
    private NavMeshAgent nm_agent;
    public int in_roam_range = 50;

    public Transform home;
    public float fl_chase_range = 2;
    public float fl_attack_range = 2;
    public float fl_flee_range = 1;

    Animator EnemyAnim;

    // Synch Position 
    [SyncVar]
    private Vector3 v3_syncPos;
    [SyncVar]
    private float fl_syncYRot;
    [SerializeField]
    private float fl_lerpRate = 10;


    // ----------------------------------------------------------------------
    void Start()
    {
        if (isServer)
        {
            nm_agent = GetComponent<NavMeshAgent>();
            EnemyAnim = GetComponent<Animator>();
            EnemyAnim.SetBool("bl_enemywalk", false);
        }

    }//-----


    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            EnemyAnim.SetBool("bl_enemywalk", true);
            SwitchStates();
         
            // Synch Position on server
            Cmd_ProvidePositionToServer(transform.position, transform.localEulerAngles.y);

        }
        else
        {
            LocalPosUpdate();
        }

        

    }//-----


    // ----------------------------------------------------------------------
    // Synch NPC positions

    [Command]
    void Cmd_ProvidePositionToServer(Vector3 pos, float rot)
    {
        v3_syncPos = pos;
        fl_syncYRot = rot;
    }//------
    // ----------------------------------------------------------------------
    void LocalPosUpdate()
    {
        // Update Local Position
        transform.position = Vector3.Lerp(transform.position, v3_syncPos, Time.deltaTime * fl_lerpRate);
        Vector3 _v3_newrot = new Vector3(0, fl_syncYRot, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_v3_newrot), Time.deltaTime * fl_lerpRate);

    }//-----




    // ----------------------------------------------------------------------
    private void SwitchStates()
    {
         

        switch (NPC_state)
        {
            case en_states.Idle:
                NPC_state = en_states.Roam;
                break;

            case en_states.Roam:
                RoamWorld();
                break;

            case en_states.Attack:
                AttackEnemy();
                break;

            case en_states.Flee:
                Flee();
                break;
        }
    }//-----

    // ----------------------------------------------------------------------
    private void AttackEnemy()
    {
        // Set Target
        nm_agent.SetDestination(tx_target.position);

        // lose the target

        if (Vector3.Distance(transform.position, tx_target.transform.position) > fl_chase_range)
        {
            NPC_state = en_states.Roam;
        }

        if (Vector3.Distance(transform.position, tx_target.transform.position) < fl_flee_range)
        {
            Flee();
        }

    }//-----

    IEnumerator Flee()
    {
        nm_agent.SetDestination(home.position);

        yield return new WaitForSeconds(3f);
        NPC_state = en_states.Roam;
    }

    // ---------------------------------------------------------------------



    // ----------------------------------------------------------------------
    private void RoamWorld()
    {
        if (!FindPlayers())
        {
            if (Vector3.Distance(transform.position, nm_agent.destination) < 4)
            {
                nm_agent.SetDestination(new Vector3(Random.Range(-in_roam_range, in_roam_range), 0, Random.Range(-in_roam_range, in_roam_range)));
            }
        }
        else
        {
            
        }
    }//-----

    // ----------------------------------------------------------------------
    bool FindPlayers()
    {
        // temp variables
        float _dist = Mathf.Infinity;
        GameObject _go_nearest_player = null;
        GameObject[] _go_Players = GameObject.FindGameObjectsWithTag("Player");

        // Are there any tagged targets in the scene?
        if (_go_Players.Length > 0)
        {
            // Loop through the list of targets
            foreach (GameObject _go in _go_Players)
            {
                float _cur_dist = Vector3.Distance(_go.transform.position, transform.position);
                if (_cur_dist < _dist)
                {
                    _go_nearest_player = _go;
                    _dist = _cur_dist;
                }
            }

            if (Vector3.Distance(transform.position, _go_nearest_player.transform.position) < fl_chase_range)
            {
                // Set the Target
                tx_target = _go_nearest_player.transform;
                NPC_state = en_states.Attack;
                return true;
            }
        }

        return false;
    }//----- 

}//========

