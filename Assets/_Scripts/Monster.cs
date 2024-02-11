using System;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected Collider selfCollider_;
    [SerializeField] protected float range_;
    [SerializeField] public int attackDamage;
    /// Returns if another object is within range
    public abstract bool InRange(Collider other);
    /// Returns if the attack was successful
    public abstract bool Attack(Collider other);
}
