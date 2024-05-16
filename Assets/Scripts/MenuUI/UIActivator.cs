using UnityEngine;

public class UIActivator : MonoBehaviour, IUIActivator
{
    private GameObject pauseMenuGO;
    private GameObject settingsMenuGO;
    private GameObject mainMenuGO;
    private GameObject ingameUiGO;
    private GameObject gameOverScreenGO;
    private GameObject upgradeMenuGO;

    void Awake()
    {
        pauseMenuGO = GameObject.Find("PauseMenu");
        mainMenuGO = GameObject.Find("MainMenu");
        settingsMenuGO = GameObject.Find("SettingsMenu");
        ingameUiGO = GameObject.Find("IngameUI");
        gameOverScreenGO = GameObject.Find("GameOverScreen");
        upgradeMenuGO = GameObject.Find("UpgradeMenu");
    }

    public void ActivatePauseMenu()
    {
        pauseMenuGO.SetActive(true);
    }

    public void ActivateMainMenu()
    {
        mainMenuGO.SetActive(true);
    }

    public void ActivateSettingsMenu()
    {
        settingsMenuGO.SetActive(true);
    }

    public void ActivateIngameUI()
    {
        ingameUiGO.SetActive(true);
    }

    public void ActivateGameOverScreen()
    {
        gameOverScreenGO.SetActive(true);
    }

    public void DeactivatePauseMenu()
    {
        pauseMenuGO.SetActive(false);
    }

    public void DeactivateMainMenu()
    {
        mainMenuGO.SetActive(false);
    }

    public void DeactivateSettingsMenu()
    {
        settingsMenuGO.SetActive(false);
    }

    public void DeactivateIngameUI()
    {
        ingameUiGO.SetActive(false);
    }

    public void DeactivateGameOverScreen()
    {
        gameOverScreenGO.SetActive(false);
    }

    public void ActivateUpgradeMenu()
    {
        upgradeMenuGO.SetActive(true);
    }

    public void DeactivateUpgradeMenu()
    {
        upgradeMenuGO.SetActive(false);
    }
}
