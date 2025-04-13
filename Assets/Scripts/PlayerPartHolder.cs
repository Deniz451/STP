using System;
using UnityEngine;

public class PlayerPartHolder : MonoBehaviour
{
    private ChangeablePart changeablePart;
    public Action OnChangeablePartClicked;

    private void Start() {
        SubscribeToChild();
    }

    private void SubscribeToChild() {
        changeablePart = transform.GetComponentInChildren<ChangeablePart>();
        if (changeablePart != null) changeablePart.OnClick += () => OnChangeablePartClicked?.Invoke();
    }
}