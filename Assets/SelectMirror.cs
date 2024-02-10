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
    [SerializeField] List<GameObject> mirrors = new List<GameObject>();
    private SpawnMirrorScript spawnMirrorScript;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnMirrorScript = GetComponent<SpawnMirrorScript>();
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
        XLogger.Log("Selecting mirror number: " + mirrorNumber);
        if (mirrorNumber == 1)
        {
            spawnMirrorScript.disablePlaceMirror = true;
            
            GameObject mirrorMesh = GameObject.Find("Mirror Mesh");
            Renderer renderer = mirrorMesh.GetComponent<Renderer>();
            renderer.enabled = false;

        }
        else
        {
            spawnMirrorScript.disablePlaceMirror = false;
            
            spawnMirrorScript.mirrorPrefab = mirrors[mirrorNumber-2];
            
            GameObject mirrorMesh = GameObject.Find("Mirror Mesh");
            Renderer renderer = mirrorMesh.GetComponent<Renderer>();
            renderer.enabled = true;
            // make the renderer material has transparent color
            renderer.material.color = new Color(1, 1, 1, 0.5f);
        }
    }
    

    private void Update()
    {

    }
}
