using System.Collections.Generic;
using UnityEngine;

public abstract class Optics : MonoBehaviour
{

    public Transform laserHolder;
    public abstract void ConstructGraph(LaserInfo _laserInfo, Vector3 _hitNormal, float _remainingLength, LayerMask _hitLayer);
    
    protected List<LaserInfo> inLasers = new List<LaserInfo>();
    protected List<LaserInfo> outLasers = new List<LaserInfo>();

    public virtual void RenderLaser()
    {
        // clear lasers
        for (int i = laserHolder.childCount - 1; i >= 0; i--)
        {
            Destroy(laserHolder.GetChild(i).gameObject);
        }
        
        foreach (LaserInfo laser in outLasers)
        {
            LineRenderer lineRenderer = Instantiate(laser.lineRendererPrefab, laser.startPosition, Quaternion.identity, laserHolder);
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, laser.startPosition);
            lineRenderer.SetPosition(1, laser.endPosition);
        }
        
        outLasers.Clear();
    }
}