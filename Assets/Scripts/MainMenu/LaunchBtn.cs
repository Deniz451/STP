using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchBtn : MonoBehaviour
{
   public void Launch()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
