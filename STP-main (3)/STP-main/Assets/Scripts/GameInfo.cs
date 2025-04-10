using UnityEngine;

[CreateAssetMenu(menuName = "Game Info", fileName = "New Game Info")]
public class GameInfo : ScriptableObject
{
    public int waveCount;
    public bool gameStarted;
}
