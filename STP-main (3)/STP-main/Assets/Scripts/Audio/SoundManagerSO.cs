using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SoundManager", fileName = "Sound Manager")]
public class SoundManagerSO : ScriptableObject
{
    private static SoundManagerSO instance;
    public static SoundManagerSO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<SoundManagerSO>("SoundManager");
            }
            return instance;
        }
    }

    [SerializeField] private AudioSource SFXObject;
    private static float pitchChangeMultiplier = 0.1f;
    private static float volumeChangeMultiplier = 0.15f;

    public static void PlaySFXClip(AudioClip audioClip, Vector3 position, float volume)
    {
        float randVolume = Random.Range(volume - volumeChangeMultiplier, volume + volumeChangeMultiplier);
        float randPitch = Random.Range(1 -  pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        AudioSource a = Instantiate(Instance.SFXObject, position, Quaternion.identity);

        a.clip = audioClip;
        a.volume = randVolume;
        a.pitch = randPitch;
        a.Play();
    }

    public static void PlaySFXClip(AudioClip[] audioClip, Vector3 position, float volume)
    {
        int randIndex = Random.Range(0, audioClip.Length);
        float randVolume = Random.Range(volume - volumeChangeMultiplier, volume + volumeChangeMultiplier);
        float randPitch = Random.Range(1 -  pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        AudioSource a = Instantiate(Instance.SFXObject, position, Quaternion.identity);

        a.clip = audioClip[randIndex];
        a.volume = randVolume;
        a.pitch = randPitch;
        a.Play();
    }
}
