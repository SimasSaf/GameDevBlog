public interface IIngameUI
{
    void UpdateHealth(int currentHealth, int maxHealth);
    void ResetLevelAndExperience();
    void LevelUp(int level);
    void AddExperience(int experience, int experienceToNextLevel);
}
