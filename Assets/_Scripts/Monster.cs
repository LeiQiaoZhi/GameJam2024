using System;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected GameObject model_;
    [SerializeField] protected float range_;
    /// Returns if another object is within range
    public abstract bool InRange(GameObject other);
    /// Returns if the attack was successful
    public abstract bool Attack(GameObject other);
}