using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxLength;
    [SerializeField] private LayerMask hitLayer;

    private void Update()
    {
        Activate();
    }

    private void ResetLine()
    {
        lineRenderer.positionCount = 0;
    }

    private void AddPoint(Vector3 _point)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, _point);
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
        lineRenderer.enabled = true;
        ResetLine();
        AddPoint(firePoint.position);

        // raycast
        RaycastLine(firePoint.position, firePoint.forward, maxLength);
    }
}