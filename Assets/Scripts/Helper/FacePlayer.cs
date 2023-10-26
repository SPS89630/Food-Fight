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
        //not on y axis
        gameObject.transform.LookAt(new Vector3(PlayerController.Instance.transform.position.x, gameObject.transform.position.y, PlayerController.Instance.transform.position.z));
    }
}
