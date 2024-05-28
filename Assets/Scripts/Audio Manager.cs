using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource AmbienceSource;
    [SerializeField] AudioSource SFXSourceMap;
    [SerializeField] AudioSource SFXSourcePlayer;
    [SerializeField] AudioSource SFXSourceGoblin;
    [SerializeField] AudioSource GameOverSource;
    
    [Header("---------- Audio Clip ----------")]
    public AudioClip Music;
    public AudioClip Ambience;
    public AudioClip MapSFX0;
    public AudioClip MapSFX1;
    public AudioClip PlayerAttack;
    public AudioClip PlayerDeath;
    public AudioClip GoblinAttack;
    public AudioClip GoblinDeath;
    public AudioClip GoblinExplosion0;
    public AudioClip GoblinExplosion1;

    private void OnEnable()
    {
        MusicSource.clip = Music;
        MusicSource.Play();

        AmbienceSource.clip = Ambience;
        AmbienceSource.Play();

        SFXSourceMap.clip = MapSFX0;
        SFXSourceMap.Play();
    }

    public void PlayPlayerSFX(AudioClip clip)
    {
        SFXSourcePlayer.PlayOneShot(clip);
    }

    public void PlayGoblinSFX(AudioClip clip)
    {
        SFXSourceGoblin.PlayOneShot(clip);
    }

    public void GameOverSFX(AudioClip clip)
    {
        GameOverSource.PlayOneShot(clip);
    }

    public void MuteSources(bool mute)
    {
        SFXSourceMap.mute = mute;
        SFXSourceGoblin.mute = mute;
        
    }

}


