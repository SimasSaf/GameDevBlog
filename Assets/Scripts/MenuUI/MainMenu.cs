using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private IMenuUIController iMenuUiController;

    void Awake()
    {
        iMenuUiController = FindAnyObjectByType<MenuUiController>();
    }
    public void OnSinglePlayerButtonPressed()
    {
        iMenuUiController.StartGame();
    }

    public void Settings()
    {
        iMenuUiController.NavigateToSettings(this);
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
