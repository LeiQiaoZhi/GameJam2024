using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpawnMirrorScript mirrorSpawner;

    private Rigidbody rb_;
    
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        mirrorSpawner.onPlaceMirror += OnMirrorPlaced;
    }
    
    private void OnDisable()
    {
        mirrorSpawner.onPlaceMirror -= OnMirrorPlaced;
    }

    private void OnMirrorPlaced()
    {
        animator.SetTrigger("Place");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Velocity", rb_.velocity.magnitude);
    }
}
