using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour, ILevelingSystemOR, ILeveling, ILevelTracker
{
    private int experience = 0;
    private int level = 0;
    private int experienceToNextLevel = 10;
    private int howMuchHarderNextLevelBecomes = 10;

    private List<ILevelingSystemObserver> observers = new List<ILevelingSystemObserver>();

    public void AddExperience(int experiencePoints)
    {
        experience += experiencePoints;
        NotifyOnAddExperience();
        if (experience >= experienceToNextLevel)
        {
            levelUp();
            NotifyOnLevelUp();
        }
    }

    public void ResetLevelAndExperience()
    {
        experience = 0;
        level = 0;
        experienceToNextLevel = 10;
        NotifyOnReset();
    }

    public void RegisterObserver(ILevelingSystemObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(ILevelingSystemObserver observer)
    {
        observers.Remove(observer);
    }

    private void levelUp()
    {
        level += 1;
        experience = 0;
        experienceToNextLevel += howMuchHarderNextLevelBecomes;
        NotifyOnLevelUp();
    }

    private void NotifyOnLevelUp()
    {
        foreach (ILevelingSystemObserver observer in observers)
        {
            observer.OnLevelUp(level);
        }
    }

    public void NotifyOnReset()
    {
        foreach (ILevelingSystemObserver observer in observers)
        {
            observer.OnReset();
        }
    }

    private void NotifyOnAddExperience()
    {
        foreach (ILevelingSystemObserver observer in observers)
        {
            observer.OnAddExperience(experience, experienceToNextLevel);
        }
    }

    public int getLevel()
    {
        return level;
    }
}
