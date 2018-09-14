// This script draws a debug line around mesh triangles
// as you move the mouse over them.
using UnityEngine;
using System.Collections;

public class TriangleHighlight : MonoBehaviour
{
    public Planet planet;
    public GameObject debugSphere;
    public GameObject debugSphere1;
    public GameObject debugSphere2;
    public GridTarget target;        
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();        
    }

    void Update()
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
        //Debug.Log(hit.triangleIndex/2);
        //Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
        //Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
        //Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
        Transform hitTransform = hit.collider.transform;
        //p0 = hitTransform.TransformPoint(p0);
        //p1 = hitTransform.TransformPoint(p1);
        //p2 = hitTransform.TransformPoint(p2);
        //Debug.DrawLine(p0, p1, Color.red, Time.deltaTime, false);
        //Debug.DrawLine(p1, p2);
        //Debug.DrawLine(p2, p0);

        //debugSphere.transform.position = p0;
        //debugSphere1.transform.position = p1;
        //debugSphere2.transform.position = p2;
        var face = hit.collider.gameObject.GetComponent<TerrainFace>().faceIndex;

        var tile = planet.terrainFaces[face].tiles[hit.triangleIndex / 2];

        target.transform.position = tile.centroid;
        target.transform.rotation = Quaternion.FromToRotation(Vector3.up, tile.localUp);


        //var elevation = tile.elevation;


        Debug.Log("tile index: " + tile.tileIndex + " - elevation: ");
        //Debug.Log("triangle index: " + hit.triangleIndex);

    }



}