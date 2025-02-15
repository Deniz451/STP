using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "ScriptableObjects/Gun", order = 2)]
public class GunSO : ScriptableObject
{
    [Header("Name")]
    public string gunName;

    [Space(30)]
    [Header("Attack Stats")]
    public float damage;
    public float bulletLifetime;
    public float attackDelay;
    public float bulletSpeed;

    [Space(5)]
    [Header("Bullet projectile")]
    public GameObject projectilePrefab; //must have a child named "BulletSpawn"

    [Space(30)]
    [Header("Movement Stats")]
    public float moveModifier;

    [Space(30)]
    [Header("Visuals")]
    public Sprite gunIcon;
    public GameObject gunPrefab;
    public Quaternion gunRotation;

    [Space(30)]
    [Header("Audio")]
    public AudioClip fire;
}