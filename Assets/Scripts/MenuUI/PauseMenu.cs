using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenuGO;
    private GameObject mainMenuGO;
    private GameObject settingsMenuGO;
    private CameraMovement cameraMovement;
    private EnemyPoolManager enemyPoolManager;
    private EnemySpawnManager enemySpawnManager;
    private SettingsMenu settingsMenu;
    private MenuUiController menuUiController;
    void Awake()
    {
        pauseMenuGO = GameObject.Find("PauseMenu");
        mainMenuGO = GameObject.Find("MainMenu");
        settingsMenuGO = GameObject.Find("SettingsMenu");
        cameraMovement = FindObjectOfType<CameraMovement>();
        enemyPoolManager = FindObjectOfType<EnemyPoolManager>();
        enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        settingsMenu = FindObjectOfType<SettingsMenu>();
        menuUiController = FindObjectOfType<MenuUiController>();

    }
    public void Pause()
    {
        Debug.Log("Pausing");
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        pauseMenuGO.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        pauseMenuGO.SetActive(false);
        Time.timeScale = 1f;
        enemySpawnManager.StopSpawningEnemies();
        enemyPoolManager.DeactivateAllEnemies();
        cameraMovement.MoveToStart();
        mainMenuGO.SetActive(true);
        menuUiController.SetFirstSelected(mainMenuGO);
    }

    public void Settings()
    {
        settingsMenu.cameFromMainMenu = false;
        pauseMenuGO.SetActive(false);
        settingsMenuGO.SetActive(true);
        menuUiController.SetFirstSelected(settingsMenuGO);
    }
}
