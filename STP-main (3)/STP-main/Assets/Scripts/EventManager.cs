using System;
using System.Collections.Generic;

public class EventManager
{
    private static EventManager _instance;
    public static EventManager Instance => _instance ??= new EventManager();

    private Dictionary<GameEvents.EventType, Action> eventDictionary = new();

    private EventManager() { }

    public void RegisterEvent(GameEvents.EventType eventName)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = null;
        }
    }

    public void Subscribe(GameEvents.EventType eventName, Action listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] += listener;
        }
    }

    public void Unsubscribe(GameEvents.EventType eventName, Action listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
    }

    public void Publish(GameEvents.EventType eventName)
    {
        if (eventDictionary.ContainsKey(eventName) && eventDictionary[eventName] != null)
        {
            eventDictionary[eventName]?.Invoke();
        }
    }
}
