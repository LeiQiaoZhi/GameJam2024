﻿using System;
using System.Collections.Generic;
using Logging;
using UnityEngine;

public abstract class Optics : LightNode
{
    [SerializeField] private Transform laserHolder;

    public abstract void ConstructGraph(LaserInfo _inLaser, Vector3 _hitNormal, float _remainingLength);

    protected LaserInfo CastRay(Ray _ray, float _length)
    {
        if (collider_ != null)
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
        if (Physics.Raycast(_ray, out RaycastHit hit, _length, laserSettings.hitLayer))
        {
            var optics = hit.collider.GetComponentInParent<Optics>();
            laserInfo.endPosition = hit.point;
            if (optics != null)
            {
                var remainingLength = _length - laserInfo.Length();
                optics.ConstructGraph(laserInfo, hit.normal, remainingLength);
            }
        }

        if (collider_ != null)
            collider_.enabled = true;

        return laserInfo;
    }
}