using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterLens : Optics
{
    [SerializeField] private int scatterNumber;
    [SerializeField] private float scatterAngle;

    public override void ConstructGraph(LaserInfo _inLaser, Vector3 _hitNormal, float _length, LayerMask _hitLayer)
    {
        inLasers.Add(_inLaser);
        Vector3 start = _inLaser.endPosition;
        Vector3 direction = -_hitNormal;

        var angleInBetween = (scatterNumber > 1) ? scatterAngle / (scatterNumber - 1) : 0;
        var leftMostAngle = -scatterAngle / 2;

        for (int i = 0; i < scatterNumber; i++)
        {
            Vector3 scatterDirection = Quaternion.AngleAxis(leftMostAngle + angleInBetween * i, Vector3.up) * direction;
            var ray = new Ray(start, scatterDirection);
            LaserInfo laserInfo = CastRay(ray, _length, _hitLayer);
            outLasers.Add(laserInfo);
        }
    }
}