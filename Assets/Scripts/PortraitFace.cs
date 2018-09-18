using System;
using UnityEngine;

public class PortraitFace : MonoBehaviour
{
    [SerializeField] private Head currentHead;
    [SerializeField] private Eyes currentEyes;
    [SerializeField] private Mouth currentMouth;
    [SerializeField] private Hair currentHair;

    [SerializeField] private Head[] heads;
    [SerializeField] private Eyes[] eyes;
    [SerializeField] private Mouth[] mouths;
    [SerializeField] private Hair[] hair;
    [SerializeField] private Color[] hairColor;

    [SerializeField] private SpriteRenderer eyesRenderer;
    [SerializeField] private SpriteRenderer pupilsRenderer;
    [SerializeField] private SpriteRenderer mouthsRenderer;
    [SerializeField] private SpriteRenderer hairsRenderer;

    [SerializeField] private SpriteRenderer headRenderer;

    public void Sad()
    {
        pupilsRenderer.sprite = currentEyes.pupilSprite;

    }

    public void Calm()
    {
        eyesRenderer.sprite = currentEyes.sprite;
        pupilsRenderer.sprite = currentEyes.pupilSprite;


    }

    public void Sleep()
    {
        eyesRenderer.sprite = currentEyes.sleepSprite;
        pupilsRenderer.sprite = null;

    }

    public void Mad()
    {
        eyesRenderer.sprite = currentEyes.madSprite;
        pupilsRenderer.sprite = currentEyes.pupilSprite;
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
}
