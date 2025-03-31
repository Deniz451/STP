using UnityEngine;
//using UnityEditor.Animations;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    [Header("Health Stats")]
    public float health;

    [Space(30)]
    [Header("Attack Stats")]
    public float damage;
    public float attackDistance;
    public float attackDelay; // time that the enemy waits after entering attack state and before attacking (give player chance to escape attack)
    public float attackCooldown; // time that the enemy must wait before performing 2nd, 3rd, ... attack in a row

    [Space(5)]
    [Header("Range Attack")]
    public GameObject projectilePrefab;

    [Space(5)]
    [Header("Melee Attack")]
    public float dashAttackDuration;
    public float dashAttackForce;
    public float maxDashSpeed;

    [Space(30)]
    [Header("Movement Stats")]
    public float moveSpeed;

    [Space(30)]
    [Header("Materials")]
    public Material dissolveMaterial;
    public float dissolveDuration;

    [Space(30)]
    [Header("Audio")]
    public AudioClip hit;

    /*[Space(30)]
    [Header("Animator")]
    public AnimatorController animatorController;*/
}
