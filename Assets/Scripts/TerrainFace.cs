using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TerrainFace : MonoBehaviour
{
    ShapeGenerator shapeGenerator;
    public Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;    
    public GridTile[] tiles;
    public float[] elevations;
    public int faceIndex;
    public int tile;

    public GameObject debugSphere;
    public TMP_Text debugText;

    public void Init(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp, int tileResolution)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;        
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;
        elevations = new float[resolution*resolution];

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, out elevations[i]);                

                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();                

       // tiles = CreateGridTiles();
    }

    public GridTile[] CreateGridTiles()
    {
        int gridDimension = 3;
        int tileres = resolution-1;
        int numberOfTiles = (tileres) * (tileres);
        tiles = new GridTile[numberOfTiles];
        var tileCounter = 0;

        for (int y = 0; y < resolution; y= y+ gridDimension)
        {
            for (int x = 0; x < resolution; x= x+ gridDimension)
            {
                Debug.Log("y: " + y + " x:" + x);

                if (x != resolution-1 && y != resolution-1)
                {                    
                    int i = x + (y * resolution);

                    var vert1 = i;
                    var vert2 = i + gridDimension;
                    var vert3 = i + resolution;
                    var vert4 = i + resolution + gridDimension;
                                        
                    tiles[tileCounter] = new GridTile()
                    {
                        tileIndex = tileCounter,
                        faceIndex = this.faceIndex,
                        elevation = (elevations[vert1] + elevations[vert2] + elevations[vert3] + elevations[vert4]) / 4f,
                        verts = new Vector3[]
                        {
                             mesh.vertices[i],
                             mesh.vertices[i+gridDimension],
                             mesh.vertices[resolution+i],
                             mesh.vertices[resolution+i+gridDimension]
                        },

                        localUp = (mesh.normals[i] + mesh.normals[i + gridDimension] + mesh.normals[resolution + i] + mesh.normals[resolution + i + gridDimension]) / 4

                    };
                    tiles[tileCounter].centroid = (tiles[tileCounter].verts[0] + tiles[tileCounter].verts[1] + tiles[tileCounter].verts[2] + tiles[tileCounter].verts[3]) / 4.0f;
                    tileCounter++;
                }
            }
        }
        Debug.Log(tiles.Length);
        return tiles;
    }

    [ContextMenu("Height Debug All")]
    public void HeightDebug()
    {
        foreach (GridTile tile in tiles)
        {
            var d = Instantiate(debugSphere);
            d.transform.position = tile.centroid;
            d.transform.rotation = Quaternion.FromToRotation(Vector3.up, tile.localUp);
            //d.transform.LookAt(Vector3.zero);
          //  d.transform.position = d.transform.position + Vector3
            d.name = tile.tileIndex.ToString();
           // d.text = tile.angle.ToString("F2");
           // d.color = tile.elevation < 0.17f ? Color.green : Color.red;
            
        }
    }

    [ContextMenu("Height Debug 1")]
    public void Height1Debug()
    {
       
        ////var a = Instantiate(debugSphere);      
        ////var b = Instantiate(debugSphere);  
        ////var c = Instantiate(debugSphere);
        ////var d = Instantiate(debugSphere);
        //var e = Instantiate(debugSphere);
       
        //a.transform.position = tiles[tile].verts[0];
        //a.name = "0";
        //b.transform.position = tiles[tile].verts[1];
        //b.name = "1";
        //c.transform.position = tiles[tile].verts[2];
        //c.name = "2";
        //d.transform.position = tiles[tile].verts[3];
        //d.name = "3";
        //e.transform.position = tiles[tile].centroid;
        //e.name = "cent";


    }
}
