using UnityEngine;

public class Mirror : Optics
{
    public override void ConstructGraph(LaserInfo _inLaser, Vector3 _hitNormal, float _length, LayerMask _hitLayer)
    {
        inLasers.Add(_inLaser);
        Vector3 start = _inLaser.endPosition;
        Vector3 direction = Vector3.Reflect(_inLaser.Direction(), _hitNormal);
        var ray = new Ray(start, direction);

        LaserInfo laserInfo = CastRay(ray, _length, _hitLayer);
        laserInfo.lineRendererPrefab = _inLaser.lineRendererPrefab;

        outLasers.Add(laserInfo);
    }
}