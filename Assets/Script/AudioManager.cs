using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    public int musicIndex = 0;
    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instace AudioManager dans la scène");
            return;
        }
        instance = this;
    }
    public void Start()
    {
        musicIndex = Random.Range(0, playlist.Length);
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    public void PlayNextSong()
    {

        musicIndex = Random.Range(0, playlist.Length);
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject TempGO = new GameObject("TempAudio");
        TempGO.transform.position = pos;
        AudioSource audio = TempGO.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.outputAudioMixerGroup = soundEffectMixer;
        audio.Play();
        Destroy(TempGO, clip.length);
        return audioSource;

    }

}
