using System;
using UnityEngine;

[DefaultExecutionOrder(int.MinValue)]
public class GameInitialization : MonoBehaviour
{
    public Texture2D cursorTEX;

    private void Awake()
    {
        foreach (GameEvents.EventType eventType in Enum.GetValues(typeof(EventType))) {
            EventManager.Instance.RegisterEvent(eventType);
        }

        Cursor.SetCursor(cursorTEX, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
