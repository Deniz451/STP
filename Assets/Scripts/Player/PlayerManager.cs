using System;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GunSO _weaponL;
    public GunSO WeaponL {
        get => _weaponL;
        set {
            if (_weaponL != value) {
                _weaponL = value;
            }
        }
    }

    public GunSO _weaponR;
    public GunSO WeaponR {
        get => _weaponR;
        set {
            if (_weaponR != value) {
                _weaponR = value;
            }
        }
    }

    private int _currenctyAmount = 999;
    public int CurrenctyAmount {
        get => _currenctyAmount;
        set {
            if (_currenctyAmount != value) {
                _currenctyAmount = value;
                UpdateCurrencyText(_currenctyAmount);
            }
        }
    }

    public Action<GunSO, GunSO> switchWeapons;
    public TextMeshProUGUI currencyAmount;


    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GamePause, DisablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.GameResume, EnablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopOpen, DisablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopClose, EnablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDeath, DisablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.CameraInGameEvent, EnablePlayer);
        EventManager.Instance.Subscribe(GameEvents.EventType.CameraToShopEvent, DisablePlayer);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GamePause, DisablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameResume, EnablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopOpen, DisablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopClose, EnablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDeath, DisablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.CameraInGameEvent, EnablePlayer);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.CameraToShopEvent, DisablePlayer);
    }

    private void Start() {
        switchWeapons?.Invoke(_weaponL, _weaponR);
    }

    private void EnablePlayer() {
        EventManager.Instance.Publish(GameEvents.EventType.PlayerEnabled);
    }

    private void DisablePlayer() {
        EventManager.Instance.Publish(GameEvents.EventType.PlayerDisabled);
    }

    private void UpdateCurrencyText(int amount) {
        currencyAmount.text = amount.ToString();
    }

    public void EquipWeaponL(GunSO weapon) {
        switchWeapons?.Invoke(weapon, _weaponR);
    }

    public void EquipWeaponR(GunSO weapon) {
        switchWeapons?.Invoke(_weaponL, weapon);
    }
}
