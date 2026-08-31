// Copyright (c) 2012-2024 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundConfig _soundConfig;
    [SerializeField] private AudioSource _effectsAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioMixerGroup _mixerVolume;
    
    private const float OffSoundVariable = -80f;
    private const string SoundMixerName = "effects";

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        var musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        var soundEffectsEnabled = PlayerPrefs.GetInt("SoundEffectsEnabled", 1) == 1;
        
        SetMusicEnabled(musicEnabled);
        SetSoundEffectsEnabled(soundEffectsEnabled);
    }

    public void PlaySoundEffect(string soundName)
    {
        if (_soundConfig.SoundDictionary.ContainsKey(soundName))
        {
            var soundClip = _soundConfig.SoundDictionary[soundName];
            _effectsAudioSource.PlayOneShot(soundClip);
        }
        else
            Debug.LogWarning("Sound not found: " + soundName);
    }

    public void PlayMusic(string musicName)
    {
        if (_soundConfig.SoundDictionary.ContainsKey(musicName))
        {
            var soundClip = _soundConfig.SoundDictionary[musicName];
            _musicAudioSource.clip = soundClip;
            _musicAudioSource.Play();
        }
        else
            Debug.LogWarning("Music not found: " + musicName);
    }

    public void SetSoundEffectsEnabled(bool isEnabled)
    {
        var volume = isEnabled ? 0f : OffSoundVariable;
        _mixerVolume.audioMixer.SetFloat(SoundMixerName, volume);
        PlayerPrefs.SetInt("SoundEffectsEnabled", isEnabled ? 1 : 0);
        PlayerPrefs.Save(); 
    }

    public void SetMusicEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            _musicAudioSource.volume = 1f; 
            PlayerPrefs.SetInt("MusicEnabled", 1);
        }
        else
        {
            _musicAudioSource.volume = 0f;
            PlayerPrefs.SetInt("MusicEnabled", 0);
        }
        PlayerPrefs.Save(); 
    }

    public void ResetOptions()
    {
        PlayerPrefs.DeleteKey("MusicEnabled");
        PlayerPrefs.DeleteKey("SoundEffectsEnabled");
        PlayerPrefs.Save(); 
        
        SetMusicEnabled(true);
        SetSoundEffectsEnabled(true);
    }
}