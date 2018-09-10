using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace : MonoBehaviour
{
    ShapeGenerator shapeGenerator;
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;
    public GridTile[] tiles;
    public float[] elevations;
    public int faceIndex;
    public int tile;

    public GameObject debugSphere;

    public void Init(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
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
        elevations = new float[resolution * resolution];

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);
                elevations[i] = shapeGenerator.PointElevation(pointOnUnitSphere);

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

        tiles = CreateGridTiles();

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    public GridTile[] CreateGridTiles()
    {
        int tileres = resolution-1;
        int numberOfTiles = (tileres) * (tileres);
        tiles = new GridTile[numberOfTiles];
        var tileCounter = 0;

        for (int y = 0; y < resolution; y++)
        {
            Debug.Log("y=" + y);
            for (int x = 0; x < resolution; x++)
            {
                Debug.Log("x=" + x);

                if (x != resolution-1 && y != resolution-1)
                {
                    //var el0 = elevations[i];
                    //var el1 = elevations[i + 1];
                    //var el2 = elevations[i + resolution];
                    //var el3 = elevations[i + (resolution + 1)];
                    int i = x + (y * resolution);
                    Debug.Log(tileCounter);
                    tiles[tileCounter] = new GridTile()
                    {
                        tileIndex = tileCounter,
                        faceIndex = this.faceIndex,
                        //elevation = (el0 + el1 + el2 + el3) / 4,
                        verts = new Vector3[]
                        {
                             mesh.vertices[i],
                             mesh.vertices[i+1],
                             mesh.vertices[resolution+i],
                             mesh.vertices[resolution+i+1]
                        },
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
            d.name = tile.tileIndex.ToString();
        }
    }

    [ContextMenu("Height Debug 1")]
    public void Height1Debug()
    {
       
        var a = Instantiate(debugSphere);      
        var b = Instantiate(debugSphere);  
        var c = Instantiate(debugSphere);
        var d = Instantiate(debugSphere);
        var e = Instantiate(debugSphere);
       
        a.transform.position = tiles[tile].verts[0];
        a.name = "0";
        b.transform.position = tiles[tile].verts[1];
        b.name = "1";
        c.transform.position = tiles[tile].verts[2];
        c.name = "2";
        d.transform.position = tiles[tile].verts[3];
        d.name = "3";
        e.transform.position = tiles[tile].centroid;
        e.name = "cent";


    }
}
