using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Player Part Holders")]
    public GameObject gunLPlayer;
    public GameObject gunRPlayer;


    [Header("Shop Camera Anchors")]
    public Transform gunLCamera;
    public Transform gunRCamera;

    [Header("UI Text")]
    public TextMeshProUGUI weapon1PriceTxt;
    public TextMeshProUGUI weapon2PriceTxt;
    public TextMeshProUGUI weapon3PriceTxt;
    public Image weapon1SpriteImg;
    public Image weapon2SpriteImg;
    public Image weapon3SpriteImg;

    public List<ShopItem> weapons = new();
    private bool isZoomedToPart = false;
    private bool gunLSelected = false;
    private bool gunRSelected = false;  
    public GameObject[] itemButtons;


    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.WaveEnd, OpenShop);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopContinueBtnClick, ShopClose);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.WaveEnd, OpenShop);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopContinueBtnClick, ShopClose);
    }

    private void Start() {
        gunLPlayer.GetComponent<PlayerPartHolder>().OnChangeablePartClicked += () => { 
            isZoomedToPart = true; 
            gunLSelected = true;
            gunRSelected = false;
            EventManager.Instance.Publish(GameEvents.EventType.SelectedGunL); 
            EventManager.Instance.Publish(GameEvents.EventType.ClickedChangeablePart); 
            DisplayShopUI();
        };

        gunRPlayer.GetComponent<PlayerPartHolder>().OnChangeablePartClicked += () => { 
            isZoomedToPart = true; 
            gunLSelected = false;
            gunRSelected = true;
            EventManager.Instance.Publish(GameEvents.EventType.SelectedGunR); 
            EventManager.Instance.Publish(GameEvents.EventType.ClickedChangeablePart); 
            DisplayShopUI();
        };

        foreach (var button in itemButtons) {
            button.GetComponent<ShopItemButton>().clickedShopItem += TryToBuy;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isZoomedToPart) {
            isZoomedToPart = false;
            EventManager.Instance.Publish(GameEvents.EventType.ShopRightClicked);
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isZoomedToPart) {
            ShopClose();
        }
    }

    private void OpenShop() {
        EventManager.Instance.Publish(GameEvents.EventType.ShopOpen);
    }

    private void ShopClose() {
        EventManager.Instance.Publish(GameEvents.EventType.ShopClose);
    }

    private void DisplayShopUI() {
        if (weapons[0].unlocked) weapon1PriceTxt.text = "UNLOCKED";
        else weapon1PriceTxt.text = weapons[0].price.ToString();

        if (weapons[1].unlocked) weapon2PriceTxt.text = "UNLOCKED";
        else weapon2PriceTxt.text = weapons[1].price.ToString();

        if (weapons[2].unlocked) weapon3PriceTxt.text = "UNLOCKED";
        else weapon3PriceTxt.text = weapons[2].price.ToString();
    }

    private void TryToBuy(int itemIndex) {
        if (weapons[itemIndex].unlocked) Equip(weapons[itemIndex].weaponInfo);
        else {
            float currencyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().CurrenctyAmount;
            if (currencyAmount >= weapons[itemIndex].price) { 
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().CurrenctyAmount -= weapons[itemIndex].price;
                BuyItem(weapons[itemIndex]);
            }
        }
    }

    private void BuyItem(ShopItem item) {
        item.unlocked = true;
        DisplayShopUI();
    }

    private void Equip(GunSO weapon) {
        if (gunLSelected) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().EquipWeaponL(weapon);
        if (gunRSelected) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().EquipWeaponR(weapon);
    }
}
