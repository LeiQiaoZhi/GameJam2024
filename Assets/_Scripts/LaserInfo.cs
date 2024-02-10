using UnityEngine;

[System.Serializable]
public class LaserInfo
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public LineRenderer lineRendererPrefab;
    
    public float Length()
    {
        return (endPosition - startPosition).magnitude;
    }
    
    // intensity
    public Vector3 Direction()
    {
        return (endPosition - startPosition).normalized;
    }
}