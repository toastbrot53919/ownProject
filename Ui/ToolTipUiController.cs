using UnityEngine;
using UnityEngine.UI;

public class ToolTipUiController : MonoBehaviour
{
    public Text SkillName;
    public Text SkillDescription;
    public Text AlreadySkilled;
    public Text SkillpointCost;
    public Text AttrbuteReq;
    public Image SkillIcon;

    private void Start()
    {
    }

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UpdateUI(SkillNode node)
    {
        SkillName.text = node.skillName;
        SkillDescription.text = node.skillDescription;
        if (node.isUnlocked)
        {
            AlreadySkilled.gameObject.SetActive(true);
        }
        else
        {
            AlreadySkilled.gameObject.SetActive(false);
        }
        SkillpointCost.text = "Cost: " + node.skillPointCost;

        AttrbuteReq.text = "Requiment:";

        for (int a = 0; a < node.mainStatRequirement.Count; a++)
        {

            AttrbuteReq.text += " " + node.mainStatRequirement[a] + ": " + node.mainStatValue[a];
        }

        if (node.prerequisiteSkill != null)
        {
            AttrbuteReq.text += "Skill Requiment: " + node.prerequisiteSkill.skillName;
        }
        SkillIcon.sprite = node.icon;
    }
    public void UpdateUI(Ability ability)
{
    SkillName.text = ability.abilityName;
    SkillDescription.text = ability.abilityDescription;
    AlreadySkilled.gameObject.SetActive(false);
    SkillpointCost.gameObject.SetActive(false);

    AttrbuteReq.text = ability.abilityModifierManager.printAllModifers();
    SkillIcon.sprite = ability.icon;
}

public void UpdateUI(Item item)
{
    SkillName.text = item.itemName;
    SkillDescription.text = item.description;
    AlreadySkilled.gameObject.SetActive(false);
    SkillpointCost.gameObject.SetActive(false);

    if (item is EquipableItem equipableItem)
    {
        AttrbuteReq.text = $"Bonuses:\nStrength: {equipableItem.strengthBonus}\nIntelligence: {equipableItem.intelligenceBonus}\nDexterity: {equipableItem.dexterityBonus}\nEndurance: {equipableItem.enduranceBonus}\nWisdom: {equipableItem.wisdomBonus}";
    }
    else
    {
        AttrbuteReq.text = "";
    }

    SkillIcon.sprite = item.icon;
}


}
