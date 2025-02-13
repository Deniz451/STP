using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    [SerializeField] float playerHealth;
    public GameObject deathPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
