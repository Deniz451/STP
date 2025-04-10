using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameInfo gameInfo;
    private int _waveCount = 0;
    
    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, StartWave);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopClose, StartWave);
        EventManager.Instance.Subscribe(GameEvents.EventType.AllEnemiesKilled, EndWave);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, StartWave);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopClose, StartWave);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.AllEnemiesKilled, EndWave);
    }

    private void StartWave() {
        _waveCount++;
        gameInfo.waveCount = _waveCount;
        EventManager.Instance.Publish(GameEvents.EventType.WaveStart);
    }

    private void EndWave() {
        EventManager.Instance.Publish(GameEvents.EventType.WaveEnd);
    }
}
