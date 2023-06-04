using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellBookUiController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text objectivesText;
    public ScrollRect spellListScrollRect;
    public GameObject spellListItemPrefab;
    public Transform spellListContent;
    
    public AbilityController abilityController;
  

    private void Awake()
    {
       
    }

    private void Start()
    {  
        UpdateQuestList();
    }

    public void UpdateQuestList()
    {
        // Clear the quest list content
        foreach (Transform child in spellListContent)
        {
            Destroy(child.gameObject);
        }

        // Re-populate the quest list content
        foreach (Ability ability in abilityController.learnedAbilitys)
        {
            GameObject spellListItem = Instantiate(spellListItemPrefab, spellListContent);
            spellListItem.gameObject.SetActive(true);
            spellListItem.GetComponentInChildren<TMP_Text>().text = ability.name;
            spellListItem.GetComponent<Button>().onClick.AddListener(() => ShowAbilityInformation(ability));
            spellListItem.GetComponent<UiAbilitySlot>().ability = ability;
            
        }

        // Reset the scroll position of the quest list
        spellListScrollRect.verticalNormalizedPosition = 1f;
    }

    public void ShowAbilityInformation(Ability ability)
    {
        // Set the quest information text fields to the current quest's data
        titleText.text = ability.abilityName;
        descriptionText.text = ability.abilityDescription;
    string info = "";

    info += "- Base Damage: " + ability.BaseAbilityStats.baseDamage + "\n";
    info += "- Strength Scaling: " + ability.BaseAbilityStats.strengthScaling + "\n";
    info += "- Intelligence Scaling: " + ability.BaseAbilityStats.intelligenceScaling + "\n";
    info += "- Cooldown: " + ability.BaseAbilityStats.cooldown + "\n";
    objectivesText.text = info;

        /*foreach(QuestObjective objective in ability.)
        {
            objectivesString += $"-({objective.GetObjectiveProgress()})\n";
        }   */
        
    }
}
