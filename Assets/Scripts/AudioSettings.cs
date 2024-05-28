using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            MusicVolume();
            AmbienceVolume();
            SFXVolume();
        }
    }

    public void MusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void AmbienceVolume()
    {
        float volume = ambienceSlider.value;
        audioMixer.SetFloat("Ambience", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("ambienceVolume", volume);
    }

    public void SFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("Sound Effects", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        ambienceSlider.value = PlayerPrefs.GetFloat("ambienceVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        MusicVolume();
        AmbienceVolume();
        SFXVolume();
    }
}
