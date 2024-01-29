using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip mainBGClip;
    [SerializeField] private AudioClip deckBGClip;
    [SerializeField] private AudioClip blastClip;
    [SerializeField] private AudioClip cardSelectClip;
    [SerializeField] private AudioClip deckSelectClip;
    [SerializeField] private AudioClip levelUnlockClip;
    [SerializeField] private AudioClip playButtonClip;
    [SerializeField] private AudioClip notificationClip;
    [SerializeField] private AudioClip handPlayStart;
    [SerializeField] private AudioClip countdownSFX;

    [SerializeField] private AudioSource sourceBGM;
    [SerializeField] private AudioSource sourceBGMFX;
    [SerializeField] private AudioSource sourceSFX;
    [SerializeField] private AudioSource sourceNotification;

    [SerializeField] private float bgmVolume = 1f;
    [SerializeField] private float bgmFXVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private float notificationVolume = 1f;
    [SerializeField] private float fadeDuration = 5f;

    private void Start()
    {
        sourceSFX.volume = sfxVolume;
        sourceNotification.volume = notificationVolume;
        sourceBGMFX.loop = true;
        sourceBGMFX.volume = bgmFXVolume;
        sourceBGM.loop = true;
        sourceBGM.volume = bgmVolume;
        sourceBGM.Play();
    }

    
    public void ChangeBGMVolume(float volume)
    {
        FadeVolume(sourceBGM, volume);
    }

    // Summary:
    //     The volume of the audio source (0.0 to 1.0).
    private void FadeVolume(AudioSource source, float toVolume, float initialdelay = 0f)
    {
        StartCoroutine(FadeVolumeEffect(source, toVolume, initialdelay));
    }

    private IEnumerator FadeVolumeEffect(AudioSource source, float toVolume, float initialdelay = 0f)
    {
        yield return new WaitForSeconds(initialdelay);
        float time = 0.05f;
        for (float t = 0; t < fadeDuration; t += time)
        {
            float normalizedTime = t / fadeDuration;
            source.volume = Mathf.Lerp(source.volume, toVolume, normalizedTime);
            yield return new WaitForSeconds(time);
        }
        source.volume = toVolume;
    }

    public void PlaySFX(AudioClipID clipID)
    {
        sourceSFX.PlayOneShot(GetAudioClip(clipID));
    }

    public void PlayNotification(AudioClipID clipID)
    {
        sourceNotification.PlayOneShot(GetAudioClip(clipID));
    }

    public void PlayBGMFX(AudioClipID clipID)
    {
        sourceBGMFX.clip = GetAudioClip(clipID);
        sourceBGMFX.volume = 0f;
        sourceBGMFX.Play();
        FadeVolume(sourceBGMFX, bgmFXVolume);
        FadeVolume(sourceBGM, bgmVolume/2);
    }

    public void StopBGMFX()
    {
        sourceBGMFX.Stop();
        FadeVolume(sourceBGMFX, 0);
        FadeVolume(sourceBGM, bgmVolume);
    }

    private AudioClip GetAudioClip(AudioClipID clipID)
    {
        switch (clipID)
        {
            case AudioClipID.MainBG: return mainBGClip;
            case AudioClipID.DeckBG: return deckBGClip;
            case AudioClipID.Blast: return blastClip;
            case AudioClipID.PlaySelect: return playButtonClip;
            case AudioClipID.CardSelect: return cardSelectClip;
            case AudioClipID.DeckSelect: return deckSelectClip;
            case AudioClipID.Notification: return notificationClip;
            case AudioClipID.LevelUnlock: return levelUnlockClip;
            case AudioClipID.HandPlayStart: return handPlayStart;
            case AudioClipID.CountdownSFX: return countdownSFX;
        }
        Debug.LogError("ERNOS : Audio for clipID not found.");
        return null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        if (sourceBGMFX != null) { sourceBGMFX.Stop(); }
        if (sourceSFX != null) { sourceSFX.Stop(); }
        if (sourceNotification != null) { sourceNotification.Stop(); }
    }
}

public enum AudioClipID
{
    MainBG,
    DeckBG,
    Blast,
    CardSelect,
    DeckSelect,
    PlaySelect,
    Notification,
    LevelUnlock,
    HandPlayStart,
    CountdownSFX
}
