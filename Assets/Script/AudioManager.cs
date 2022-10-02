using BaseTemplate.Behaviours;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    public int musicIndex = 0;
    public AudioMixerGroup soundEffectMixer;


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

    private void PlayNextSong()
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
