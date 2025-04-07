using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameInfo gameInfo;
    private bool _gameStarted = false;
    private bool _gamePaused = false;

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameReload, ReloadGame);         
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameReload, ReloadGame);            
    }

    private void Update() {
        if (Input.anyKey && !_gameStarted)
            StartGame();

        if (Input.GetKeyDown(KeyCode.Escape) && _gameStarted) {
            if (_gamePaused) ResumeGame();
            else PauseGame();
            _gamePaused = !_gamePaused;
        }
    }

    private void StartGame() {
        EventManager.Instance.Publish(GameEvents.EventType.GameStart);
        _gameStarted = true;
        gameInfo.gameStarted = true;
    }

    private void PauseGame() {
        EventManager.Instance.Publish(GameEvents.EventType.GamePause);
        Time.timeScale = 0.0f;
    }

    private void ResumeGame() {
        EventManager.Instance.Publish(GameEvents.EventType.GameResume);
        Time.timeScale = 1.0f;
    }

    private void ReloadGame() {
        SceneManager.LoadSceneAsync(0);
    }
}
