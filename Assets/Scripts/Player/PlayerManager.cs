using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    [SerializeField] float playerHealth;
    public GameObject deathPanel;
    public GameObject tutorialTxt;
    public Action DamageTaken;

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
            Die();

        DamageTaken?.Invoke();
    }

    private void Die()
    {
        deathPanel.SetActive(true);
        tutorialTxt.SetActive(false);
        Time.timeScale = 0f;
    }
}
