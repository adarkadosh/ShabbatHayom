﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip playerHitSound;
    [SerializeField] private AudioClip itemSpawnSound;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip timerSound;
    [SerializeField] private AudioClip buttonPressedSound;
    [SerializeField] private AudioClip itemScannedSound;
    [SerializeField] private AudioClip gameBeginSound;
    private float pitchIncrement = 0.05f;

    private AudioSource backgroundMusicSource;
    private AudioSource soundEffectsSource;
    private static bool isBackgroundMusicPlaying = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        soundEffectsSource = gameObject.AddComponent<AudioSource>();
        OnGameStart();
    }

    private void OnEnable()
    {
        // GameManager.Instance.GameStart += OnGameStart;
        // GameManager.Instance.GamePause += OnGamePause;
        // GameManager.Instance.GameResume += OnGamePause;
        PauseMenu.OnResumeGame += OnButtonPressed;
        PauseMenu.OnPauseGame += OnButtonPressed;
        PauseMenu.OnLoadMenu += OnButtonPressed;
        MainMenu.OnLoadGame += OnPressedStart;
        PoolableItem.ItemScanned += OnItemScanned;
        GameEvents.OnSpeedUp += OnDifficultyChanged;
        GameEvents.OnProductCollected += OnItemSpawned;
        GameEvents.OnObstacleHit += OnPlayerHitSound;
        GameEvents.OnGameRestart += OnGameRestart;
        // GameEvents.OnPauseGame += OnPause;
    }

    // private void OnPause()
    // {
    //     if (isBackgroundMusicPlaying)
    //     {
    //         backgroundMusicSource.Pause();
    //         isBackgroundMusicPlaying = false;
    //     } else 
    //     {
    //         backgroundMusicSource.UnPause();
    //         isBackgroundMusicPlaying = true;
    //     }
    // }

    private void OnDifficultyChanged()
    {
        backgroundMusicSource.pitch += pitchIncrement;
    }

    private void OnPressedStart()
    {
        StartCoroutine(PlayStartGameSoundAndStartGame());
    }

    private IEnumerator PlayStartGameSoundAndStartGame()
    {
        PlaySoundEffect(gameBeginSound);
        backgroundMusicSource.Pause();
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(gameBeginSound.length);
        Time.timeScale = 1f;
        backgroundMusicSource.Play();
    }

    private void OnGameStart()
    {
        if (!isBackgroundMusicPlaying)
        {
            PlayBackgroundMusic(backgroundMusic);
            isBackgroundMusicPlaying = true;
        }
    }
    
    private void OnGameRestart()
    {
        if (isBackgroundMusicPlaying)
        {
            backgroundMusicSource.Play();
            isBackgroundMusicPlaying = true;
        }
        backgroundMusicSource.pitch = 1f;
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        backgroundMusicSource.clip = clip;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }


    public void OnButtonPressed()
    {
        PlaySoundEffect(buttonPressedSound);
    }

    private void OnItemScanned()
    {
        PlaySoundEffect(itemScannedSound);
    }

    private void OnItemSpawned(Products product)
    {
        PlaySoundEffect(itemSpawnSound);
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        soundEffectsSource.PlayOneShot(clip);
    }

    private void OnPlayerHitSound()
    {
        PlaySoundEffect(playerHitSound);
    }

    private void OnDisable()
    {
        // GameManager.Instance.GameStart -= OnGameStart;
        // GameManager.Instance.GamePause -= OnGamePause;
        // GameManager.Instance.GameResume -= OnGamePause;
        PauseMenu.OnResumeGame -= OnButtonPressed;
        PauseMenu.OnPauseGame -= OnButtonPressed;
        PauseMenu.OnLoadMenu -= OnButtonPressed;
        GameEvents.OnProductCollected -= OnItemSpawned;
        GameEvents.OnObstacleHit -= OnPlayerHitSound;
        GameEvents.OnGameRestart -= OnGameRestart;
    }
}