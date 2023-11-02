using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //it's a plane so make sure it doesnt flip
        gameObject.transform.LookAt(Camera.main.transform);
    }
}
