using System;
using System.Collections;
using System.Collections.Generic;
using Logging;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    private InputAction cameraRotateAction_;

    private bool isRotating_ = false;
    
    void Start()
    {
        if (inputController == null)
        {
            XLogger.LogError("InputController is not set in CameraController");
        }
        else
        {
            cameraRotateAction_ = inputController.GetCameraRotateAction();
            cameraRotateAction_.started += _ctx => isRotating_ = true;
            cameraRotateAction_.canceled += _ctx => isRotating_ = false;
        }
    }

    private void Update()
    {
        if (!isRotating_) return;
        
        var amount = cameraRotateAction_.ReadValue<float>();
        XLogger.Log(Category.Input,"Camera rotate amount: " + amount);
        
        // TODO: apply rotation
    }

}