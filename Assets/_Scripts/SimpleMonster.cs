using UnityEngine;
using Logging;

public class SimpleMonster : Monster
{
    [SerializeField] private Vector3 hitBoxSize = Vector3.one;
    [SerializeField] private LayerMask detectableLayers_;
    private Transform monster_;

    void Start()
    {
        monster_ = selfCollider_.transform;
    }

    public override bool InRange(Collider other)
    {
        return Vector3.Distance(selfCollider_.transform.position,
                                other.transform.position) <= range_;
    }

    private bool hitSuccessful(Collider other)
    {
        Vector3 boxCenter = monster_.position + monster_.forward * hitBoxSize.z / 2; // Adjust the position as necessary

        // Check for rigidbodies in front of the character
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, hitBoxSize / 2, monster_.rotation, detectableLayers_);
        // XLogger.Log($"hitColliders.Length: {hitColliders.Length}");
        XLogger.Log(Category.Damage, "Collider in range of" + hitColliders.Length);
        foreach (var x in hitColliders)
        {
            XLogger.Log(Category.Damage, "Collider in range of" + x.GetHashCode());
            if (x.Equals(other)) return true;
        }
        return false;
    }

    public override bool Attack(Collider other)
    {
        if (InRange(other))
        {
            XLogger.Log(Category.Damage, "Successfully dealt damage" + Time.time);
            return true;
        }
        XLogger.Log(Category.Damage, "Attack failed");
        return false;
    }
}
