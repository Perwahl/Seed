using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPortrait : MonoBehaviour
{
    public enum CharacterType { male, female, robot };
    public CharacterType type;

    public enum FaceState { calm, sad, mad, sleep };
    public FaceState currentState = FaceState.calm;

    [SerializeField] private Color[] backgroundColors;
    [SerializeField] private SpriteRenderer backgroundRenderer;


    public PortraitBase portrait;
    [SerializeField] private PortraitBase[] portraitBases;

    [SerializeField] private Glasses currentGlasses;
    [SerializeField] private Eyes currentEyes;
    [SerializeField] private Mouth currentMouth;
    [SerializeField] private Hair currentHair;
    [SerializeField] private FullFaceDetail currentFullFaceDetail;
    [SerializeField] private FacialHair currentFacialHair;

    [SerializeField] private Eyes[] eyes;
    [SerializeField] private Mouth[] mouths;
    [SerializeField] private Hair[] hair;
    [SerializeField] private Glasses[] glasses;
    [SerializeField] private FullFaceDetail[] fullFaceDetails;
    [SerializeField] private FacialHair[] facialHair;
    [SerializeField] private Color[] hairColor;
    [SerializeField] private Color currentHairColor;

    [SerializeField] private FaceAnimator animator;



    public void Sad()
    {
        portrait.pupilsRenderer.sprite = currentEyes.pupilSprite;
        currentState = FaceState.sad;
    }

    public void Calm()
    {
        portrait.eyesRenderer.sprite = currentEyes.sprite;
        portrait.pupilsRenderer.sprite = currentEyes.pupilSprite;
        portrait.mouthsRenderer.sprite = currentMouth.sprite;

        currentState = FaceState.calm;

    }

    public void Sleep()
    {
        portrait.eyesRenderer.sprite = currentEyes.sleepSprite;
        portrait.pupilsRenderer.sprite = null;
        currentState = FaceState.sleep;

    }

    public void Mad()
    {
        portrait.eyesRenderer.sprite = currentEyes.madSprite;
        portrait.pupilsRenderer.sprite = currentEyes.pupilSprite;
        portrait.mouthsRenderer.sprite = currentMouth.madSprite;
        currentState = FaceState.mad;

    }

    public void GenerateFace()
    {

        if (portrait != null)
        {
            Destroy(portrait.gameObject);
        }
        animator.face = this;
        animator.Animate();

        backgroundRenderer.color = backgroundColors[UnityEngine.Random.Range(0, backgroundColors.Length)];

        PortraitBase[] p = Array.FindAll(portraitBases, element => element.type == type);
        portrait = Instantiate(p[UnityEngine.Random.Range(0, p.Length)], this.transform, false);

        currentEyes = (Eyes)GetRandomComponent(eyes, type);
        portrait.eyesRenderer.sprite = currentEyes.sprite;
        portrait.pupilsRenderer.sprite = currentEyes.pupilSprite;

        currentMouth = (Mouth)GetRandomComponent(mouths, type);
        portrait.mouthsRenderer.sprite = currentMouth.sprite;


        currentHair = (Hair)GetRandomComponent(hair, type);
        portrait.hairsRenderer.sprite = currentHair.sprite;
        currentHairColor = hairColor[UnityEngine.Random.Range(0, hairColor.Length)];
        portrait.hairsRenderer.color = currentHairColor;

        var faceColor = portrait.faceRenderer.color;

        faceColor = Color.Lerp(faceColor, Color.black, UnityEngine.Random.Range(0.0f, 0.4f));
        faceColor = Color.Lerp(faceColor, Color.red, UnityEngine.Random.Range(0.0f, 0.2f));
        faceColor = Color.Lerp(faceColor, Color.yellow, UnityEngine.Random.Range(0.0f, 0.2f));

        portrait.faceRenderer.color = faceColor;
        currentState = FaceState.calm;

        OptionalItems();

    }

    private void OptionalItems()
    {
        if (UnityEngine.Random.value > 0.7F)
        {
            currentGlasses = (Glasses)GetRandomComponent(glasses, type);
            portrait.glassesRenderer.sprite = currentGlasses.sprite;
        }
        else
        {
            portrait.glassesRenderer.gameObject.SetActive(false);

        }

        if (UnityEngine.Random.value > 0.7F && type == CharacterType.male)
        {
            currentFacialHair = (FacialHair)GetRandomComponent(facialHair, type);
            portrait.facialHairRenderer.sprite = currentFacialHair.sprite;
            portrait.facialHairRenderer.color = currentHairColor;
        }
        else
        {
            portrait.facialHairRenderer.gameObject.SetActive(false);

        }

        if (UnityEngine.Random.value > 0.7F)
        {
            currentFullFaceDetail = (FullFaceDetail)GetRandomComponent(fullFaceDetails, type);
            portrait.fullFaceDetailRenderer.sprite = currentFullFaceDetail.sprite;
        }
        else
        {
            portrait.fullFaceDetailRenderer.gameObject.SetActive(false);

        }

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

    static FaceComponent GetRandomComponent(FaceComponent[] array, CharacterType type)
    {
        List<FaceComponent> components = new List<FaceComponent>();
        bool match = false;

        foreach (FaceComponent comp in array)
        {
            foreach (CharacterType t in comp.types)
            {
                if (type == t)
                {
                    match = true;
                    components.Add(comp);
                }
            }

        }

        if (!match)
        {
            return null;
        }
        else
        {
            var comp = components[UnityEngine.Random.Range(0, components.Count)];
            return comp;

        }

    }
}
