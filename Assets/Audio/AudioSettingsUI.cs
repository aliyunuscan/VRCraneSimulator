using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        if (masterSlider != null)
        {
            float masterVol = PlayerPrefs.GetFloat("Master", 1f);
            masterSlider.value = masterVol;
            
            masterSlider.onValueChanged.AddListener((v) =>
            {
                AudioManagerVR.Instance.SetMasterVolume(v);
                PlayerPrefs.SetFloat("Master", v);
            });
            
        }

        if (musicSlider != null)
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSlider.value = musicVol;

            musicSlider.onValueChanged.AddListener((v) =>
            {
                AudioManagerVR.Instance.SetMusicVolume(v);
                PlayerPrefs.SetFloat("MusicVolume", v);
            });
        }

        if (sfxSlider != null)
        {
            float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfxSlider.value = sfxVol;

            sfxSlider.onValueChanged.AddListener((v) =>
            {
                AudioManagerVR.Instance.SetSFXVolume(v);
                PlayerPrefs.SetFloat("SFXVolume", v);
            });
        }
    }
}
