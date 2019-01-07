using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    public bool isLerping = false;

    [Header("Slicing Materials")]
    public Material slicingMaterial;
    public Material slicingMaterialVanguard;
    public Material slicingMaterialRifle;

    [Header("Colour Slider")]
    public Color slicingColour;

    [Header("Outline Material")]
    public Material outlineMaterial;

    [Header("Colour Slider")]
    public Color outlineColour;

    [Header("Outline Width")]
    [Range(0, 10)] public float outlineWidth = 0.05f;

    [Header("Slicing Type")]
    [Range(0, 7)] public int sliceType = 7;
    [Range(0, 7)] public int sliceTypeVanguard = 7;
    [Range(0, 7)] public int sliceTypeRifle = 7;

    [Header("Slicing Angle")]
    [Range(0, 360)] public int sliceAngle;
    [Range(0, 360)] public int sliceAngleVanguard;
    [Range(0, 360)] public int sliceAngleRifle;

    [Header("Slicing Width")]
    [Range(0, 1)] public float sliceWidth = 0.5f;
    [Range(0, 1)] public float sliceWidthVanguard = 0.5f;
    [Range(0, 1)] public float sliceWidthRifle = 0.5f;

    [Header("Number of Slices")]
    [Range(0, 200)] public int noOfSlices = 10; 
    [Range(0, 200)] public int noOfSlicesVanguard = 10;
    [Range(0, 200)] public int noOfSlicesRifle = 10;

    void Start()
    {
        slicingMaterial.SetColor("_Color", Color.yellow);
        outlineColour = outlineMaterial.GetColor("_OutlineColor");
    }

    // Update is called once per frame
    void Update()
    {
        if (isLerping)
        {
            float lerp = Mathf.PingPong(Time.time, 1) / 1;

            outlineMaterial.SetColor("_OutlineColor", Color.Lerp(Color.yellow, new Color(lerp, 1 - lerp, lerp / 2, 1), Mathf.PingPong(Time.time, 1)));
            slicingMaterial.SetColor("_Color", Color.Lerp(Color.white, new Color(lerp, 1 - lerp, lerp / 2, 1), Mathf.PingPong(Time.time, 1)));
        }

        else
        {
            outlineMaterial.SetColor("_OutlineColor", outlineColour);
            slicingMaterial.SetColor("_Color", slicingColour);
        }

        UpdateSlicing();
        UpdateOutline();
    }

    void UpdateSlicing()
    {
        slicingMaterial.SetInt("_SliceType", sliceType);
        slicingMaterialVanguard.SetInt("_SliceType", sliceTypeVanguard);
        slicingMaterialRifle.SetInt("_SliceType", sliceTypeRifle);

        slicingMaterial.SetFloat("_SliceAngle", sliceAngle);
        slicingMaterialVanguard.SetFloat("_SliceAngle", sliceTypeVanguard);
        slicingMaterialRifle.SetFloat("_SliceAngle", sliceTypeRifle);

        slicingMaterial.SetFloat("_SlicingWidth", sliceWidth);
        slicingMaterialVanguard.SetFloat("_SlicingWidth", sliceWidthVanguard);
        slicingMaterialRifle.SetFloat("_SlicingWidth", sliceWidthRifle);

        slicingMaterial.SetInt("_NoSlices", noOfSlices);
        slicingMaterialVanguard.SetInt("_NoSlices", noOfSlicesVanguard);
        slicingMaterialRifle.SetInt("_NoSlices", noOfSlicesRifle);
    }

    void UpdateOutline()
    {
        outlineMaterial.SetFloat("_Outline", outlineWidth);
    }
}