using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShopItem", fileName = "New Shop Item")]
public class ShopItem : ScriptableObject 
{
    public int index;
    public float price;
    public GameObject model;
}
