using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameStarted = false;


    private void Update() {
        if (Input.anyKey && !gameStarted)
            StartGame();
    }

    private void StartGame() {
        gameStarted = true;
        EventManager.Instance.Publish(GameEvents.EventType.GameStart);
    }
}
