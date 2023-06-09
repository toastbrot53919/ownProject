﻿using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
}


[System.Serializable]
public class EquipableItem : Item
{
    public EquipManager.EquipmentType equipmentType;
    public float strengthBonus;
    public float intelligenceBonus;
    public float dexterityBonus;
    public float enduranceBonus;
    public float wisdomBonus;

    public StatsModifier subStatsModifier;
}
