using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public ITargetSelector targetSelector_;
    [SerializeField] public NavMeshAgent navAgent_;
    [SerializeField] private Monster monster_;
    private Vector3 targetDest_;

    public void Start()
    {
        // By default, if the navAgent does not have a destination set,
        // it will simply return the body's current position
        targetDest_ = navAgent_.destination;
    }

    public void Update()
    {
        GameObject curTarget = targetSelector_.Target_;

        if (monster_.InRange(curTarget))
        {
            monster_.Attack(curTarget);
            return; // For this frame, that's it
        }

        // Two possible cases: target moved or target changed
        if (targetDest_ != targetSelector_.Target_.transform.position)
        {
            targetDest_ = targetSelector_.Target_.transform.position;
            navAgent_.SetDestination(targetDest_);
        }
    }
}
