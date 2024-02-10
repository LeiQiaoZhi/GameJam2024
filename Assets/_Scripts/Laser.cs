using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxLength;
    [SerializeField] private LayerMask hitLayer;

    private List<LaserInfo> outLasers_ = new List<LaserInfo>();

    private void Update()
    {
        // Activate();
    }

    private void ResetLine()
    {
        lineRendererPrefab.positionCount = 0;
    }

    private void AddPoint(Vector3 _point)
    {
        lineRendererPrefab.positionCount++;
        lineRendererPrefab.SetPosition(lineRendererPrefab.positionCount - 1, _point);
    }

    private void RaycastLine(Vector3 _start, Vector3 _direction, float _length)
    {
        var ray = new Ray(_start, _direction);
        Vector3 endPoint = _start + _direction * _length;
        if (Physics.Raycast(ray, out RaycastHit hit, _length, hitLayer))
        {
            endPoint = hit.point;
            AddPoint(endPoint);

            // reflection
            var reflection = hit.collider.GetComponentInParent<Reflection>();
            if (reflection != null)
            {
                Vector3 reflectDirection = Vector3.Reflect(_direction, hit.normal);
                var remainingLength = _length - (hit.point - _start).magnitude;
                RaycastLine(hit.point, reflectDirection, remainingLength);
            }
        }
        else
        {
            AddPoint(endPoint);
        }
    }

    private void Activate()
    {
        lineRendererPrefab.enabled = true;
        ResetLine();
        AddPoint(firePoint.position);

        // raycast
        RaycastLine(firePoint.position, firePoint.forward, maxLength);
    }

    public void ConstructGraph()
    {
        var ray = new Ray(firePoint.position, firePoint.forward);
        var laserInfo = new LaserInfo
        {
            startPosition = firePoint.position,
            endPosition = firePoint.position + firePoint.forward * maxLength,
            lineRendererPrefab = lineRendererPrefab
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

        outLasers_.Add(laserInfo);
    }

    public void RenderLaser()
    {
        for (int i = firePoint.childCount - 1; i >= 0; i--)
        {
            Destroy(firePoint.GetChild(i).gameObject);
        }

        foreach (LaserInfo laser in outLasers_)
        {
            LineRenderer newLineRenderer =
                Instantiate(lineRendererPrefab, laser.startPosition, Quaternion.identity, firePoint);
            newLineRenderer.enabled = true;
            newLineRenderer.positionCount = 2;
            newLineRenderer.SetPosition(0, laser.startPosition);
            newLineRenderer.SetPosition(1, laser.endPosition);
        }

        outLasers_.Clear();
    }
}