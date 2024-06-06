using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public bool cameFromMainMenu;
    private IMenuUIController iMenuUiController;
    private bool isFullscreen = false;
    private AudioManager audioManager;

    void Awake()
    {
        iMenuUiController = FindAnyObjectByType<MenuUiController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void SetVolume(float volume)
    {
        audioManager.SetVolume(volume);
    }

    public void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void Back()
    {
        iMenuUiController.NavigateBackFromSettings();
    }
}
