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
    private InputAction selectMirror1Action_;
    private InputAction selectMirror2Action_;
    private InputAction selectMirror3Action_;
    private InputAction selectMirror4Action_;
    private InputAction selectMirror5Action_;
    private InputAction breakMirrorAction_;
    private InputAction shootAction_;

    private void OnEnable()
    {
        defaultControls_ = new DefaultControls();
        moveAction_ = defaultControls_.Gameplay.Move;
        cameraRotateAction_ = defaultControls_.Gameplay.CameraRotate;
        placeObjectAction_ = defaultControls_.Gameplay.PlaceObject;
        selectMirror1Action_ = defaultControls_.Gameplay.SelectMirror1;
        selectMirror2Action_ = defaultControls_.Gameplay.SelectMirror2;
        selectMirror3Action_ = defaultControls_.Gameplay.SelectMirror3;
        selectMirror4Action_ = defaultControls_.Gameplay.SelectMirror4;
        selectMirror5Action_ = defaultControls_.Gameplay.SelectMirror5;
        shootAction_ = defaultControls_.Gameplay.Shoot;
        breakMirrorAction_ = defaultControls_.Gameplay.BreakMirror;
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
    
    public InputAction GetSelectMirrorAction1()
    {
        return selectMirror1Action_;
    }
    
    public InputAction GetSelectMirrorAction2()
    {
        return selectMirror2Action_;
    }
    
    public InputAction GetSelectMirrorAction3()
    {
        return selectMirror3Action_;
    }
    
    public InputAction GetSelectMirrorAction4()
    {
        return selectMirror4Action_;
    }
    
    public InputAction GetSelectMirrorAction5()
    {
        return selectMirror5Action_;
    }
    
    public InputAction GetShootAction()
    {
        return shootAction_;
    }
    
    public InputAction GetBreakMirrorAction()
    {
        return breakMirrorAction_;
    }
}
