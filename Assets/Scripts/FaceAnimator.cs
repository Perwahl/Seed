using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimator : MonoBehaviour
{

    public PortraitFace face;

    private void Start()
    {
        StartCoroutine(BlinkLoop());
        StartCoroutine(GlanceLoop());

    }

    private IEnumerator BlinkLoop()
    {        
        PortraitFace.FaceState previousFace;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 10f));
            previousFace = face.currentState;
            face.ChangeFace(PortraitFace.FaceState.sleep);
            yield return new WaitForSeconds(0.2f);
            face.ChangeFace(previousFace);

        }
    }

    private IEnumerator GlanceLoop()
    {        
        while (true)
        {
            var direction = Random.value < 0.5f ? -1 : 1;
            yield return new WaitForSeconds(Random.Range(3f, 15f));
            face.pupilsRenderer.transform.position = face.pupilsRenderer.transform.position + (transform.right*0.1f * direction);
            yield return new WaitForSeconds(Random.Range(0.2f, 3f));
            face.pupilsRenderer.transform.position = face.pupilsRenderer.transform.position + (transform.right * -0.1f*direction);

        }
    }

}
