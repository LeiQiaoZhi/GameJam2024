using UnityEngine;

[CreateAssetMenu(fileName = "LaserSettings", menuName = "LaserSettings")]
public class LaserSettings : ScriptableObject
{
    public float maxLength;
    public LayerMask hitLayer;
    public LayerMask damageLayer;
}