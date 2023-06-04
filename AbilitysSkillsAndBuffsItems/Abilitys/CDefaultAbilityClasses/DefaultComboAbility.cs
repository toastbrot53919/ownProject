using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DefaultComboAbility")]
public class DefaultComboAbility : Ability
{
    // SimpleStrike specific properties, if any
    public GameObject MeelePrefab;
    public float lifeTime = 0.5f;
    public float[] comboDamage = new float[3] { 0, 4, 12 };

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        HealthController healthController = target.GetComponent<HealthController>();
        if (healthController != null)
        {

            healthController.TakeDamage(abilityObject.data.damage,abilityObject.data.CasterStats.gameObject);
        }
    }
    public override void PreActivateAbility(AbilityData abilityData)
    {
        ComboController comboController = abilityData.CasterCombatController.comboController;

        // Use the name of this ability (ThreeHitComboAbility) to manage combo count
        string comboName = "ThreeHitComboAbility";

        // Increase combo counter or reset if already at maximum
        if (comboController.GetComboCounter(comboName) < 2)
        {
            comboController.IncreaseComboCounter(comboName);
        }
        else
        {
            comboController.ResetComboCounter(comboName);
        }

        // Instantiate the attack object (MeleePrefab) with appropriate damage
        int comboCount = comboController.GetComboCounter(comboName); // -1 to make it 0-based index

        abilityData.damage += comboDamage[comboCount]+BaseAbilityStats.baseDamage+abilityModifierManager.GetAdditionalModifiedValue().baseDamage;
        if(comboCount == 0)
        {
            animationName = "1HandSwordLightAttack1";
        }
        else if(comboCount == 1)
        {
            animationName = "1HandSwordLightAttack2";
        }
        else if(comboCount == 2)
        {
            animationName = "1HandSwordLightAttack3";
        }
    }

    public override void Activate(AbilityData abilityData)
    {
        Debug.Log("Activate");
        GameObject meleeStrikeInstance = Instantiate(MeelePrefab, abilityData.CasterStats.transform.position, abilityData.CasterStats.transform.rotation);
       
        AbilityObject abilityObject = meleeStrikeInstance.GetComponent<AbilityObject>();
        abilityObject.data = abilityData;
        RaiseOnObjectSpawned(abilityObject, null);

        
        abilityObject.ParentAbility = this;
        Debug.Log(abilityData.damage);
        Destroy(meleeStrikeInstance, lifeTime);
    }


}


