using System.Collections.Generic;
using Logging;
using UnityEngine;

public abstract class LightNode : MonoBehaviour
{
    [SerializeField] protected LaserSettings laserSettings;
    [SerializeField] protected LineRendererPool lineRendererPool;

    [SerializeField] protected Collider collider;
    protected List<LaserInfo> inLasers = new List<LaserInfo>();
    protected List<LaserInfo> outLasers = new List<LaserInfo>();

    public void ClearStates()
    {
        inLasers.Clear();
        outLasers.Clear();
    }

    public virtual void RenderLaser()
    {
        XLogger.Log(
            $"RenderLaser {gameObject.name} inLasers.Count: {inLasers.Count} outLasers.Count: {outLasers.Count}");
        lineRendererPool.DeactivateFrom(0);
        for (int i = 0; i < outLasers.Count; i++)
        {
            LaserInfo laser = outLasers[i];
            LineRenderer lineRenderer = lineRendererPool.GetLineRenderer(i);
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, laser.startPosition);
            lineRenderer.SetPosition(1, laser.endPosition);
        }

        lineRendererPool.DeactivateFrom(outLasers.Count);
    }

    public void DamageDetect()
    {
        for (int i = 0; i < outLasers.Count; i++)
        {
            LaserInfo laser = outLasers[i];
            var ray = new Ray(laser.startPosition, laser.Direction());
            var results = Physics.RaycastAll(ray, laser.Length(), laserSettings.damageLayer);

            for (int j = 0; j < results.Length; j++)
            {
                RaycastHit result = results[j];
                XLogger.Log(Category.Damage, $"DamageDetect {result.collider.name}");
                
                var damagable = result.collider.GetComponentInParent<Damagable>();
                if (damagable != null)
                {
                    damagable.Damage(1);
                }
                
            }
        }
    }
}