using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartBtn : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
