using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, EnablePlayer);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, EnablePlayer);
    }

    private void EnablePlayer() {
        EventManager.Instance.Publish(GameEvents.EventType.PlayerEnabled);
    }

    private void DisablePlayer() {

    }
}
