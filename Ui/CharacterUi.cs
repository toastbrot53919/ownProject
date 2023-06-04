using UnityEngine;
using UnityEngine.UI;

public class CharacterUi : MonoBehaviour
{
    public Text unspentStatPointsText;
    public Text strengthText;
    public Text intelligenceText;
    public Text dexterityText;
    public Text enduranceText;
    public Text wisdomText;



    public Text subStatsPhysical;
    public Text subStatsSpellCasting;
    public Text subStatsDefensive;
    public Text subStatsUniversal;

    public Button openCharacterStatsMenu;
    public Image unspentStatPoints;

    public CharacterStats characterStats;
    public UIManager uiManager;

    private void Start()
    {
        openCharacterStatsMenu.onClick.AddListener(() => uiManager.OpenCharacterStatusUI());
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
        strengthText.text = "Strength: " + characterStats.strength;
        intelligenceText.text = "Intelligence: " + characterStats.intelligence;
        dexterityText.text = "Dexterity: " + characterStats.dexterity;
        enduranceText.text = "Endurance: " + characterStats.endurance;
        wisdomText.text = "Wisdom: " + characterStats.wisdom;

        subStatsPhysical.text = "Critical Chance: " + characterStats.criticalChance.ToString("F1") + "%" + "\nCritical Damage: " + characterStats.criticalDamage + "%" + "\nAttack Speed: " + characterStats.attackSpeed.ToString("F2");
        subStatsSpellCasting.text = "Spell Crit Chc: " + characterStats.spellCriticalChance.ToString("F1") + "%" + "\nSpell Crit Dmg: " + characterStats.spellCriticalDamage + "%" + "\nCooldown: " + characterStats.cooldown;
        subStatsDefensive.text = "Armor: " + characterStats.armor + "\nMagic Resi: " + characterStats.magicResistance + "\nDodge Chance: " + characterStats.dodgeChance.ToString("F1") + "%";
        subStatsUniversal.text = "Max Life: " + characterStats.maxLife + "\nLife Reg: " + characterStats.lifeRegen + "\nMax Mana: " + characterStats.maxMana + "\nMana Reg: " + characterStats.manaRegen;
    }

}
