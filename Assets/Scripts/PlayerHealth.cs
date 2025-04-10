using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public GameObject healthGrid;
    public Action DamageTaken;
    public event Action OnDeath;
    private float _health = 10;
    private List<GameObject> healthBarImgs = new();

    private void Start() {
        for (int i = 0; i < _health; i++) {
            GameObject healthBarImg = new() { name = "HealthBarImg" };
            healthBarImg.transform.SetParent(healthGrid.transform);
            healthBarImg.AddComponent<Image>().color = Color.red;
            healthBarImg.GetComponent<RectTransform>().localScale = Vector3.one;
            healthBarImgs.Add(healthBarImg);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= (int)damage;

        for (int i = 0; i < (int)damage; i++) {
            Destroy(healthBarImgs[^1]);
             healthBarImgs.RemoveAt(healthBarImgs.Count - 1);
        }

        if (_health <= 0)
            Die();

        DamageTaken?.Invoke();
    }

    private void Die() {
        OnDeath?.Invoke();
        EventManager.Instance.Publish(GameEvents.EventType.PlayerDeath);
    }
}
