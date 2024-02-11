using System.Collections;
using System.Collections.Generic;
using Logging;
using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyMirrorScript : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    // Serialized Field to select mirror layer
    [SerializeField] private LayerMask mirrorLayer;
    private InputAction breakMirrorAction_;
    
    // Start is called before the first frame update
    void Start()
    {
        breakMirrorAction_ = inputController.GetBreakMirrorAction();
        breakMirrorAction_.performed += _ctx => BreakMirror();
    }

    private void BreakMirror()
    {
        XLogger.Log("Breaking Mirror");
        // cast a box to see if any mirrors are in front of the player
        Vector3 detectionSize = new Vector3(4, 3, 2);
        Vector3 boxCenter = transform.position + transform.forward * 2; // Adjust the position as necessary
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, detectionSize / 2, transform.rotation, mirrorLayer);

        XLogger.Log("Mirror count: " + hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            // if there are mirrors in front of the player, destroy the first one
            Destroy(hitColliders[i].transform.root.gameObject);
        }
    }
}
