using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Logging;

public class SpawnMirrorScript : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Transform character;
    [SerializeField] private GameObject mirrorPrefab;

    [SerializeField] private Vector3 detectionBoxSize = new Vector3(1, 1, 1); // The size of the detection box
    [SerializeField] private LayerMask detectableLayers; // LayerMask to filter which layers to detect


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
            placeObjectAction_ = inputController.GetPlaceObjectAction();
            placeObjectAction_.performed += _ctx => PlaceMirror();
        }
    }

    private void PlaceMirror()
    {
        // Place in front of character
        // mirror same rotation as character
        Vector3 boxCenter = character.position + character.forward * detectionBoxSize.z; // Adjust the position as necessary

        // Check for rigidbodies in front of the character
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, detectionBoxSize / 2, character.rotation, detectableLayers);
        bool isSpaceOccupied = hitColliders.Length > 0;

        if (!isSpaceOccupied)
        {
            Vector3 mirrorPosition = character.position + character.forward;
            mirrorPosition.y = -1;
            Instantiate(mirrorPrefab, mirrorPosition, character.rotation);
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
