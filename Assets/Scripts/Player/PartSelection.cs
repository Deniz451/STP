using UnityEngine;

public class PartSelection : MonoBehaviour
{
    public CameraController cameraController;
    MeshRenderer meshRenderer;
    public Material outliner;
    private Material[] originalMaterials;
    public CameraController.CameraPoints cameraPos;
    public Transform[] allParts;
    public GameObject[] btns;
    public GameObject[] availableParts;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterials = meshRenderer.materials;
    }

    private void OnMouseDown()
    {
        cameraController.LerpCameraPos(2, cameraPos, 1.75f);
        foreach (var part in allParts) part.GetComponent<BoxCollider>().enabled = true;
        GetComponent<BoxCollider>().enabled = false;
        foreach (var btn in btns) btn.SetActive(false);
        DisplayParts();
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

    private void DisplayParts()
    {
        Vector3 pos = new(transform.position.x + 2, transform.position.y, transform.position.z);
        GameObject part = Instantiate(availableParts[1], pos, Quaternion.identity);
    }
}
