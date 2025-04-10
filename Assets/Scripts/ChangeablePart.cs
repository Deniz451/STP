using System;
using UnityEngine;

public class ChangeablePart : MonoBehaviour
{
    public Material outliner;
    private MeshRenderer _meshRenderer;
    private Material[] _originalMaterials;
    public bool canSelect = false;

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopOpen, () => canSelect = true);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopClose, () => canSelect = false);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopOpen, () => canSelect = true);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopClose, () => canSelect = false);
    }

    private void Start() {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalMaterials = _meshRenderer.materials;
    }

    private void OnMouseDown() {
 
    }

    private void OnMouseEnter() {
        Debug.Log("Enter");
        if (!canSelect) return;

        Material[] newMaterials = _meshRenderer.materials;
        if (newMaterials.Length > 1)
        {
            newMaterials[1] = outliner;
            _meshRenderer.materials = newMaterials;
        }
    }

    private void OnMouseExit() {        
        if (!canSelect) return;

        _meshRenderer.materials = _originalMaterials;
    }
}
