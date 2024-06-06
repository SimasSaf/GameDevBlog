using UnityEngine;

public class AudioManager : MonoBehaviour, IMusicManager, ISoundEffectManager
{
    public AudioSource mainMenuMusic;
    public AudioSource bossMusic;
    public AudioSource soundEffect;

    private void Start()
    {
        SetInitialVolume();
    }

    private void SetInitialVolume()
    {
        SetVolume(0.5f);
    }

    public void SetVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp01(volume);
        mainMenuMusic.volume = clampedVolume;
        bossMusic.volume = clampedVolume;
        soundEffect.volume = clampedVolume;
    }

    public void PlayMainMenuMusic()
    {
        bossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayBossMusic()
    {
        mainMenuMusic.Stop();
        bossMusic.Play();
    }

    public void ShootSoundEffect()
    {
        soundEffect.PlayOneShot(soundEffect.clip);
    }
}
