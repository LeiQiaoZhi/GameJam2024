using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] private List<Laser> lasers;
    [SerializeField] private List<Optics> optics;

    private void Update()
    {
        // construct graph
        foreach (Laser laser in lasers)
        {
            laser.ConstructGraph();
        }
        
        // render laser
        foreach (Laser laser in lasers)
        {
            laser.RenderLaser();
        }

        foreach (Optics optic in optics)
        {
            optic.RenderLaser();
        }
        
        // deal damage
    }
}