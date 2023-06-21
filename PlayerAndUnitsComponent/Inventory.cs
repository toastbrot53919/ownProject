using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour,ICanStoreAndLoad<InventorySaveData>
{
    public List<Item> items;
    private QuestSystem questSystem;
      private void Start()
    {
        questSystem = GetComponent<QuestSystem>();
        items = new List<Item>();

    }

    public void AddItem(Item item)
    {
        items.Add(item);
        if(questSystem!=null)  // Check if questSystem is not null
        {
            Debug.Log("collect:"+item.name);
            questSystem.UpdateQuestObjective("collect:"+item.name); // Call UpdateQuestObjective method with item id
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }
    public InventorySaveData GetSaveData()
    {
        return new InventorySaveData(this);
    }
    public void LoadFromSaveData(InventorySaveData saveData)
    {
        foreach(string itemName in saveData.items){
            Item item = ItemManager.getItemyString(itemName);
            if(item != null){
                items.Add(item);
            }
        }
    }
}
[System.Serializable]
public class InventorySaveData{
    public List<string> items;
    public InventorySaveData(Inventory inventory){
        items = new List<string>();
        foreach(Item item in inventory.items){
            items.Add(item.name);
        }
    }
}