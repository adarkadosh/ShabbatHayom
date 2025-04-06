using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip playerHitSound;
    [SerializeField] private AudioClip itemSpawnSound;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip timerSound;
    [SerializeField] private AudioClip togglePauseSound;
    private float pitchIncrement = 0.1f;

    

    private AudioSource backgroundMusicSource;
    private AudioSource soundEffectsSource;

    private void Awake()
    {
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        soundEffectsSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // GameManager.Instance.GameStart += OnGameStart;
        // GameManager.Instance.GamePause += OnGamePause;
        // GameManager.Instance.GameResume += OnGamePause;
        OnGameStart(); //Move to the game manager
        GameEvents.OnProductCollected += OnItemSpawned;
        
        
    }
    
    private void OnDifficultyChanged()
    {
        backgroundMusicSource.pitch += pitchIncrement;
    }

    private void OnGameStart()
    {
        PlayBackgroundMusic(backgroundMusic);
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        backgroundMusicSource.clip = clip;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }
    
    private void OnGamePause()
    {
        PlaySoundEffect(togglePauseSound);
    }
    
    private void OnItemSpawned()
    {
        PlaySoundEffect(itemSpawnSound);
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        soundEffectsSource.PlayOneShot(clip);
    }

    private void OnDisable()
    {
        // GameManager.Instance.GameStart -= OnGameStart;
        // GameManager.Instance.GamePause -= OnGamePause;
        // GameManager.Instance.GameResume -= OnGamePause;
        GameEvents.OnProductCollected -= OnItemSpawned;
    }
}