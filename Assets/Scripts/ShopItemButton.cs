using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemButton : MonoBehaviour, IPointerClickHandler
{
    public int index;
    public Action<int> clickedShopItem;

    public void OnPointerClick(PointerEventData eventData)
    {
        clickedShopItem?.Invoke(index);
    }
}
