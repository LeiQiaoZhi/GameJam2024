using System;
using System.Collections.Generic;
using Logging;
using UnityEngine;

public abstract class Optics : LightNode
{
    [SerializeField] private Transform laserHolder;
  
    public abstract void ConstructGraph(LaserInfo _inLaser, Vector3 _hitNormal, float _remainingLength,
        LayerMask _hitLayer);

    protected LaserInfo CastRay(Ray _ray, float _length, LayerMask _hitLayer)
    {
        collider_.enabled = false;
        if (_length <= 0)
        {
            return null;
        }

        var laserInfo = new LaserInfo
        {
            startPosition = _ray.origin,
            endPosition = _ray.origin + _ray.direction * _length
        };
        if (Physics.Raycast(_ray, out RaycastHit hit, _length, _hitLayer))
        {
            // reflection
            var optics = hit.collider.GetComponentInParent<Optics>();
            laserInfo.endPosition = hit.point;
            if (optics != null)
            {
                var remainingLength = _length - laserInfo.Length();
                optics.ConstructGraph(laserInfo, hit.normal, remainingLength, _hitLayer);
            }
        }

        collider_.enabled = true;

        return laserInfo;
    }
}