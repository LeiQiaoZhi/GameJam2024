using UnityEngine;

public class FocusLens : Optics
{
    [SerializeField] private int focusLength;
    
    private Vector3 GetFocus(Vector3 _normal)
    {
        return transform.position + _normal * focusLength;
    }
    public override void ConstructGraph(LaserInfo _inLaser, Vector3 _hitNormal, float _length)
    {
        inLasers.Add(_inLaser);
        Vector3 start = _inLaser.endPosition;
        Vector3 focus = GetFocus(-_hitNormal);
        Vector3 direction = (focus - start).normalized;
        direction.y = 0;
        
        var ray = new Ray(start, direction);
        LaserInfo laserInfo = CastRay(ray, _length);
        outLasers.Add(laserInfo);
    }
}