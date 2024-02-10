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
        Vector3 mirrorPosition = character.position + character.forward;
        mirrorPosition.y = -1;
        Instantiate(mirrorPrefab, mirrorPosition, character.rotation);
        // Instantiate(mirrorPrefab, transform.position, Quaternion.identity);
    }
}
