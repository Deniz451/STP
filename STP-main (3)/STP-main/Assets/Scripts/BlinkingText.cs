using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    public Color colorA;
    public Color colorB;
    public float duration;
    private float _elapsedTime = 0f;
    private TextMeshProUGUI _text;
    private bool _blink;
    public bool Blink 
    {
        get => _blink;
        set
        {
            if (_blink != value)
            {
                _blink = value;
                if (!_blink) ResetText(); 
            }
        }
    }

    private void Start() {
        _text = GetComponent<TextMeshProUGUI>();
        Blink = true;
    }

    void Update()
    {
        if (_text == null || !Blink) return;

        _elapsedTime += Time.unscaledDeltaTime;

        float t = Mathf.PingPong(_elapsedTime / duration, 1f);
        _text.color = Color.Lerp(colorA, colorB, t);
    }

    private void ResetText() {
        _text.color = colorA;
    }
}