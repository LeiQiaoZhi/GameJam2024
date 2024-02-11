using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Laser : LightNode
{
    [SerializeField] private Transform firePoint;

    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private bool on;

    private void Start()
    {
        if (on)
            TurnOn();
        else
            TurnOff();
    }

    public void ConstructGraph()
    {
        if (!on) return;

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
                optics.ConstructGraph(laserInfo, hit.normal, remainingLength);
            }
        }

        outLasers.Add(laserInfo);
    }

    public void TurnOff()
    {
        on = false;
        outLasers.Clear();
        lineRendererPool.DeactivateFrom(0);
        fireEffect.Stop();
    }

    public void TurnOn()
    {
        on = true;
        fireEffect.Play();
    }
}