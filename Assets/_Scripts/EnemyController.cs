using UnityEngine;
using UnityEngine.AI;
using Logging;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public ITargetSelector targetSelector_;
    [SerializeField] public NavMeshAgent navAgent_;
    [SerializeField] private Monster monster_;
    [SerializeField] private float attackCD_;
    private float cdLeft_;
    private Vector3 targetDest_;

    public void Start()
    {
        // By default, if the navAgent does not have a destination set,
        // it will simply return the body's current position
        targetDest_ = navAgent_.destination;
        cdLeft_ = attackCD_;
    }

    public void Update()
    {
        Collider curTarget = targetSelector_.Target_;

        if (monster_.InRange(curTarget))
        {
            if (cdLeft_ > 0f)
            {
                XLogger.Log(Category.Damage, "Waiting for CD, " + cdLeft_ + " seconds left");
                cdLeft_ -= Time.deltaTime;
            }
            else
            {
                monster_.Attack(curTarget);
                cdLeft_ = attackCD_;
            }

            return; // For this frame, that's it
        }

        /// Reset attack CD when we go out of range
        cdLeft_ = attackCD_;

        // Two possible cases: target moved or target changed
        if (targetDest_ != targetSelector_.Target_.transform.position)
        {
            targetDest_ = targetSelector_.Target_.transform.position;
            navAgent_.SetDestination(targetDest_);
        }
    }
}
