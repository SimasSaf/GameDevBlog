using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

enum NavigatingFrom
{
    PAUSE_MENU,
    MAIN_MENU
}

public class MenuUiController
    : MonoBehaviour,
        IMenuUIController,
        IHealthObserver,
        ILevelingSystemObserver
{
    public InputActionReference escapeAction;
    public InputActionReference enterAction;

    private EventSystem eventSystem;

    private IUIActivator iUIActivator;
    private ICameraMovement iCameraMovement;
    private IMusicManager iAudioManager;
    private IIngameUI iIngameUI;

    private IEarthHealthManagerOR iHealthManager;
    private ILevelingSystemOR iLevelingSystem;

    private EnemySpawnManager enemySpawnManager;
    private EnemyPoolManager enemyPoolManager;
    private NavigatingFrom navigatingFrom;

    private GameObject pauseMenuGO;
    private GameObject settingsMenuGO;
    private GameObject mainMenuGO;
    private GameObject gameOverScreenGO;
    private GameObject upgradeMenuGO;

    private LevelingSystem levelingSystem;

    private void Awake()
    {
        pauseMenuGO = GameObject.Find("PauseMenu");
        mainMenuGO = GameObject.Find("MainMenu");
        settingsMenuGO = GameObject.Find("SettingsMenu");
        gameOverScreenGO = GameObject.Find("GameOverScreen");
        upgradeMenuGO = GameObject.Find("UpgradeMenu");

        iUIActivator = FindObjectOfType<UIActivator>();
        iIngameUI = FindAnyObjectByType<IngameUI>();
        iAudioManager = FindObjectOfType<AudioManager>();
        iCameraMovement = FindObjectOfType<CameraMovement>();

        enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        enemyPoolManager = FindAnyObjectByType<EnemyPoolManager>();

        iAudioManager = FindObjectOfType<AudioManager>();
        iHealthManager = FindObjectOfType<EarthHealthManager>();
        iLevelingSystem = FindObjectOfType<LevelingSystem>();

        levelingSystem = FindAnyObjectByType<LevelingSystem>();
    }

    private void Start()
    {
        eventSystem = EventSystem.current;

        iHealthManager.RegisterObserver(this);
        iLevelingSystem.RegisterObserver(this);

        iUIActivator.DeactivateGameOverScreen();
        iUIActivator.DeactivatePauseMenu();
        iUIActivator.DeactivateSettingsMenu();
        iUIActivator.DeactivateIngameUI();
        iUIActivator.DeactivateUpgradeMenu();

        NavigateToMainMenu();
        escapeAction.action.Enable();
        enterAction.action.Enable();
    }

    private void OnDisable()
    {
        escapeAction.action.Disable();
        enterAction.action.Disable();
    }

    void Update()
    {
        if (escapeAction.action.triggered)
        {
            if (
                !mainMenuGO.activeSelf && !settingsMenuGO.activeSelf && !gameOverScreenGO.activeSelf
            )
            {
                if (Time.timeScale == 1)
                {
                    iUIActivator.ActivatePauseMenu();
                    SetFirstSelected(pauseMenuGO);
                    Pause();
                }
                else
                {
                    ResumeGame();
                }
            }

            if (settingsMenuGO.activeSelf)
            {
                NavigateBackFromSettings();
            }
        }
    }

    public void SetFirstSelected(GameObject menu)
    {
        Selectable firstSelectable = menu.GetComponentInChildren<Selectable>();
        if (firstSelectable != null)
        {
            eventSystem.SetSelectedGameObject(firstSelectable.gameObject);
        }
        else
        {
            foreach (Transform child in menu.transform)
            {
                firstSelectable = child.GetComponentInChildren<Selectable>();
                if (firstSelectable != null)
                {
                    eventSystem.SetSelectedGameObject(firstSelectable.gameObject);
                    return;
                }
            }
        }
    }

    public void StartGame()
    {
        iAudioManager.PlayBossMusic();

        if (iCameraMovement != null)
        {
            iCameraMovement.MoveToEarth();
            iUIActivator.DeactivateMainMenu();
            iUIActivator.ActivateIngameUI();

            enemySpawnManager.StartSpawningEnemies();
        }
        else
        {
            Debug.LogError("CameraMovement script is not assigned in the UIManager.");
        }
    }

    public void NavigateToMainMenu()
    {
        iAudioManager.PlayMainMenuMusic();

        iUIActivator.DeactivatePauseMenu();
        iUIActivator.DeactivateSettingsMenu();
        iUIActivator.DeactivateIngameUI();
        iUIActivator.DeactivateGameOverScreen();

        enemySpawnManager.StopSpawningEnemies();
        enemyPoolManager.DeactivateAllEnemies();

        iCameraMovement.MoveToStart();

        levelingSystem.NotifyOnReset();

        iUIActivator.ActivateMainMenu();
        SetFirstSelected(mainMenuGO);

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Pause();

        iUIActivator.DeactivateMainMenu();
        iUIActivator.DeactivateSettingsMenu();
        iUIActivator.DeactivatePauseMenu();

        iUIActivator.ActivateGameOverScreen();
        SetFirstSelected(gameOverScreenGO);
    }

    public void ResumeGame()
    {
        iUIActivator.DeactivatePauseMenu();
        iUIActivator.DeactivateUpgradeMenu();
        Time.timeScale = 1f;
    }

    public void NavigateToSettings(PauseMenu navigatingFromScript)
    {
        navigatingFrom = NavigatingFrom.PAUSE_MENU;
        iUIActivator.DeactivatePauseMenu();
        iUIActivator.DeactivateMainMenu();
        iUIActivator.ActivateSettingsMenu();
        SetFirstSelected(settingsMenuGO);
    }

    public void NavigateToSettings(MainMenu navigatingFromScript)
    {
        navigatingFrom = NavigatingFrom.MAIN_MENU;
        iUIActivator.DeactivatePauseMenu();
        iUIActivator.DeactivateMainMenu();
        iUIActivator.ActivateSettingsMenu();
        SetFirstSelected(settingsMenuGO);
    }

    public void NavigateBackFromSettings()
    {
        if (navigatingFrom == NavigatingFrom.PAUSE_MENU)
        {
            NavigateToPauseMenu();
            return;
        }
        if (navigatingFrom == NavigatingFrom.MAIN_MENU)
        {
            NavigateToMainMenu();
            return;
        }
        else
        {
            Debug.LogError("No clue how we got here, menu problem");
        }
    }

    public void NavigateToPauseMenu()
    {
        iUIActivator.DeactivateSettingsMenu();
        iUIActivator.ActivatePauseMenu();
        SetFirstSelected(pauseMenuGO);
    }

    public void OnDamageTaken(int currentHealth, int maxHealth)
    {
        iIngameUI.UpdateHealth(currentHealth, maxHealth);
    }

    public void OnFatalDamageTaken()
    {
        GameOver();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    public void OnLevelUp(int level)
    {
        iIngameUI.LevelUp(level);
        Pause();
        iUIActivator.ActivateUpgradeMenu();
        SetFirstSelected(upgradeMenuGO);
    }

    public void OnReset()
    {
        iIngameUI.ResetLevelAndExperience();
    }

    public void OnAddExperience(int experience, int experienceToNextLevel)
    {
        iIngameUI.AddExperience(experience, experienceToNextLevel);
    }
}
