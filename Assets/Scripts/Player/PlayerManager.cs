using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IDamagable
{
    [SerializeField] float playerHealth;
    public Slider healthBar;
    public GameObject deathPanel;
    public GameObject hud;
    public Action DamageTaken;

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        healthBar.value = playerHealth;

        if (playerHealth <= 0)
            Die();

        DamageTaken?.Invoke();
    }

    private void Die()
    {
        deathPanel.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0f;
    }
}
