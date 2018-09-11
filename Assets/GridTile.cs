using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GridTile
{
    public int faceIndex; 
    public float elevation;    
    public int tileIndex;
    public Vector3 centroid;
    public Vector3[] verts;
    public Vector3 localUp;
}
