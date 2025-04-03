using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject titleCanvas;

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, EnableUICanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, DisableTitleCanvas);
    }

    private void OnDestroy() {

    }

    private void EnableUICanvas() {
        uiCanvas.SetActive(true);
    }

    private void DisableUICanvas() {
        uiCanvas.SetActive(false);
    }

    private void EnableTitleCanvas() {
        titleCanvas.SetActive(true);
    }

    private void DisableTitleCanvas() {
        titleCanvas.SetActive(false);
    }
}
