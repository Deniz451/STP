using UnityEngine;
using UnityEngine.UI;

public class BlinkingImage : MonoBehaviour
{
    public Color colorA;
    public Color colorB;
    public float duration;
    private float _elapsedTime = 0f;
    private Image _image;
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
        _image = GetComponent<Image>();
        Blink = true;
    }

    void Update()
    {
        if (_image == null || !Blink) return;

        _elapsedTime += Time.unscaledDeltaTime;

        float t = Mathf.PingPong(_elapsedTime / duration, 1f);
        _image.color = Color.Lerp(colorA, colorB, t);
    }

    private void ResetText() {
        _image.color = colorA;
    }
}
