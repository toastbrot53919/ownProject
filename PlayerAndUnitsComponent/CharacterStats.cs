using System;
using System.Collections;
using UnityEngine;
[Serializable]
public class CharacterStats : MonoBehaviour, ICanStoreAndLoad<CharacterStatsSaveData>
{
    // MainStats
    public float strength;
    public float intelligence;
    public float dexterity;
    public float endurance;
    public float wisdom;

    // SubStats

    public float criticalChance;
    public float criticalDamage;
    public float attackSpeed;

    public float spellCriticalChance;
    public float spellCriticalDamage;

    public float cooldown;

    public float maxLife;
    public float maxMana;
    public float lifeRegen;
    public float manaRegen;

    public float armor;
    public float magicResistance;

    public float dodgeChance;

    public int unspentStatPoints;

    public event Action StatsChanged;
    private EquipManager equipManager;
    private SkillController skillController;
    private BuffSystem buffSystem;

    private void Awake()
    {
        buffSystem = GetComponent<BuffSystem>();
        equipManager = GetComponent<EquipManager>();
        skillController = GetComponent<SkillController>();
    }
    private void Start()
    {
        // Initialize unspentStatPoints or load from saved game data
        unspentStatPoints = 10;
        StartCoroutine(InitializeCharacterStats());        
    }
    private IEnumerator InitializeCharacterStats()
    {
        yield return new WaitUntil(() => equipManager != null);
        UpdateSubStats();

                
         HealthController healthController = GetComponent<HealthController>();
         if(healthController != null){
                healthController.updateHealth();
         }
         ManaController manaController = GetComponent<ManaController>();
            if(manaController != null){
                    manaController.updateMana();
            }
        
    }
    public void AddStatPoints(int amount)
    {
        unspentStatPoints += amount;
        StatsChanged?.Invoke();
    }

    public void UpdateSubStats()
    {

        strength += equipManager.TotalStrength;
        intelligence += equipManager.TotalIntelligence;
        dexterity += equipManager.TotalDexterity;
        endurance += equipManager.TotalEndurance;
        wisdom += equipManager.TotalWisdom;


        criticalChance = 0.02f * dexterity;
        criticalDamage = 1.5f + (0.14f * dexterity);
        attackSpeed = 1 + (0.01f * strength * dexterity);

        spellCriticalChance = 0.02f * intelligence;
        spellCriticalDamage = 1.5f + (0.14f * intelligence);

        armor = 1.5f * endurance;
        magicResistance = 1.5f * endurance;


        // Calculate substats based on main stats + equipment bonuses.
        maxLife = 100 + 20 * endurance;
        maxMana = 100 + 20 * wisdom;
        lifeRegen = 1 + 0.25f * endurance;
        manaRegen = 0.5f + 0.25f * wisdom;

        dodgeChance = 0.009f * dexterity;

        AddStatBonuses(equipManager.TotalStatModier);
        AddStatBonuses(skillController.totalStatsModier);
        AddStatBonuses(buffSystem.TotalstatsModifier);


        StatsChanged?.Invoke();
    }

    public void AddStatBonuses(StatsModifier statModifier)
    {

        attackSpeed += statModifier.attackSpeed;
        criticalChance += statModifier.criticalChance;
        criticalDamage += statModifier.criticalDamage;
        spellCriticalChance += statModifier.spellCriticalChance;
        spellCriticalDamage += statModifier.spellCriticalDamage;
        cooldown += statModifier.cooldown;
        dodgeChance += statModifier.dodgeChance;
        armor += statModifier.armor;
        magicResistance += statModifier.magicResistance;
        maxLife += statModifier.maxLife;
        maxMana += statModifier.maxMana;
        lifeRegen += statModifier.lifeRegen;
        manaRegen += statModifier.manaRegen;
    }
    public void RemoveStatBonuses(StatsModifier statModifier)
    {
        attackSpeed -= statModifier.attackSpeed;
        criticalChance -= statModifier.criticalChance;
        criticalDamage -= statModifier.criticalDamage;
        spellCriticalChance -= statModifier.spellCriticalChance;
        spellCriticalDamage -= statModifier.spellCriticalDamage;
        cooldown -= statModifier.cooldown;
        dodgeChance -= statModifier.dodgeChance;
        armor -= statModifier.armor;
        magicResistance -= statModifier.magicResistance;
        maxLife -= statModifier.maxLife;
        maxMana -= statModifier.maxMana;
        lifeRegen -= statModifier.lifeRegen;
        manaRegen -= statModifier.manaRegen;
    }


