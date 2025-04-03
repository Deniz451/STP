using System;
using UnityEngine;

[DefaultExecutionOrder(int.MinValue)]
public class GameInitialization : MonoBehaviour
{
    private void Awake()
    {
        foreach (GameEvents.EventType eventType in Enum.GetValues(typeof(EventType))) {
            EventManager.Instance.RegisterEvent(eventType);
        }
    }
}
