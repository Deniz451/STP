using UnityEngine;

public class ShopManager : MonoBehaviour
{

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.WaveEnd, OpenShop);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopContinueBtnClick, ShopClose);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.WaveEnd, OpenShop);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopContinueBtnClick, ShopClose);
    }

    private void OpenShop() {
        EventManager.Instance.Publish(GameEvents.EventType.ShopOpen);
    }

    private void ShopClose() {
        EventManager.Instance.Publish(GameEvents.EventType.ShopClose);
    }
}
