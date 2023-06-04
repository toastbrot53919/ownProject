using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsUI : MonoBehaviour
{
    public Text unspentStatPointsText;
    public Text strengthText;
    public Text intelligenceText;
    public Text dexterityText;
    public Text enduranceText;
    public Text wisdomText;

    public Button strengthButton;
    public Button intelligenceButton;
    public Button dexterityButton;
    public Button enduranceButton;
    public Button wisdomButton;

    public CharacterStats characterStats;

    private void Start()
    {
        strengthButton.onClick.AddListener(() => IncreaseStat(Archetype.Strength));
        intelligenceButton.onClick.AddListener(() => IncreaseStat(Archetype.Intelligence));
        dexterityButton.onClick.AddListener(() => IncreaseStat(Archetype.Dexterity));
        enduranceButton.onClick.AddListener(() => IncreaseStat(Archetype.Endurance));
        wisdomButton.onClick.AddListener(() => IncreaseStat(Archetype.Wisdom));

        characterStats.StatsChanged += UpdateUI;
        UpdateUI();
    }

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void UpdateUI()
    {
        unspentStatPointsText.text = "Unspent Points: " + characterStats.unspentStatPoints;
        strengthText.text = "Strength: " + characterStats.strength;
        intelligenceText.text = "Intelligence: " + characterStats.intelligence;
        dexterityText.text = "Dexterity: " + characterStats.dexterity;
        enduranceText.text = "Endurance: " + characterStats.endurance;
        wisdomText.text = "Wisdom: " + characterStats.wisdom;
    }

    private void IncreaseStat(Archetype mainStatType)
    {
        characterStats.IncreaseStat(mainStatType, 1);
    }
}
