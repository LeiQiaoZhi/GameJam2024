using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private DefaultControls defaultControls_;
    
    private InputAction moveAction_;
    private InputAction cameraRotateAction_;
    private InputAction placeObjectAction_;

    private void OnEnable()
    {
        defaultControls_ = new DefaultControls();
        moveAction_ = defaultControls_.Gameplay.Move;
        cameraRotateAction_ = defaultControls_.Gameplay.CameraRotate;
        placeObjectAction_ = defaultControls_.Gameplay.PlaceObject;
        defaultControls_.Enable();
    }
    
    private void OnDisable()
    {
        defaultControls_.Disable();
    }
    
    /// APIs ///
    public InputAction GetMoveAction()
    {
        return moveAction_;
    }
    
    public InputAction GetCameraRotateAction()
    {
        return cameraRotateAction_;
    }
    
    public InputAction GetPlaceObjectAction()
    {
        return placeObjectAction_;
    }
}
