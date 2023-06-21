using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EquipManager : MonoBehaviour,ICanStoreAndLoad<EquipManagerSaveData>
{
    public enum EquipmentType { Weapon, Shield, Helmet, ChestArmor, LegArmor, Boots, Ring, Wrist }

    public Dictionary<EquipmentType, EquipableItem> equippedItems = new Dictionary<EquipmentType, EquipableItem>();

    // Properties to store the total stats from all equipped items.
    public StatsModifier TotalStatModier;
    public float TotalStrength = 0;
    public float TotalIntelligence= 0;
    public float TotalDexterity= 0;
    public float TotalEndurance= 0;
    public float TotalWisdom=0 ;
    // Add more stat properties as needed.

    public void EquipItem(EquipmentType type, EquipableItem item)
    {
        if (equippedItems.ContainsKey(type))
        {
            UnequipItem(type);
        }

        equippedItems[type] = item;
        ApplyItemStats(item);
    }

    public void UnequipItem(EquipmentType type)
    {
        if (!equippedItems.ContainsKey(type)) return;

        EquipableItem item = equippedItems[type];
        RemoveItemStats(item);
        equippedItems.Remove(type);
    }

    private void ApplyItemStats(EquipableItem item)
    {
        TotalStrength += item.strengthBonus;
        TotalIntelligence += item.intelligenceBonus;
        TotalDexterity += item.dexterityBonus;
        TotalEndurance += item.enduranceBonus;
        TotalWisdom += item.wisdomBonus;

        TotalStatModier.Add(item.subStatsModifier);

        // Add more stat effects as needed.
    }

    private void RemoveItemStats(EquipableItem item)
    {
        TotalStrength -= item.strengthBonus;
        TotalIntelligence -= item.intelligenceBonus;
        TotalDexterity -= item.dexterityBonus;
        TotalEndurance -= item.enduranceBonus;
        TotalWisdom -= item.wisdomBonus;

        TotalStatModier.Sub(item.subStatsModifier);

        // Remove more stat effects as needed.
    }

    internal void SetEquipManager(EquipManager equipManager)
    {
        equippedItems = equipManager.equippedItems;
        TotalStatModier = equipManager.TotalStatModier;
        TotalStrength = equipManager.TotalStrength;
        TotalIntelligence = equipManager.TotalIntelligence;
        TotalDexterity = equipManager.TotalDexterity;
        TotalEndurance = equipManager.TotalEndurance;
        TotalWisdom = equipManager.TotalWisdom;
        

    }
    public EquipManagerSaveData GetSaveData()
    {
        return new EquipManagerSaveData(this);
    }
    public void LoadFromSaveData(EquipManagerSaveData saveData)
    {
        foreach(KeyValuePair<EquipmentType, string> item in saveData.equippedItems){
            EquipableItem equipableItem = ItemManager.getItemyString(item.Value) as EquipableItem;
            if(equipableItem != null){
                equippedItems.Add(item.Key, equipableItem);
            }
            else{
                Debug.Log("Item not found: " + item.Value);
            }
        }
    }
    
}
[Serializable]
public class EquipManagerSaveData{
    public Dictionary<EquipManager.EquipmentType, string> equippedItems;
    public EquipManagerSaveData(EquipManager equipManager){
        equippedItems = new Dictionary<EquipManager.EquipmentType, string>();
        foreach(KeyValuePair<EquipManager.EquipmentType, EquipableItem> item in equipManager.equippedItems){
            equippedItems.Add(item.Key, item.Value.name);
        }
    }
    
}
