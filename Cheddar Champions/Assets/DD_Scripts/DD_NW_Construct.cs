using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DD_NW_Construct : NetworkBehaviour
{
    // ----------------------------------------------------------------------
    public GameObject go_block;
    public GameObject go_template_block;
    private GameObject go_template;
    public Camera cam_PC;
    private float fl_next_move_time;
    public GameObject go_spawn_point_marker;
    private GameObject go_spawn_point;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer) return;

        go_template = Instantiate(go_template_block, transform.position +
            transform.TransformDirection(Vector3.forward), transform.rotation);

        go_template.SetActive(false);

        go_spawn_point = Instantiate(go_spawn_point_marker, transform.position +
            transform.TransformDirection(Vector3.forward), transform.rotation);
    }//-----


    // ----------------------------------------------------------------------
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        PlaceObject();
    }//-----

    // ----------------------------------------------------------------------
    void PlaceObject()
    {
        if (Input.GetKeyDown(KeyCode.B))
            if (go_template.activeSelf) go_template.SetActive(false);
            else go_template.SetActive(true);


        if (go_template.activeSelf)
        {
            // Raycast, to find spawn point
            RaycastHit _rc_hit;
            Ray _ray = cam_PC.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _rc_hit))
            {
                Vector3 _spawn_point = _rc_hit.point;
                Vector3 _v3_PC_pos = transform.position;

                // Round off the position
                _spawn_point = new Vector3(Mathf.Round(_spawn_point.x),
                    Mathf.Round(_spawn_point.y), Mathf.Round(_spawn_point.z));

                // Move spawn point indicator
                go_spawn_point.transform.position = _spawn_point;
                _spawn_point += new Vector3(0, 2.5f, 0); // offset for 5m tall block

                // Rotate Spawn Template
                if (_spawn_point.x > _v3_PC_pos.x + 1 || _spawn_point.x < _v3_PC_pos.x - 1)
                    go_template.transform.eulerAngles = new Vector3(go_template.transform.eulerAngles.x, 
                        90, go_template.transform.eulerAngles.z);
                else
                    go_template.transform.eulerAngles = new Vector3(go_template.transform.eulerAngles.x, 
                        0, go_template.transform.eulerAngles.z);

                // Move the template object within limits
                if (Mathf.Abs(_spawn_point.x - _v3_PC_pos.x) < 10 && Mathf.Abs(_spawn_point.z - _v3_PC_pos.z) < 10
                              && Mathf.Abs(_spawn_point.x - _v3_PC_pos.x) > 1.5F && 
                              Mathf.Abs(_spawn_point.z - _v3_PC_pos.z) > 1.5F)
                {
                    go_template.transform.position = _spawn_point;
                }
            }

            // CheckSpace();

            if (Input.GetMouseButtonDown(1))
            {
                CmdSpawnBlock(go_template.transform.position, go_template.transform.rotation);
            }
        }

    }//-----

    // ----------------------------------------------------------------------
    [Command]
    void CmdSpawnBlock(Vector3 _v3_pos, Quaternion _q_rot)
    {
        // Create a block on the server
        var _block = Instantiate(go_block, _v3_pos, _q_rot);

        NetworkServer.Spawn(_block); // The object to spawn must be specified in the NW Manager
    }//-----  
 
}//==========

