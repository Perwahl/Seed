// This script draws a debug line around mesh triangles
// as you move the mouse over them.
using UnityEngine;

public class TriangleHighlight : MonoBehaviour
{
    public PlanetBehavior planet;
    public GameObject debugSpherePrefab;
    public GameObject square;
    public GameObject[] debugSpheres;
    Vector3 lastMouseCoordinate = Vector3.zero;

    public GridTarget target;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        debugSpheres = new GameObject[16];
        for (int i = 0; i < 16; i++)
        {
            debugSpheres[i] = Instantiate(debugSpherePrefab);
        }
    }

    void Update()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;

        if (Mathf.Abs(mouseDelta.magnitude) > 1)
        {
            RaycastHit hit;
            if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                return;

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            Transform hitTransform = hit.collider.transform;
            int vert = triangles[hit.triangleIndex * 3];
            int face = hit.collider.GetComponent<TerrainFace>().faceIndex;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    var i = x + (y * 4);

                    int vert1 = vert + x + (y * planet.terrainResolution);
                    Vector3 pos = vertices[vert1];
                    var p1 = hitTransform.TransformPoint(pos);
                    debugSpheres[i].transform.position = p1;
                    Color col = planet.terrainFaces[face].elevations[vert1] > 8.15f ? Color.red : Color.green;
                    debugSpheres[i].GetComponent<Renderer>().material.color = col;
                    // Debug.Log("face: " + face + " vert:" + vert1 + " elevation: " + planet.terrainFaces[face].elevations[vert1]);

                }
            }
        }
        lastMouseCoordinate = Input.mousePosition;

        if (Input.GetMouseButtonUp(0) )
        {
            RaycastHit hit;
            if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                return;

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            Transform hitTransform = hit.collider.transform;
            int vert = triangles[hit.triangleIndex * 3];
            int face = hit.collider.GetComponent<TerrainFace>().faceIndex;

            Vector3 pos = vertices[vert];
            var p1 = hitTransform.TransformPoint(pos);

            var building = Instantiate(square);
            building.transform.position = p1;
        }

    }
}