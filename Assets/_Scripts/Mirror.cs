using UnityEngine;

public class Mirror : Optics
{
    public override void ConstructGraph(LaserInfo _laserInfo, Vector3 _hitNormal, float _length, LayerMask _hitLayer)
    {
        inLasers.Add(_laserInfo);
        Vector3 start = _laserInfo.endPosition;
        Vector3 direction = Vector3.Reflect(Vector3.forward, _hitNormal);
        var ray = new Ray(start, direction);
        var laserInfo = new LaserInfo
        {
            startPosition = start,
            endPosition = start + direction * _length,
            lineRendererPrefab = _laserInfo.lineRendererPrefab
        };
        if (Physics.Raycast(ray, out RaycastHit hit, _length, _hitLayer))
        {
            // reflection
            var optics = hit.collider.GetComponentInParent<Optics>();
            if (optics != null)
            {
                laserInfo.endPosition = hit.point;
                var remainingLength = _length - laserInfo.Length();
                optics.ConstructGraph(laserInfo, hit.normal, remainingLength, _hitLayer);
            }
        }

        outLasers.Add(laserInfo);
    }
}