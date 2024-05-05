
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUiController : MonoBehaviour
{

    private GameObject pauseMenuGO;
    private GameObject settingsMenuGO;

    private GameObject mainMenuGO;
    private PauseMenu pauseMenu;
    private MainMenu mainMenu;
    private SettingsMenu settingsMenu;
    private EventSystem eventSystem;

    void Start()
    {
        pauseMenuGO.SetActive(false);
        settingsMenuGO.SetActive(false);
    }

    void Awake()
    {
        pauseMenuGO = GameObject.Find("PauseMenu");
        mainMenuGO = GameObject.Find("MainMenu");
        settingsMenuGO = GameObject.Find("SettingsMenu");
        pauseMenu = FindObjectOfType<PauseMenu>();
        mainMenu = FindObjectOfType<MainMenu>();
        settingsMenu = FindObjectOfType<SettingsMenu>();
        eventSystem = EventSystem.current;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainMenuGO.activeSelf && !settingsMenuGO.activeSelf)
            {
                if (Time.timeScale == 1)
                {
                    pauseMenuGO.SetActive(true);
                    SetFirstSelected(pauseMenuGO);
                    pauseMenu.Pause();
                }
                else
                {
                    pauseMenuGO.SetActive(false);
                    pauseMenu.Continue();
                }
            }

            if (settingsMenuGO.activeSelf)
            {
                settingsMenu.Back();
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
            Debug.LogError("No selectable component found in " + menu.name);
        }
    }
}