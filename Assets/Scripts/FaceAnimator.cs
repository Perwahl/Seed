using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimator : MonoBehaviour
{

    public CharacterPortrait face;
    private Coroutine blinkLoop;
    private Coroutine glanceLoop;

    public void Animate()
    {
        if(blinkLoop != null) StopCoroutine(blinkLoop);
        if (glanceLoop != null) StopCoroutine(glanceLoop);
      
        blinkLoop = StartCoroutine(BlinkLoop());
        glanceLoop = StartCoroutine(GlanceLoop());

    }

    private IEnumerator BlinkLoop()
    {
        CharacterPortrait.FaceState previousFace;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 10f));
            previousFace = face.currentState;
            face.ChangeFace(CharacterPortrait.FaceState.sleep);
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
            face.portrait.pupilsRenderer.transform.position = face.portrait.pupilsRenderer.transform.position + (transform.right*0.1f * direction);
            yield return new WaitForSeconds(Random.Range(0.2f, 3f));
            face.portrait.pupilsRenderer.transform.position = face.portrait.pupilsRenderer.transform.position + (transform.right * -0.1f*direction);

        }
    }

}
