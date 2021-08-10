using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperiorArrays;
public class LightMaskCamera : MonoBehaviour
{

    [SerializeField]
    private int alphaLayer;

    [SerializeField]
    private LayerMask alphaLayerMask;

    //Condense alpha layer with custom editor

    [SerializeField]
    private LayerMask litMasks;

    [SerializeField]
    private LayerMask unlitMasks;

    private LightRaySystem lightMaster;

    public GameObject[] lightSources;

    [SerializeField]
    private GameObject camFab;

    [SerializeField]
    private Material whiteAlpha;

    private GameObject[] masks = new GameObject[0];
    private Mesh[] maskMeshs = new Mesh[0];

    private RenderTexture[] renders = new RenderTexture[0];

    [SerializeField]
    private Material renderAlphaShader;

    // Start is called before the first frame update
    void Start()
    {

       if(GetComponent<Camera>().orthographic == true)
        {
            InitiateStartup();
        }
        else
        {
            Debug.LogError("Light Mask System requires attatched camera to be set to orthographic");
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lightSources.Length; i++)
        {
            lightMaster.CycleLights(lightSources[i],maskMeshs[i] );
        }
        
    }

    private void InitiateStartup()
    {
        CreateCameras();
        SetupRaySystem();
        CreateMasks();
        CreateRender();
        GetComponent<LightRaySystem>().collisionObjects = litMasks | unlitMasks;
    }


    private void CreateCameras()
    {
        GameObject maskCam = Instantiate(camFab);
        maskCam.name = "SpriteMaskCamera";
        maskCam.transform.parent = transform;
        maskCam.transform.localPosition = new Vector3(0, 0, 0);
        maskCam.transform.eulerAngles = new Vector3(0, 0, 0);
        maskCam.GetComponent<Camera>().orthographic = true;
        maskCam.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        maskCam.GetComponent<Camera>().backgroundColor = new Color(0, 0, 0);
        maskCam.GetComponent<Camera>().targetTexture = CreateRenderTextures();
        maskCam.GetComponent<Camera>().cullingMask = alphaLayerMask;

        GameObject litWorldCam = Instantiate(camFab);
        litWorldCam.name = "LitWorldCamera";
        litWorldCam.transform.parent = transform;
        litWorldCam.transform.localPosition = new Vector3(0, 0, 0);
        litWorldCam.transform.eulerAngles = new Vector3(0, 0, 0);
        litWorldCam.GetComponent<Camera>().orthographic = true;
        litWorldCam.GetComponent<Camera>().targetTexture = CreateRenderTextures();
        litWorldCam.GetComponent<Camera>().cullingMask = litMasks;

        GameObject unlitWorldCam = Instantiate(camFab);
        unlitWorldCam.name = "UnlitWorldCamera";
        unlitWorldCam.transform.parent = transform;
        unlitWorldCam.transform.localPosition = new Vector3(0, 0, 0);
        unlitWorldCam.transform.eulerAngles = new Vector3(0, 0, 0);
        unlitWorldCam.GetComponent<Camera>().orthographic = true;
        unlitWorldCam.GetComponent<Camera>().targetTexture = CreateRenderTextures();
        unlitWorldCam.GetComponent<Camera>().cullingMask = unlitMasks;
    }

    private RenderTexture CreateRenderTextures()
    {
        RenderTexture texture = new RenderTexture(1920,1080,1);
        renders = Sray.AppendArray(renders, texture);
        return texture;
    }

    private void SetupRaySystem()
    {
        lightMaster = gameObject.AddComponent<LightRaySystem>();
        

    }
    

    private void CreateMasks()
    {

        GameObject maskLibrary = new GameObject();
        maskLibrary.name = "MaskLibrary";

        for (int i = 0; i < lightSources.Length; i++)
        {
            GameObject mask = new GameObject();
            mask.name = "(" + lightSources[i].name + ")_Mask";
            mask.transform.parent = maskLibrary.transform;
            mask.AddComponent<MeshRenderer>();

            mask.GetComponent<MeshRenderer>().material = whiteAlpha;

            mask.AddComponent<MeshFilter>();
            masks = Sray.AppendArray(masks, mask);

            mask.gameObject.layer = alphaLayer;

            print(mask.layer);

            Mesh newMesh = mask.GetComponent<MeshFilter>().mesh = new Mesh();
            maskMeshs = Sray.AppendArray(maskMeshs, newMesh);
            newMesh.name = "(" + lightSources[i].name + ")_Mesh";

        }


    }

    private Material SetTextures(Material source)
    {

        source.SetTexture("Mask", renders[0]);
        source.SetTexture("FrontWorld", renders[1]);
        source.SetTexture("BackWorld", renders[2]);


        return source;
    }

    private void CreateRender()
    {

        GameObject renderVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        renderVisual.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
        renderVisual.transform.localScale = new Vector3(18, 10, 1);
        renderVisual.transform.eulerAngles = new Vector3(180,180 , 0);
        renderVisual.transform.parent = transform;

        renderVisual.GetComponent<MeshRenderer>().material = SetTextures(renderAlphaShader);

    }


}

