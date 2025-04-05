using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{ 
    public Volume globalVolume;
    private ChromaticAberration chromaticAberration;
    private float _duration = 0.2f;

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDashStart, ShowChromaticAberrationCaller);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDashEnd, HideChromaticAberrationCaller);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDashStart, ShowChromaticAberrationCaller);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDashEnd, HideChromaticAberrationCaller);
    }

    private void Start() {
        globalVolume.profile.TryGet(out chromaticAberration);
    }

    private void ShowChromaticAberrationCaller() { StartCoroutine(ShowChromaticAberration()); }

    private void HideChromaticAberrationCaller() { StartCoroutine(HideChromaticAberration()); }

    private IEnumerator ShowChromaticAberration() {
        float _elapsedTime = 0;

        while (_elapsedTime < _duration) {
            chromaticAberration.intensity.value = _elapsedTime / _duration;
            _elapsedTime += Time.deltaTime;
            yield return null;
        }   

        chromaticAberration.intensity.value = 1;
    }

    private IEnumerator HideChromaticAberration() {
        float _elapsedTime = 0;

        while (_elapsedTime < _duration) {
            chromaticAberration.intensity.value = 1 - (_elapsedTime / _duration);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }   

        chromaticAberration.intensity.value = 0;
    }
}
