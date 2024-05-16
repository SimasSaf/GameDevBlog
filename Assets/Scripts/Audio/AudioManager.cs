using UnityEngine;

public class AudioManager : MonoBehaviour, IMusicManager, ISoundEffectManager
{
    public AudioSource mainMenuMusic;
    public AudioSource bossMusic;
    public AudioSource soundEffect;

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
