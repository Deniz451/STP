using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShopItem", fileName = "New Shop Item")]
public class ShopItem : ScriptableObject 
{
    public GunSO weaponInfo;
    public int price;
    public bool unlocked;
}
