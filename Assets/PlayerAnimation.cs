using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Rigidbody rb_;
    
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Velocity", rb_.velocity.magnitude);
    }
}
