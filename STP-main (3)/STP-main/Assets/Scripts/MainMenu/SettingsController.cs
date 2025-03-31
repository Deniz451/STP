using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject MainMenuPanel;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SettingsPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }
    }
}
