using TMPro;
using UnityEngine;

public class TitleStartText : MonoBehaviour
{
    public float duration = 0.35f;

    private CanvasGroup canvasGroup;
    private float lerpTime;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (text != null)
        {
            canvasGroup = text.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = text.gameObject.AddComponent<CanvasGroup>();
            }
        }
    }

    void Update()
    {
        if (canvasGroup != null)
        {
            lerpTime += Time.deltaTime / duration;

            float alpha = Mathf.PingPong(lerpTime, 1f);

            canvasGroup.alpha = Mathf.Lerp(0.3f, 1f, alpha);
            text.color = Color.Lerp(new Color(0.7f, 0.7f, 0.7f), Color.white, alpha);
        }
    }
}