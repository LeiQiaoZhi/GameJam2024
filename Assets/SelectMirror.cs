using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Logging;
using UnityEngine.Serialization;

public class SelectMirrorScript : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    
    private InputAction placeObjectAction_;

    // Start is called before the first frame update
    void Start()
    {
        if (inputController == null)
        {
            XLogger.LogError("InputController is not set in CameraController");
        }
        else
        {
            placeObjectAction_ = inputController.GetSelectMirrorAction1();
            placeObjectAction_.performed += _ctx => SelectMirror(1);
            placeObjectAction_ = inputController.GetSelectMirrorAction2();
            placeObjectAction_.performed += _ctx => SelectMirror(2);
            placeObjectAction_ = inputController.GetSelectMirrorAction3();
            placeObjectAction_.performed += _ctx => SelectMirror(3);
            placeObjectAction_ = inputController.GetSelectMirrorAction4();
            placeObjectAction_.performed += _ctx => SelectMirror(4);
        }
    }
    

    private void SelectMirror(int mirrorNumber)
    {
        
    }
    

    private void Update()
    {

    }
}
