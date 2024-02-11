using UnityEngine;

public class SimpleTargetSelector : ITargetSelector
{
    [SerializeField] Collider playerTagert;

    protected override void UpdateTarget()
    {
        Target_ = playerTagert;
    }
}
