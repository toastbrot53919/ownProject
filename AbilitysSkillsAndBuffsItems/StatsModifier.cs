[System.Serializable]
public class StatsModifier
{
    public float Strength;
    public float Intelligence;
    public float Dexterity;
    public float Endurance;
    public float Wisdom;

    public float attackSpeed;
    public float criticalChance;
    public float criticalDamage;
    public float spellCriticalChance;
    public float spellCriticalDamage;
    public float cooldown;
    public float dodgeChance;
    public float armor;
    public float magicResistance;
    public float maxLife;
    public float maxMana;
    public float lifeRegen;
    public float manaRegen;
    public float movementSpeed;
    public StatsModifier(
        float strength = 0f,
        float intelligence = 0f,
        float dexterity = 0f,
        float endurance = 0f,
        float wisdom = 0f,
        float attackSpeed = 0f,
        float criticalChance = 0f,
        float criticalDamage = 0f,
        float spellCriticalChance = 0f,
        float spellCriticalDamage = 0f,
        float cooldown = 0f,
        float dodgeChance = 0f,
        float armor = 0f,
        float magicResistance = 0f,
        float maxLife = 0f,
        float maxMana = 0f,
        float lifeRegen = 0f,
        float manaRegen = 0f,
        float movementSpeed = 0f
    )
    {
        Strength = strength;
        Intelligence = intelligence;
        Dexterity = dexterity;
        Endurance = endurance;
        Wisdom = wisdom;
        this.attackSpeed = attackSpeed;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
        this.spellCriticalChance = spellCriticalChance;
        this.spellCriticalDamage = spellCriticalDamage;
        this.cooldown = cooldown;
        this.dodgeChance = dodgeChance;
        this.armor = armor;
        this.magicResistance = magicResistance;
        this.maxLife = maxLife;
        this.maxMana = maxMana;
        this.lifeRegen = lifeRegen;
        this.manaRegen = manaRegen;
        this.movementSpeed = movementSpeed;
    }

    public void Add(StatsModifier other)
    {
        Strength += other.Strength;
        Intelligence += other.Intelligence;
        Dexterity += other.Dexterity;
        Endurance += other.Endurance;
        Wisdom += other.Wisdom;

        attackSpeed += other.attackSpeed;
        criticalChance += other.criticalChance;
        criticalDamage += other.criticalDamage;
        spellCriticalChance += other.spellCriticalChance;
        spellCriticalDamage += other.spellCriticalDamage;
        cooldown += other.cooldown;
        dodgeChance += other.dodgeChance;
        armor += other.armor;
        magicResistance += other.magicResistance;
        maxLife += other.maxLife;
        maxMana += other.maxMana;
        lifeRegen += other.lifeRegen;
        manaRegen += other.manaRegen;
        movementSpeed += other.movementSpeed;
    }
    public void Sub(StatsModifier other)
    {
        Strength -= other.Strength;
        Intelligence -= other.Intelligence;

        Dexterity -= other.Dexterity;
        Endurance -= other.Endurance;
        Wisdom -= other.Wisdom;
        
        attackSpeed -= other.attackSpeed;
        criticalChance -= other.criticalChance;
        criticalDamage -= other.criticalDamage;
        spellCriticalChance -= other.spellCriticalChance;
        spellCriticalDamage -= other.spellCriticalDamage;
        cooldown -= other.cooldown;
        dodgeChance -= other.dodgeChance;
        armor -= other.armor;
        magicResistance -= other.magicResistance;
        maxLife -= other.maxLife;
        maxMana -= other.maxMana;
        lifeRegen -= other.lifeRegen;
        manaRegen -= other.manaRegen;
        movementSpeed -= other.movementSpeed;
    }
}
