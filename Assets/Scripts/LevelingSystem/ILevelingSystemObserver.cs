public interface ILevelingSystemObserver
{
    void OnLevelUp(int level);
    void OnReset();
    void OnAddExperience(int experience, int experienceToNextLevel);
}
