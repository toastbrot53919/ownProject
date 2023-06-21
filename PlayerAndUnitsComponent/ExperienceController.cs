using System;
using UnityEngine;
public class ExperienceController : MonoBehaviour, ICanStoreAndLoad<ExperienceSystemSaveData>
{
    public int CurrentXP ;
    public int Level ;
    public int XpToNextLevel { get; private set; }

    public event Action LevelUpEvent;
    public event Action<int> ExperienceGained;

    public ExperienceController()
    {
        CurrentXP = 0;
        Level = 1;
        UpdateXpToNextLevel();
    }

    public void AddExperience(int amount)
    {
        CurrentXP += amount;
        ExperienceGained?.Invoke(amount);

        while (CurrentXP >= XpToNextLevel)
        {
            CurrentXP -= XpToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Level++;
        UpdateXpToNextLevel();
        LevelUpEvent?.Invoke();
    }

    private void UpdateXpToNextLevel()
    {
        XpToNextLevel = CalculateXpForLevel(Level);
    }

    private int CalculateXpForLevel(int level)
    {
        // Implement your custom XP calculation logic here
        return (int)Math.Floor(Math.Pow(level, 2) * 100);
    }
    public ExperienceSystemSaveData GetSaveData()
    {
        return new ExperienceSystemSaveData(this);
    }
    public void LoadFromSaveData(ExperienceSystemSaveData saveData)
    {
        CurrentXP = (int)saveData.experience;
        Level = saveData.level;
        UpdateXpToNextLevel();
    }
}
[System.Serializable]
public class ExperienceSystemSaveData{
    public int level;
    public float experience;
    public float experienceToNextLevel;
    public ExperienceSystemSaveData(ExperienceController experienceSystem){
        level = experienceSystem.Level;
        experience = experienceSystem.CurrentXP;
        experienceToNextLevel = experienceSystem.XpToNextLevel;
    }
}