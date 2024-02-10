using System;
using System.Collections;
using System.Collections.Generic;
using Logging;
using UnityEngine;
using UnityEngine.Serialization;

public class Laser : LightNode
{
    [SerializeField] private Transform firePoint;
    
    public void ConstructGraph()
    {
        var maxLength = laserSettings.maxLength;
        LayerMask hitLayer = laserSettings.hitLayer;
        
        var ray = new Ray(firePoint.position, firePoint.forward);
        var laserInfo = new LaserInfo
        {
            startPosition = firePoint.position,
            endPosition = firePoint.position + firePoint.forward * maxLength
        };
        if (Physics.Raycast(ray, out RaycastHit hit, maxLength, hitLayer))
        {
            laserInfo.endPosition = hit.point;
            // optics
            var optics = hit.collider.GetComponentInParent<Optics>();
            if (optics != null)
            {
                var remainingLength = maxLength - laserInfo.Length();
                optics.ConstructGraph(laserInfo, hit.normal, remainingLength, hitLayer);
            }
        }

        outLasers.Add(laserInfo);
    }

    
}