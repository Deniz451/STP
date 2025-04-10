using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopContinueBtn : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public TMP_FontAsset defaultFont;
    public TMP_FontAsset hoverFont;

    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager.Instance.Publish(GameEvents.EventType.ShopContinueBtnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.font = hoverFont;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.font = defaultFont;
    }
}
