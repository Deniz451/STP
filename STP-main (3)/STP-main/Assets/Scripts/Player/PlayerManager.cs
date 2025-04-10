using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, EnablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.GamePause, DisablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.GameResume, EnablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopOpen, DisablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopClose, EnablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDeath, DisablePlayer);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, EnablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GamePause, DisablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameResume, EnablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopOpen, DisablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopClose, EnablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDeath, DisablePlayer);
    }

    private void EnablePlayer() {
        EventManager.Instance.Publish(GameEvents.EventType.PlayerEnabled);
    }

    private void DisablePlayer() {
        EventManager.Instance.Publish(GameEvents.EventType.PlayerDisabled);
    }
}
