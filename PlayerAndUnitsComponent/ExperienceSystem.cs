using System;

public class ExperienceSystem
{
    public int CurrentXP { get; private set; }
    public int Level { get; private set; }
    public int XpToNextLevel { get; private set; }

    public event Action LevelUpEvent;
    public event Action<int> ExperienceGained;

    public ExperienceSystem()
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
}
