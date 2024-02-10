using UnityEngine;

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
}