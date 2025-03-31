using System.Collections;
using UnityEngine;

public class SoundDestroyer : MonoBehaviour
{
    private AudioSource audioSource;
    private float clipLenght;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Start()
    {
        clipLenght = audioSource.clip.length;
        yield return new WaitForSeconds(clipLenght);
        Destroy(gameObject);
    }
}
