using UnityEngine;

public class FaceComponent : ScriptableObject
{
    [SerializeField] private bool optional;
    [SerializeField] public Sprite sprite;
   

    public enum FaceComponentType { head,  eyes, hair, mouth };

    [SerializeField]
    private FaceComponentType type;
}