    public void IncreaseStat(Archetype stateType, int amount)
    {
        if (unspentStatPoints >= amount)
        {
            switch (stateType)
            {
                case Archetype.Strength:
                    strength += amount;
                    break;
                case Archetype.Intelligence:
                    intelligence += amount;
                    break;
                case Archetype.Dexterity:
                    dexterity += amount;
                    break;
                case Archetype.Endurance:
                    endurance += amount;
                    break;
                case Archetype.Wisdom:
                    wisdom += amount;
                    break;
                default:
                    Debug.LogWarning("Invalid stat name.");
                    return;
            }

            unspentStatPoints -= amount;
            UpdateSubStats();
        }
        else
        {
            Debug.LogWarning("Not enough stat points.");
        }
    }

    internal void SetStats(CharacterStats stats)
    {
        strength = stats.strength;
        intelligence = stats.intelligence;
        dexterity = stats.dexterity;
        endurance = stats.endurance;
        wisdom = stats.wisdom;
        equipManager = GetComponent<EquipManager>();
        UpdateSubStats();
    }
    public void LoadFromSaveData(CharacterStatsSaveData data)
    {
        strength = data.strength;
        intelligence = data.intelligence;
        dexterity = data.dexterity;
        endurance = data.endurance;
        wisdom = data.wisdom;
        criticalChance = data.criticalChance;
        criticalDamage = data.criticalDamage;
        attackSpeed = data.attackSpeed;
        spellCriticalChance = data.spellCriticalChance;
        spellCriticalDamage = data.spellCriticalDamage;
        cooldown = data.cooldown;
        maxLife = data.maxLife;
        maxMana = data.maxMana;
        lifeRegen = data.lifeRegen;
        manaRegen = data.manaRegen;
        armor = data.armor;
        magicResistance = data.magicResistance;
        dodgeChance = data.dodgeChance;
        unspentStatPoints = data.unspentStatPoints;
        UpdateSubStats();
    }
    public CharacterStatsSaveData GetSaveData()
    {
        return new CharacterStatsSaveData(this);
    }
}
[System.Serializable]
public class CharacterStatsSaveData{
    public float strength;
    public float intelligence;
    public float dexterity;
    public float endurance;
    public float wisdom;
    public float criticalChance;
    public float criticalDamage;
    public float attackSpeed;
    public float spellCriticalChance;
    public float spellCriticalDamage;
    public float cooldown;
    public float maxLife;
    public float maxMana;
    public float lifeRegen;
    public float manaRegen;
    public float armor;
    public float magicResistance;
    public float dodgeChance;
    public int unspentStatPoints;
    public CharacterStatsSaveData(CharacterStats characterStats){
        strength = characterStats.strength;
        intelligence = characterStats.intelligence;
        dexterity = characterStats.dexterity;
        endurance = characterStats.endurance;
        wisdom = characterStats.wisdom;
        criticalChance = characterStats.criticalChance;
        criticalDamage = characterStats.criticalDamage;
        attackSpeed = characterStats.attackSpeed;
        spellCriticalChance = characterStats.spellCriticalChance;
        spellCriticalDamage = characterStats.spellCriticalDamage;
        cooldown = characterStats.cooldown;
        maxLife = characterStats.maxLife;
        maxMana = characterStats.maxMana;
        lifeRegen = characterStats.lifeRegen;
        manaRegen = characterStats.manaRegen;
        armor = characterStats.armor;
        magicResistance = characterStats.magicResistance;
        dodgeChance = characterStats.dodgeChance;
        unspentStatPoints = characterStats.unspentStatPoints;
    }
}