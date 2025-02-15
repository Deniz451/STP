using System;
using UnityEngine;

public class SelectablePart : MonoBehaviour
{
    public Material outliner;
    private MeshRenderer meshRenderer;
    private Material[] originalMaterials;

    public Action<SelectablePart> clicked;


    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterials = meshRenderer.materials;
    }

    private void OnMouseDown()
    {
        clicked?.Invoke(this);
    }

    private void OnMouseEnter()
    {
        Material[] newMaterials = meshRenderer.materials;
        if (newMaterials.Length > 1)
        {
            newMaterials[1] = outliner;
            meshRenderer.materials = newMaterials;
        }
    }

    private void OnMouseExit()
    {
        meshRenderer.materials = originalMaterials;
    }
}
