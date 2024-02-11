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

    [SerializeField] private Vector3 detectionBoxSize = new Vector3(0, 0, 0); // The size of the detection box
    [SerializeField] private LayerMask detectableLayers; // LayerMask to filter which layers to detect
    [FormerlySerializedAs("spawnYOffset")] [SerializeField] public float spawnHeight;
    
    [SerializeField] private Material virtualMirrorMaterial;
    [SerializeField] private Material virtualBaseMaterial;
    [SerializeField] private Material virtualMirrorMaterialRed;
    [SerializeField] private Material virtualBaseMaterialRed;
    [SerializeField] private Material virtualTurretMaterial;
    [SerializeField] private Material virtualTurretMaterialRed;
    public int mirrorToPlace;
    
    [FormerlySerializedAs("canPlaceMirror")] public bool placingMirror = false;

    public Action onPlaceMirror;
    
    private SelectMirrorScript selectMirrorScript;

    private InputAction placeObjectAction_;
    
    [SerializeField] private List<int> mirrorPrices = new List<int>();
    [SerializeField] private MoneyManager moneyManager;
    
    void Start()
    {
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
    
    public void setDetectionBoxSize(int _mirrorType)
    {
        if (_mirrorType == 2)
        {
            detectionBoxSize = new Vector3(12, 4, 1);
        }
        else if (_mirrorType == 3)
        {
            detectionBoxSize = new Vector3(10, 4, 1);
        }
        else if (_mirrorType == 4)
        {
            detectionBoxSize = new Vector3(8, 4, 1);
        }
        else if (_mirrorType == 5)
        {
            detectionBoxSize = new Vector3(2, 2, 1.8f);
        }
    }

    public void PlaceMirror()
    {
        selectMirrorScript.DestroyVirtualImage();
        
        
        int mirrorPrice = mirrorPrices[mirrorToPlace - 2];
        if (placingMirror && !isSpaceOccupied() && moneyManager.GetMoney() >= mirrorPrice)
        {

            moneyManager.ChangeMoney(-1 * mirrorPrice);
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
        if (mirrorToPlace >= 2 && mirrorToPlace <= 4)  // mirrors
        {
            Material mirrorMaterial;
            Material baseMaterial;
            if (isSpaceOccupied() || moneyManager.GetMoney() < mirrorPrices[mirrorToPlace - 2])
            {
                mirrorMaterial = virtualMirrorMaterialRed;
                baseMaterial = virtualBaseMaterialRed;
            }
            else
            {
                mirrorMaterial = virtualBaseMaterial;
                baseMaterial = virtualBaseMaterial;
            }
            GameObject mirrorObject = gameObject.transform.Find("Mirror Shape Only/AllMirror/Mirror").gameObject;  // for all three the mirror part is called "cube"
            mirrorObject.GetComponent<Renderer>().material = mirrorMaterial;
            GameObject baseObject = gameObject.transform.Find("Mirror Shape Only/AllMirror/Base").gameObject;
            baseObject.GetComponent<Renderer>().material = baseMaterial;
        }
        else if (mirrorToPlace == 5)
        {
            Material turretMaterial;
            if (isSpaceOccupied() || moneyManager.GetMoney() < mirrorPrices[mirrorToPlace - 2])
            {
                turretMaterial = virtualTurretMaterialRed;
            }
            else
            {
                turretMaterial = virtualTurretMaterial;
            }
            GameObject turretObject = gameObject.transform.Find("Mirror Shape Only/Cube1").gameObject;
            turretObject.GetComponent<Renderer>().material = turretMaterial;
            turretObject = gameObject.transform.Find("Mirror Shape Only/Cube2").gameObject;
            turretObject.GetComponent<Renderer>().material = turretMaterial;
        }
        
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
