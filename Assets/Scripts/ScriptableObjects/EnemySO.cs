using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    public float health;

    [Header("Attack Stats")]
    public float damage;
    public float attackDistance;
    public float attackDelay; // time that the enemy waits after entering attack state and before attacking (give player chance to escape attack)
    public float attackCooldown; // time that the enemy must wait before performnig 2nd, 3rd, ... attack in a row

    [Header("Projectiles")]
    public GameObject projectilePrefab;

    [Header("Materials")]
    public Material dissolveMaterial;
    public float dissolveDuration;
}
