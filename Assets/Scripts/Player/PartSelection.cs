using UnityEngine;

public class PartSelection : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public Material outliner;
    private Material[] originalMaterials;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterials = meshRenderer.materials;
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
