using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public QuestSystem questSystem;
      private void Start()
    {
        questSystem = GetComponent<QuestSystem>();

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
}
