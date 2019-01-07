using UnityEngine;

public class Extrude : MonoBehaviour
{
    public Material material;

    [Header("Max Extrusion Amount")]
    [Range(0, 0.1f)]public float maxExtrusionAmount = 0.05f;

    [Header("Extrusion Speed")]
    [Range(0, 2)]public float extrusionSpeed = 0.5f;

    [Header("Ping Pong")]
    public bool extrusionPingPong = false;

    // Update is called once per frame
    void Update()
    {
        Extruding();
    }

    void Extruding()
    {
        if (extrusionPingPong)
        {
            material.SetFloat("_Amount", Mathf.PingPong(Time.time * extrusionSpeed / 10, maxExtrusionAmount));
        }

        else
        {
            material.SetFloat("_Amount", maxExtrusionAmount);
        }
    }
}
