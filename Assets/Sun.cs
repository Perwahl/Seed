using UnityEngine;

public class Sun : MonoBehaviour
{

    public Planet planet;


    void Update()
    {

        transform.Rotate(transform.up, 0.01f);

    }
}
