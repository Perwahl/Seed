using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [Range(2, 256)]
    public int terrainResolution = 10;
    public int tileResolution = 10;
        
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    public TerrainFace[] terrainFaces;

    private void Start()
    {
        Initialize();
        GenerateColours();

    }

    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colourSettings);        

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        // terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
                terrainFaces[i] = meshObj.AddComponent<TerrainFace>();
                meshObj.AddComponent<MeshCollider>().sharedMesh = terrainFaces[i].mesh;

                terrainFaces[i].faceIndex = i;
               
            }

            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;

            // terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            terrainFaces[i].Init(shapeGenerator, meshFilters[i].sharedMesh, terrainResolution, directions[i], tileResolution);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
            
            

        }
    }

    [ContextMenu("One Planet")]
    public void CombineAndRuleThemAll()
    {
        List<int> triangles = new List<int>();
        List<Vector3> vertices = new List<Vector3>();

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[terrainFaces.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.active = false;
            i++;
        }

        GameObject meshObj = new GameObject("planet");

        meshObj.AddComponent<MeshRenderer>();
        meshObj.AddComponent<MeshCollider>();
        meshObj.AddComponent<MeshFilter>();
        meshObj.GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;

        Mesh mesh= new Mesh();
        mesh.CombineMeshes(combine);
        mesh.RecalculateNormals();
        
        meshObj.GetComponent<MeshFilter>().mesh = mesh;
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colorGenerator.UpdateColors();
    }
}
