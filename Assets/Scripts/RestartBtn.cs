using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartBtn : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
