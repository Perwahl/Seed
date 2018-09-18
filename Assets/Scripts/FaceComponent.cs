using UnityEngine;

public class FaceComponent : ScriptableObject
{
    [SerializeField] private bool optional;
    [SerializeField] public Sprite sprite;
   

    public enum FaceComponentType { head,  eyes, hair, mouth };

    [SerializeField]
    private FaceComponentType type;


}

[CreateAssetMenu(menuName = "FaceComponent/Eyes")]
public class Eyes : FaceComponent
{
   // [SerializeField] public Sprite sadSprite;
    [SerializeField] public Sprite sleepSprite;
    [SerializeField] public Sprite madSprite;

    [SerializeField] public Sprite pupilSprite;


}

[CreateAssetMenu(menuName = "FaceComponent/Mouth")]
public class Mouth : FaceComponent
{


}

[CreateAssetMenu(menuName = "FaceComponent/Hair")]
public class Hair : FaceComponent
{
}

[CreateAssetMenu(menuName = "FaceComponent/BaseHead")]
public class Head : FaceComponent
{
}
