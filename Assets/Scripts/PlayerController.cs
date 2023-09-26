using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Movement variables
    public float moveSpeed = 5.0f;
    public float jumpForce = 500.0f;
    public bool canMove = true;

    private Rigidbody rigidBody;
    private Animator animator;
    private Camera mainCamera;

    public FruitID currentFruit;

    public static PlayerController Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get the input axes
       
    }

    bool IsGrounded()
    {
        // Check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, 1.0f);
    }
}

