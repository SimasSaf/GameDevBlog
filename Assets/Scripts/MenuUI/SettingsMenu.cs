using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public bool cameFromMainMenu;
    public AudioMixer audioMixer;
    private IMenuUIController iMenuUiController;
    private bool isFullscreen = false;

    void Awake()
    {
        iMenuUiController = FindAnyObjectByType<MenuUiController>();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void ToggleFullscreen()
    {
        if (isFullscreen)
        {
            isFullscreen = false;
            Screen.fullScreen = isFullscreen;
        }
        else
        {
            isFullscreen = true;
            Screen.fullScreen = isFullscreen;
        }
    }

    public void Back()
    {
        iMenuUiController.NavigateBackFromSettings();
    }
}
