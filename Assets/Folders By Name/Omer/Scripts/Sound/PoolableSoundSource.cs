using System;
using System.Collections;

using UnityEngine;

public class PoolableSoundSource : MonoBehaviour, IPoolable
{
    
    [SerializeField]
    private AudioSource audioSource;

    private void OnEnable()
    {
        // GameManager.Instance.GamePause += PauseSound;
        // GameManager.Instance.GameResume += ResumeSound;
        // GameManager.Instance.GameOver += OnGameOver;
        // GameManager.Instance.PlayerHit += OnGameOver;
    }

    // PoolableSoundSource Logic
    public void SetUpClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void PlayAudioSource()
    {
        audioSource.Play();
        float duration = audioSource.clip.length;
        StartCoroutine(ReturnToPoolAfterDuration(duration));
    }
    
    public void PlayLoopSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private IEnumerator ReturnToPoolAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        SoundPool.Instance.Return(this);
    }
    
    private void PauseSound()
    {
        audioSource.Pause();
    }
    
    private void ResumeSound()
    {
        audioSource.UnPause();
    }
    
    private void OnGameOver()
    {
        SoundPool.Instance.Return(this);
    }
    
    public void Reset()
    {
        audioSource.enabled = true;
        audioSource.clip = null;
        audioSource.loop = false;
        audioSource.transform.position = Vector3.zero;
    }
    
}