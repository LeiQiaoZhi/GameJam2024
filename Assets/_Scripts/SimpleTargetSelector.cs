using UnityEngine;

public class SimpleTargetSelector : ITargetSelector
{
    [SerializeField] public Collider playerTagert;

    protected override void UpdateTarget()
    {
        Target_ = playerTagert;
    }
}
