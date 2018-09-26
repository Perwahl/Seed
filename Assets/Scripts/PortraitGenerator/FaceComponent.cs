using UnityEngine;

public class FaceComponent : ScriptableObject
{    
    [SerializeField] public Sprite sprite;
   

    public enum FaceComponentType { eyes, hair, mouth };

    [SerializeField] public FaceComponentType componentType;
    [SerializeField] public CharacterPortrait.CharacterType[] types;
  


}

