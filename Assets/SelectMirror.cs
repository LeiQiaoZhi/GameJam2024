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

    public void DestroyVirtualImage()
    {
        GameObject oldChild = GameObject.Find("Mirror Shape Only");
        if (oldChild != null)
            Destroy(oldChild);
    }
    

    private void SelectMirror(int mirrorNumber)
    {
        XLogger.Log("Selecting mirror number: " + mirrorNumber);
        if (mirrorNumber == 1)
        {
            spawnMirrorScript.canPlaceMirror = false;
            DestroyVirtualImage();
        }
        else
        {
            spawnMirrorScript.canPlaceMirror = true;
            spawnMirrorScript.mirrorPrefab = mirrors[mirrorNumber-2];

            GameObject oldChild = GameObject.Find("Mirror Shape Only");
            if (oldChild != null)
                Destroy(oldChild);
            
            Vector3 mirrorPosition = transform.position + transform.forward;
            mirrorPosition.y = spawnMirrorScript.spawnHeight;
            GameObject newChild = Instantiate(mirrors[mirrorNumber-2], mirrorPosition, transform.rotation, transform);
            
            newChild.name = "Mirror Shape Only";
            foreach (Rigidbody rb in newChild.GetComponentsInChildren<Rigidbody>())
            {
                Debug.Log("Destroying Rigidbody on " + rb.gameObject.name);
                Destroy(rb);
            }
            
            foreach (Collider rb in newChild.GetComponentsInChildren<Collider>(true))
            {
                Debug.Log("Destroying Rigidbody on " + rb.gameObject.name);
                Destroy(rb);
            }
        }
    }
}
