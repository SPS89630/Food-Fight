using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveAction : MonoBehaviour
{
    public MeshRenderer renderer;
    public Vector3 teleportPosition; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Click!");
        PlayerController.Instance.transform.DOMove(teleportPosition, 1f);
    }
}
