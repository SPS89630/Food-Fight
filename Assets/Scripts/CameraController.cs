using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera variables
    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float mouseSensitivity = 100.0f;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    public bool cameraFree = true;
    public bool respawn = false;

    public static CameraController Instance { get; private set; }
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

    void LateUpdate () 
    {
    
    }
}
