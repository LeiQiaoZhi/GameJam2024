using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera cam_;

    private void Start()
    {
        cam_ = Camera.main;
    }

    void Update()
    {
        // Make the health bar face the camera
        transform.LookAt(cam_.transform.position,
             Vector3.up);
    }
}