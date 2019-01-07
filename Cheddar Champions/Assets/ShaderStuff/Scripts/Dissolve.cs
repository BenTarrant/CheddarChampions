using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material[] materials;

    [Header("Dissolve Type")]
    [Range(0, 7)] public int dissolveType = 1;

    [Header("Amount of Slices")]
    [Range(0, 250)] public int dissolveSlices = 60;

    [Header("Dissolve Speed")]
    [Range(0, 2)] public float dissolveSpeed = 0.5f;

    [Header("Outline Colour")]
    public Color outlineColour;

    [Header("Outline Max Width")]
    [Range(0, 0.1f)] public float outlineMaxWidth = 0.1f;

    [Header("Ping Pong")]
    public bool outlinePingPong = false;

    // Update is called once per frame
    void Update()
    {
        DissolveCharacter();
    }

    void DissolveCharacter()
    {
        // Set slicing type to 7 (Grid).
        materials[1].SetInt("_SliceType", dissolveType);
        materials[1].SetInt("_NoSlices", dissolveSlices);
        materials[1].SetFloat("_SlicingWidth", Mathf.PingPong(Time.time * dissolveSpeed, 1));

        if (outlinePingPong)
        {
            materials[0].SetColor("_OutlineColor", outlineColour);
            materials[0].SetFloat("_Outline", Mathf.PingPong(Time.time * dissolveSpeed / 10, outlineMaxWidth));
        }

        else
        {
            materials[0].SetColor("_OutlineColor", outlineColour);
            materials[0].SetFloat("_Outline", 0.05f);
        }
    }
}

//https://docs.unity3d.com/Manual/SL-SurfaceShaderExamples.html
//http://wiki.unity3d.com/index.php/Silhouette-Outlined_Diffuse