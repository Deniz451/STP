using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private GameObject player;
    private Vector3 originalPosition;
    private bool isShaking = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerHealth>().DamageTaken += DamageShake;
    }

    private void ShootShake()
    {
        if (!isShaking)
            StartCoroutine(Shake(0.25f, 0.3f));
    }
    private void DamageShake()
    {
        if (!isShaking)
            StartCoroutine(Shake(0.4f, 0.55f));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {

        isShaking = true;
        originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
        isShaking = false;
    }
}
