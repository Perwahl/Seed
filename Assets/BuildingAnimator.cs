using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAnimator : MonoBehaviour {

    public Transform[] rotate;
    
    // Update is called once per frame
    void Update () {
        
        foreach (Transform transform in rotate)
        {
            transform.Rotate(Vector3.up, 0.5f);
        }

        

    }
}
