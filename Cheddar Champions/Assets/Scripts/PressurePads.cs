using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PressurePads : MonoBehaviour
{
    bool m_Started; // for gizmo drawing
    public LayerMask m_LayerMask; // for collision layering
    public int miceNeeded;
    // 1 = needs a ONE PLAYER
    // 2 = needs a TWO PLAYERS
    // 3 = needs a THREE PLAYERS

    private bool DespawnedDoor;
    public Button buttonSqueak;

    ParticleSystem[] particles;

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
        //buttonSqueak.enabled = false;
        particles = GetComponentsInChildren<ParticleSystem>();

    }

    void FixedUpdate()
    {
        StartCoroutine("MyCollisions");
    }

    IEnumerator MyCollisions()
    {

        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        int i = 0;

        if (i == 0)
        {
            ReturnParticles();
        }

        //Check when there is a new collider coming into contact with the box
        if (i < hitColliders.Length)
        {
            //print("length is: " + hitColliders.Length);
            //Increase the number of Colliders in the array
            i++;
            MeshCollider col = gameObject.GetComponentInParent<MeshCollider>();
            

            if (hitColliders.Length == 1 && miceNeeded == 1)
            {
                print("Deactivating Barrier");
                DespawnedDoor = true;
                Destroy(transform.parent.gameObject);
            }

           else if (hitColliders.Length == 2 && miceNeeded == 2)
            {
                print("Deactivating Barrier");
                DespawnedDoor = true;
                GreenParticles();
                Destroy(transform.parent.gameObject);
            }

            else if (hitColliders.Length == 1 && miceNeeded == 2)
            {
                print("Need more mice!");
                RedParticles();
                //buttonSqueak.enabled = true;
            }

            else if (hitColliders.Length == 3 && miceNeeded == 3)
            {
                print("Deactivating Barrier");
                DespawnedDoor = true;
                Destroy(transform.parent.gameObject);
            }

            else if (hitColliders.Length == 2 && miceNeeded == 3)
            {
                print("Need more mice!");
                //buttonSqueak.enabled = true;
            }

            else if (hitColliders.Length == 1 && miceNeeded == 3)
            {
                print("Need more mice!");
                //buttonSqueak.enabled = true;
            }

            //buttonSqueak.enabled = false;
            //ReturnParticles();
            yield return new WaitForSeconds(10f);



        }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    public void RedParticles()
    {
        foreach(ParticleSystem childPS in particles)
        {
            ParticleSystem.MainModule Childmain = childPS.main;
            Childmain.startColor = Color.red;
        }

    }

    public void GreenParticles()
    {
        foreach (ParticleSystem childPS in particles)
        {
            ParticleSystem.MainModule Childmain = childPS.main;
            Childmain.startColor = Color.green;
        }
    }

    public void ProximityParticles()
    {
        //particles.Stop();
        //particles.Play();
    }

    public void ReturnParticles()
    {
        foreach (ParticleSystem childPS in particles)
        {
            ParticleSystem.MainModule Childmain = childPS.main;
            Childmain.startColor = Color.white;
        }
    }
}
