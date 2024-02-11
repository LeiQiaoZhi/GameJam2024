using UnityEngine;

public class SimpleTargetSelector : ITargetSelector
{
    [SerializeField] GameObject playerTagert;

    protected override void UpdateTarget()
    {
        Target_ = playerTagert;
    }
}
