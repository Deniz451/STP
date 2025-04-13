using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon", fileName = "New Weapon")]
public class GunSO : ScriptableObject
{
    public float damage;
    public float bulletLifetime;
    public float attackDelay;
    public float bulletSpeed;
    public GameObject projectilePrefab;
    public GameObject model;
    public GameObject prefab;
    public AudioClip fireClip;
}