using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimator : MonoBehaviour
{

    public PortraitFace face;

    private void Start()
    {
        StartCoroutine(BlinkLoop());
    }

    private IEnumerator BlinkLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 5f));
            face.Sleep();
            yield return new WaitForSeconds(0.2f);
            face.Calm();
        }
    }

}
