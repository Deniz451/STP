using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    private void Update() {
        if (Input.anyKey) {
            EventManager.Instance.Publish(GameEvents.EventType.GameReload);
        }
    }
}
