using System;
using UnityEngine;

public class PortraitFace : MonoBehaviour
{
    public enum FaceState { calm, sad, mad, sleep};

    public FaceState currentState;

    [SerializeField] private Head currentHead;
    [SerializeField] private Eyes currentEyes;
    [SerializeField] private Mouth currentMouth;
    [SerializeField] private Hair currentHair;

    [SerializeField] private Head[] heads;
    [SerializeField] private Eyes[] eyes;
    [SerializeField] private Mouth[] mouths;
    [SerializeField] private Hair[] hair;
    [SerializeField] private Color[] hairColor;

    [SerializeField] public SpriteRenderer eyesRenderer;
    [SerializeField] public SpriteRenderer pupilsRenderer;
    [SerializeField] private SpriteRenderer mouthsRenderer;
    [SerializeField] private SpriteRenderer hairsRenderer;

    [SerializeField] private SpriteRenderer headRenderer;

    public void Sad()
    {
        pupilsRenderer.sprite = currentEyes.pupilSprite;
        currentState = FaceState.sad;
    }

    public void Calm()
    {
        eyesRenderer.sprite = currentEyes.sprite;
        pupilsRenderer.sprite = currentEyes.pupilSprite;
        mouthsRenderer.sprite = currentMouth.sprite;

        currentState = FaceState.calm;

    }

    public void Sleep()
    {
        eyesRenderer.sprite = currentEyes.sleepSprite;
        pupilsRenderer.sprite = null;
        currentState = FaceState.sleep;

    }

    public void Mad()
    {
        eyesRenderer.sprite = currentEyes.madSprite;
        pupilsRenderer.sprite = currentEyes.pupilSprite;
        mouthsRenderer.sprite = currentMouth.madSprite;
        currentState = FaceState.mad;

    }

    public void GenerateFace()
    {
        currentHead = heads[UnityEngine.Random.Range(0, heads.Length)];
        headRenderer.sprite = currentHead.sprite;

        currentEyes = eyes[UnityEngine.Random.Range(0, eyes.Length)];
        eyesRenderer.sprite = currentEyes.sprite;
        pupilsRenderer.sprite = currentEyes.pupilSprite;

        currentMouth = mouths[UnityEngine.Random.Range(0, mouths.Length)];
        mouthsRenderer.sprite = currentMouth.sprite;

        currentHair = hair[UnityEngine.Random.Range(0, hair.Length)];
        hairsRenderer.sprite = currentHair.sprite;
        hairsRenderer.color = hairColor[UnityEngine.Random.Range(0, hairColor.Length)];

    }

    public void ChangeFace(FaceState state)
    {
        switch (state)
        {
            case FaceState.calm:
                Calm();
                break;
            case FaceState.mad:
                Mad();
                break;
            case FaceState.sad:
                Sad();
                break;
            case FaceState.sleep:
                Sleep();
                break;
        }
            
    }
}
