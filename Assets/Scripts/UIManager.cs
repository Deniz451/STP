using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameInfo gameInfo;
    public GameObject uiCanvas;
    public GameObject titleCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject shopCanvas;
    public GameObject deathCanvas;
    public TextMeshProUGUI waveTxt;
    public TextMeshProUGUI deathTxt1;
    public TextMeshProUGUI deathTxt2;
    public GameObject shopContinueText;
    public GameObject shopBackText;

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, EnableUICanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, DisableTitleCanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.GamePause, EnablePauseMenuCanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.GamePause, DisableUICanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.GameResume, DisablePauseMenuCanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.GameResume, EnableUICanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.WaveStart, UpdateWaveText);
        EventManager.Instance.Subscribe(GameEvents.EventType.WaveEnd, DisableUICanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopOpen, EnableShopCanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopClose, DisableShopCanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopClose, EnableUICanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDeath, EnableDeathCanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDeath, DisableUICanvas);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDeath, ShowDeathTextCaller);
        EventManager.Instance.Subscribe(GameEvents.EventType.ClickedChangeablePart, ChangeShopTextToZoomed);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopRightClicked, ChangeShopTextToDefault);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, EnableUICanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, DisableTitleCanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GamePause, EnablePauseMenuCanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GamePause, DisableUICanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameResume, DisablePauseMenuCanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameResume, EnableUICanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.WaveStart, UpdateWaveText);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.WaveEnd, DisableUICanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopOpen, EnableShopCanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopClose, EnableDeathCanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopClose, EnableUICanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDeath, DisableUICanvas);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDeath, ShowDeathTextCaller);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ClickedChangeablePart, ChangeShopTextToZoomed);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopRightClicked, ChangeShopTextToDefault);
    }

    private void ChangeShopTextToDefault() {
        shopContinueText.SetActive(true);
        shopBackText.SetActive(false);
    }

    private void ChangeShopTextToZoomed() {
        shopBackText.SetActive(true);
        shopContinueText.SetActive(false);
    }

    private void EnableUICanvas() {
        uiCanvas.SetActive(true);
    }

    private void DisableUICanvas() {
        uiCanvas.SetActive(false);
    }

    private void DisableTitleCanvas() {
        titleCanvas.SetActive(false);
    }

    private void EnablePauseMenuCanvas() {
        pauseMenuCanvas.SetActive(true);    
    }
    
    private void DisablePauseMenuCanvas() {
        pauseMenuCanvas.SetActive(false);    
    }

    private void EnableShopCanvas() {
        shopCanvas.SetActive(true);    
    }
    
    private void DisableShopCanvas() {
        shopCanvas.SetActive(false);    
    }

    private void EnableDeathCanvas() {
        deathCanvas.SetActive(true);    
    }

    private void UpdateWaveText() {
        if (gameInfo.waveCount < 10) waveTxt.text = "0" + gameInfo.waveCount.ToString();
        else waveTxt.text = gameInfo.waveCount.ToString();
    }

    private void ShowDeathTextCaller() { StartCoroutine(ShowDeathText()); }

    private IEnumerator ShowDeathText() {
        deathTxt1.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -1f);
        deathTxt2.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -1f);
        float elapseTime = 0;
        float duration = 1;

        while (elapseTime < duration) {
            float lerpValue = Mathf.Lerp(-1f, 0f, elapseTime / duration);
            deathTxt1.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, lerpValue);
            deathTxt2.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, lerpValue);
            elapseTime += Time.unscaledDeltaTime;
            yield return null;
        }

        deathTxt1.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0f);
        deathTxt2.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0f);
    }
}
