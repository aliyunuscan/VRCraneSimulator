using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManagerVR : MonoBehaviour
{
    public static AudioManagerVR Instance;

    [Header("Mixer Setup")]
    public AudioMixer audioMixer;

    [Header("Mixer Groups")]
    public AudioMixerGroup masterGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Audio Sources")]
    public AudioSource musicSourcePrefab;
    public AudioSource sfxSourcePrefab;

    [Header("Pool Settings")]
    public int sfxPoolSize = 10;

    private List<AudioSource> sfxPool;

    public AudioClip musicClip;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitSFXPool();

        PlayMusic(musicClip);
    }

    private void InitSFXPool()
    {
        sfxPool = new List<AudioSource>();
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource source = Instantiate(sfxSourcePrefab, transform);
            source.outputAudioMixerGroup = sfxGroup;
            source.spatialBlend = 1f; // 3D sound
            source.playOnAwake = false;
            sfxPool.Add(source);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        AudioSource music = Instantiate(musicSourcePrefab, transform);
        music.clip = clip;
        music.loop = true;
        music.outputAudioMixerGroup = musicGroup;
        music.spatialBlend = 0f; // 2D music
        music.Play();
    }

    public void PlaySFX(AudioClip clip, Vector3 position, float spatialBlend = 1f)
    {
        AudioSource source = GetAvailableSFXSource();
        if (source == null) return;

        source.transform.position = position;
        source.clip = clip;
        source.spatialBlend = spatialBlend; // 1 = 3D, 0 = 2D
        source.Play();
    }

    public void PlaySFX2D(AudioClip clip, float spatialBlend = 0f)
    {
        AudioSource source = GetAvailableSFXSource();
        if (source == null) return;

        source.clip = clip;
        source.spatialBlend = spatialBlend; // 1 = 3D, 0 = 2D
        source.Play();
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in sfxPool)
        {
            if (!source.isPlaying)
                return source;
        }
        return null; // no available source
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20);
    }
}
