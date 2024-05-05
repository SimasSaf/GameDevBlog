using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public bool cameFromMainMenu;
    public AudioMixer audioMixer;
    private GameObject mainMenuGO;
    private GameObject settingsMenuGO;
    private GameObject pauseMenuGO;
    private MenuUiController menuUiController;

    private bool isFullscreen = false;

    void Awake()
    {
        mainMenuGO = GameObject.Find("MainMenu");
        settingsMenuGO = GameObject.Find("SettingsMenu");
        pauseMenuGO = GameObject.Find("PauseMenu");
        menuUiController = FindObjectOfType<MenuUiController>();
    } 

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void ToggleFullscreen()
    {
        if(isFullscreen){
            Debug.Log("Setting to false was true " + isFullscreen);
            isFullscreen = false;
            Screen.fullScreen = isFullscreen;
        }
        else {
            Debug.Log("Setting to true was false " + isFullscreen);
            isFullscreen = true;
            Screen.fullScreen = isFullscreen;
        }
    }

    public void Back()
    {
        if (cameFromMainMenu)
        {
            BackToMainMenu();
        }
        else
        {
            BackToPauseMenu();
        }
    }

    private void BackToMainMenu()
    {
        settingsMenuGO.SetActive(false);
        mainMenuGO.SetActive(true);
        menuUiController.SetFirstSelected(mainMenuGO);
    }

    private void BackToPauseMenu()
    {
        settingsMenuGO.SetActive(false);
        pauseMenuGO.SetActive(true);
        menuUiController.SetFirstSelected(pauseMenuGO);
    }
}
