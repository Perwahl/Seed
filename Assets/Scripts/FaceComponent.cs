using UnityEngine;

[CreateAssetMenu(menuName = "FaceComponent")]
public class FaceComponent : ScriptableObject
{
    [SerializeField] private bool optional;
    [SerializeField] public Sprite sprite;

    public enum FaceComponentType { head,  eyes, hair, mouth };

    [SerializeField]
    private FaceComponentType type;


}

//public class Eyes : FaceComponent
//{
   

//}

//public class Mouth : FaceComponent
//{
   

//}

//public class Hair :FaceComponent
//{
//    private bool optional;

//}
