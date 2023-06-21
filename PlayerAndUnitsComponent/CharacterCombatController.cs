using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterCombatController : MonoBehaviour, IStatsProvider
{
    public CharacterStats characterStats;
    public AbilityController abilityController;
    public AnimationController animationController;
    public IStunnable stunnable;
    public ComboController comboController;

    AIController aiController;


    private void Start()
    {
        aiController = GetComponent<AIController>();
        characterStats = GetComponent<CharacterStats>();
        stunnable = GetComponent<IStunnable>();
        abilityController = GetComponent<AbilityController>();
        animationController = GetComponent<AnimationController>();
        comboController = new ComboController();
    }
    public void StopAbility(Ability ability){
        abilityController.AbortAbility(ability);
    }
    public void PerformAbility(Ability ability, GameObject target)
    {
        if(stunnable.isStunned())
        {
            return;
        }
        if(!abilityController.checkCooldown(ability.name,ability.TotalAbilityStats.cooldown))
        {
            Debug.Log("Ability on cooldown" + ability.name);
            return;
        }
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
           playerController.faceIndirectionOfCamera();
        }
        float damageAbility = ability.TotalAbilityStats.baseDamage 
        + ability.TotalAbilityStats.strengthScaling * characterStats.strength
        + ability.TotalAbilityStats.intelligenceScaling * characterStats.intelligence;

        float critChance = characterStats.criticalChance;
        if (Random.Range(0f, 1f) <= critChance)
        {
            damageAbility *= 2;
        }


        AbilityData abilityData = new AbilityData
        {
            casterStats = characterStats,
            target = target,
            damage = damageAbility,
            casterController = abilityController,
            casterCombatController = this,
            projectileSpeed = ability.TotalAbilityStats.projectileSpeed,
            stunDuration = ability.TotalAbilityStats.stunDuration,
            
            


            // ... other fields
        };
        abilityController.setCooldown(ability.name,ability.BaseAbilityStats.cooldown);
        comboController.UpdateComboController();
        abilityController.CastAbility(ability, abilityData);
        if(aiController != null){
            aiController.setNextActionDelay(animationController.returnAnimationLockTiming(ability.animationName));
        }

    }
    public CharacterStats GetCharacterStats()
    {
        return characterStats;
    }

}
