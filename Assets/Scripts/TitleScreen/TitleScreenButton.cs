using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rect;
    private Vector3 originPos;
    private Vector3 desiredPos;
    private Vector3 startPos;
    private float startTime;
    private float lerpDuration = 0.5f;
    private bool lerp = false;


    private void Start()
    {
        rect = GetComponent<RectTransform>();       
        originPos = rect.position; 
    }

    private void Update() 
    {
       if (!lerp) return;
        
        float distCovered = Time.time - startTime;
        float fractionOfJourney = distCovered / lerpDuration;

        float easedFraction = Mathf.SmoothStep(0f, 1f, fractionOfJourney);
        
        rect.position = Vector3.Lerp(startPos, desiredPos, easedFraction);

        if (fractionOfJourney >= 1) lerp = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        startPos = rect.position;
        desiredPos = new (originPos.x + 100, originPos.y, originPos.z);
        startTime = Time.time;
        lerp = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        startPos = rect.position;
        desiredPos = originPos;
        startTime = Time.time;
        lerp = true;
    }
}
