using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuUiController : MonoBehaviour
{
    public InputActionReference escapeAction;
    public InputActionReference enterAction;
    private GameObject pauseMenuGO;
    private GameObject settingsMenuGO;

    private GameObject mainMenuGO;
    private PauseMenu pauseMenu;
    private SettingsMenu settingsMenu;
    private EventSystem eventSystem;

    private void Awake()
    {
        pauseMenuGO = GameObject.Find("PauseMenu");
        mainMenuGO = GameObject.Find("MainMenu");
        settingsMenuGO = GameObject.Find("SettingsMenu");
        pauseMenu = FindObjectOfType<PauseMenu>();
        settingsMenu = FindObjectOfType<SettingsMenu>();
        eventSystem = EventSystem.current;
    }

    private void Start()
    {
        pauseMenuGO.SetActive(false);
        settingsMenuGO.SetActive(false);
        escapeAction.action.Enable();
        enterAction.action.Enable();    }

    private void OnDisable()
    {
        escapeAction.action.Disable();
        enterAction.action.Disable();    }

    void Update()
    {
        if (escapeAction.action.triggered) // Check if the cancel action has been triggered
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
