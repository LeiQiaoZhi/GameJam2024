using System;
using System.Collections.Generic;
using Logging;
using UnityEngine;

public abstract class Optics : MonoBehaviour
{
    [SerializeField] private Transform laserHolder;
    [SerializeField] private LineRendererPool lineRendererPool;

    private Collider collider_;

    public abstract void ConstructGraph(LaserInfo _inLaser, Vector3 _hitNormal, float _remainingLength,
        LayerMask _hitLayer);

    protected List<LaserInfo> inLasers = new List<LaserInfo>();
    protected List<LaserInfo> outLasers = new List<LaserInfo>();

    protected virtual void Start()
    {
        collider_ = GetComponentInChildren<Collider>();   
    }

    public virtual void RenderLaser()
    {
        XLogger.Log($"RenderLaser {gameObject.name} inLasers.Count: {inLasers.Count} outLasers.Count: {outLasers.Count}");
        lineRendererPool.DeactivateFrom(0);
        for (int i = 0; i < outLasers.Count; i++)
        {
            LaserInfo laser = outLasers[i];
            LineRenderer lineRenderer = lineRendererPool.GetLineRenderer(i);
            // LineRenderer lineRenderer = Instantiate(laser.lineRendererPrefab, laser.startPosition, Quaternion.identity,
            // laserHolder);
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, laser.startPosition);
            lineRenderer.SetPosition(1, laser.endPosition);
        }

        lineRendererPool.DeactivateFrom(outLasers.Count);

        inLasers.Clear();
        outLasers.Clear();
    }

    public LaserInfo CastRay(Ray _ray, float _length, LayerMask _hitLayer)
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