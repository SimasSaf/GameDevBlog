using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private IMenuUIController iMenuUiController;
    private ILeveling iLeveling;

    void Awake()
    {
        iMenuUiController = FindAnyObjectByType<MenuUiController>();
        iLeveling = FindObjectOfType<LevelingSystem>();
    }

    public void MainMenu()
    {
        iLeveling.ResetLevelAndExperience();
        iMenuUiController.NavigateToMainMenu();
    }
}
