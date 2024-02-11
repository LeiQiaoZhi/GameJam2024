using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Logging;
using UnityEngine.Serialization;

public class SpawnMirrorScript : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Transform character;
    [SerializeField] public GameObject mirrorPrefab;

    [SerializeField] private Vector3 detectionBoxSize = new Vector3(1, 1, 1); // The size of the detection box
    [SerializeField] private LayerMask detectableLayers; // LayerMask to filter which layers to detect
    [FormerlySerializedAs("spawnYOffset")] [SerializeField] public float spawnHeight;
    
    [SerializeField] private Material virtualMirrorMaterial;
    [SerializeField] private Material virtualBaseMaterial;
    [SerializeField] private Material virtualMirrorMaterialRed;
    [SerializeField] private Material virtualBaseMaterialRed;
    public int mirrorToPlace;
    
    [FormerlySerializedAs("canPlaceMirror")] public bool placingMirror = false;

    public Action onPlaceMirror;
    
    private SelectMirrorScript selectMirrorScript;

    private InputAction placeObjectAction_;
    
    [SerializeField] private int initialMoney = 100;
    [SerializeField] private List<int> mirrorPrices = new List<int>();
    [SerializeField] private int money;

    public int GetMoney()
    {
        return money;
    }
    
    public void SetMoney(int newMoney)
    {
        money = newMoney;
    }
    
    
    void Start()
    {
        money = initialMoney;
        selectMirrorScript = GetComponent<SelectMirrorScript>();
        if (inputController == null)
        {
            XLogger.LogError("InputController is not set in CameraController");
        }
        else
        {
            placeObjectAction_ = inputController.GetPlaceObjectAction();
            placeObjectAction_.performed += _ctx => PlaceMirror();
        }
    }

    private bool isSpaceOccupied()
    {
        Vector3 boxCenter = character.position + character.forward * detectionBoxSize.z; // Adjust the position as necessary

        // Check for rigidbodies in front of the character
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, detectionBoxSize / 2, character.rotation, detectableLayers);
        // XLogger.Log($"hitColliders.Length: {hitColliders.Length}");
        bool isSpaceOccupied = hitColliders.Length > 0;
        
        return isSpaceOccupied;
    }

    public void PlaceMirror()
    {
        selectMirrorScript.DestroyVirtualImage();
        
        
        int mirrorPrice = mirrorPrices[mirrorToPlace - 2];
        if (placingMirror && !isSpaceOccupied() && money >= mirrorPrice)
        {
            
            money -= mirrorPrice;
            onPlaceMirror?.Invoke();
            
            Vector3 mirrorPosition = character.position + character.forward;
            mirrorPosition.y = spawnHeight;
            GameObject mirror = Instantiate(mirrorPrefab, mirrorPosition, character.rotation);
            
            var optics = mirror.GetComponent<Optics>();
            if (optics != null)
            {
                if (LaserManager.Instance != null)
                    LaserManager.Instance.AddOptics(optics);
            }

        }
        placingMirror = false;  // Disable placing mirrors after placing one
        selectMirrorScript.SelectMirror(1);
    }

    public void UpdateMirrorMaterial()
    {
        Material mirrorMaterial;
        Material baseMaterial;
        if (isSpaceOccupied() || money < mirrorPrices[mirrorToPlace - 2])
        {
            mirrorMaterial = virtualMirrorMaterialRed;
            baseMaterial = virtualBaseMaterialRed;
        }
        else
        {
            mirrorMaterial = virtualBaseMaterial;
            baseMaterial = virtualBaseMaterial;
        }
        GameObject mirrorMesh = gameObject.transform.Find("Mirror Shape Only/AllMirror/Mirror").gameObject;  // for all three the mirror part is called "cube"
        mirrorMesh.GetComponent<Renderer>().material = mirrorMaterial;
        GameObject baseMesh = gameObject.transform.Find("Mirror Shape Only/AllMirror/Base").gameObject;
        baseMesh.GetComponent<Renderer>().material = baseMaterial;
    }
    
    public void Update()
    {
        if (placingMirror)
        {
            UpdateMirrorMaterial();
        }
    }
    
    // Optional: Draw the detection box in the Scene view for easier debugging
    void OnDrawGizmos()
    {
        if (character != null)
        {
            Vector3 boxCenter = character.position + character.forward * detectionBoxSize.z;
            Gizmos.matrix = Matrix4x4.TRS(boxCenter, character.rotation, detectionBoxSize);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
