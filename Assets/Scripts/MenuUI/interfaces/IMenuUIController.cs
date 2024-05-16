public interface IMenuUIController
{
    void StartGame();
    void NavigateToSettings(PauseMenu navigatingFromScript);
    void NavigateToSettings(MainMenu navigatingFromScript);
    void ResumeGame();
    void NavigateToMainMenu();
    void NavigateBackFromSettings();

}