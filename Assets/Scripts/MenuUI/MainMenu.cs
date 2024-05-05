using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private EnemySpawnManager enemySpawnManager;
    private GameObject mainMenuGO;
    private GameObject settingsMenuGO;
    private CameraMovement cameraMovement;
    private SettingsMenu settingsMenu;
    private MenuUiController menuUiController;

    void Awake()
    {
        mainMenuGO = GameObject.Find("MainMenu");
        settingsMenuGO = GameObject.Find("SettingsMenu");
        cameraMovement = FindObjectOfType<CameraMovement>();
        enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        settingsMenu = FindObjectOfType<SettingsMenu>();
        menuUiController = FindObjectOfType<MenuUiController>();
    }

    public void OnSinglePlayerButtonPressed()
    {
        if (cameraMovement != null)
        {
            cameraMovement.MoveToEarth();
            HideMainMenu();
            enemySpawnManager.StartSpawningEnemies();
            HideMainMenu();
        }
        else
        {
            Debug.LogError("CameraMovement script is not assigned in the UIManager.");
        }
    }

    public void HideMainMenu()
    {
        mainMenuGO.SetActive(false);
    }

    public void Settings()
    {
        settingsMenu.cameFromMainMenu = true;
        mainMenuGO.SetActive(false);
        settingsMenuGO.SetActive(true);
        menuUiController.SetFirstSelected(settingsMenuGO);
    }

    public void Exit()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
