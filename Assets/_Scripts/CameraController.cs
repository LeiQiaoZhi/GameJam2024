using System;
using System.Collections;
using System.Collections.Generic;
using Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform centerTargetTransform;
    private InputAction cameraRotateAction_;
    [SerializeField] private float rotationSpeed = 50f; // Customize this value in the Editor
    
    private bool isRotating_ = false;
    private Vector3 targetPreviousPosition; // Previous position of the target
    
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
            
            targetPreviousPosition = centerTargetTransform.position;
        }
    }

    private void Update()
    {
        
        FollowTarget(); // Follow the target before rotation
        if (isRotating_)
        {
            var rotateAmount = cameraRotateAction_.ReadValue<float>();
            RotateCamera(rotateAmount);
        }
    }
    
    private void FollowTarget()
    {
        // Maintain the initial distance from the target after rotation
        Vector3 targetCurrentPosition = centerTargetTransform.position;
        Vector3 targetPosDelta = targetCurrentPosition - targetPreviousPosition;
        cameraTransform.position += targetPosDelta;
        targetPreviousPosition = targetCurrentPosition;
    }
    
    private void RotateCamera(float _rotationInput)
    {
        float horizontalRotation = _rotationInput * rotationSpeed * Time.deltaTime * (-1);
        cameraTransform.RotateAround(centerTargetTransform.position, Vector3.up, horizontalRotation);
    }
}