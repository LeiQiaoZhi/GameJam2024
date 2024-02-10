using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] private float damageCheckInterval = 0.1f;
    [SerializeField] private List<Laser> lasers;
    [SerializeField] private List<Optics> optics;

    private float nextCheckTime_;

    private void Start()
    {
        nextCheckTime_ = Time.time + damageCheckInterval;
    }

    private void Update()
    {
        // construct graph
        foreach (Laser laser in lasers)
            laser.ConstructGraph();

        // render laser
        foreach (Laser laser in lasers)
            laser.RenderLaser();

        foreach (Optics optic in optics)
            optic.RenderLaser();

        if (Time.time > nextCheckTime_)
        {
            CheckDamage();
            nextCheckTime_ = Time.time + damageCheckInterval;
        }

        // clear states
        foreach (Laser laser in lasers)
            laser.ClearStates();

        foreach (Optics optic in optics)
            optic.ClearStates();
    }

    private void CheckDamage()
    {
        // deal damage
        foreach (Laser laser in lasers)
            laser.DamageDetect();

        foreach (Optics optic in optics)
            optic.DamageDetect();
    }
}