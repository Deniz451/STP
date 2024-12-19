using UnityEngine;

public class StartBtn : MonoBehaviour
{
    public event System.Action onGameStart;

    public void StartGame()
    {
        onGameStart?.Invoke();
    }
}
