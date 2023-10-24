using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public MeshRenderer renderer;
    public Material oldMat;
    public Material highlightMaterial;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        oldMat = renderer.material;
    }
    void OnMouseOver()
    {
        renderer.material = highlightMaterial;
    }

    void OnMouseExit()
    {
        renderer.material = oldMat;
    }
}
