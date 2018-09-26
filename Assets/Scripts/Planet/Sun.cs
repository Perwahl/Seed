using UnityEngine;

public class Sun : MonoBehaviour
{

    public PlanetBehavior planet;


    void Update()
    {

        transform.Rotate(transform.up, 0.01f);

    }
}
