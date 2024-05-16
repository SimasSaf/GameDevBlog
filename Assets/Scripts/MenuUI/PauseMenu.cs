using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private IMenuUIController iMenuUiController;

    void Awake()
    {
        iMenuUiController = FindAnyObjectByType<MenuUiController>();
    }
    public void Continue()
    {
        iMenuUiController.ResumeGame();
    }

    public void MainMenu()
    {
        iMenuUiController.NavigateToMainMenu();
    }

    public void Settings()
    {
        iMenuUiController.NavigateToSettings(this);
    }
}
