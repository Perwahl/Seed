using System;
using UnityEngine;

public class FaceGenerator : MonoBehaviour
{
    [SerializeField] private FaceComponent[] heads;
    [SerializeField] private FaceComponent[] eyes;
    [SerializeField] private FaceComponent[] mouths;
    [SerializeField] private FaceComponent[] hair;
    [SerializeField] private Color[] hairColor;


    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer eyesRenderer;
    [SerializeField] private SpriteRenderer mouthsRenderer;
    [SerializeField] private SpriteRenderer hairsRenderer;

    [ContextMenu("test")]
    public void GenerateFace()
    {
        headRenderer.sprite = heads[UnityEngine.Random.Range(0, heads.Length )].sprite;
        eyesRenderer.sprite = eyes[UnityEngine.Random.Range(0, eyes.Length )].sprite;
        mouthsRenderer.sprite = mouths[UnityEngine.Random.Range(0, mouths.Length )].sprite;
        hairsRenderer.sprite = hair[UnityEngine.Random.Range(0, hair.Length )].sprite;
        hairsRenderer.color = hairColor[UnityEngine.Random.Range(0, hairColor.Length)];

    }
}
