using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameInfo gameInfo;
    private bool _gameStarted = false;
    private bool _gamePaused = false;

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameReload, ReloadGame);
        EventManager.Instance.Subscribe(GameEvents.EventType.ResumeBtnClick, ResumeGame);
        EventManager.Instance.Subscribe(GameEvents.EventType.ExitBtnClick, ReloadGame);  
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDeath, PauseGame);                                    
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameReload, ReloadGame);        
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ResumeBtnClick, ResumeGame);    
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ExitBtnClick, ReloadGame);     
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDeath, PauseGame);  
    }

    private void Update() {
        if (Input.anyKey && !_gameStarted)
            StartGame();

        if (Input.GetKeyDown(KeyCode.Escape) && _gameStarted) {
            if (!_gamePaused) PauseGame();
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
        _gamePaused = true;
    }

    private void ResumeGame() {
        EventManager.Instance.Publish(GameEvents.EventType.GameResume);
        Time.timeScale = 1.0f;
        _gamePaused = false;
    }

    private void ReloadGame() {
        SceneManager.LoadSceneAsync(0);
    }
}
