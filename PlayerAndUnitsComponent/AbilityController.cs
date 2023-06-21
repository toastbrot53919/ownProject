using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class AbilityController : MonoBehaviour,ICanStoreAndLoad<AbilityControllerSaveData>
{
    public Transform firePoint;
    public Transform hitPoint;
    public List<Ability> learnedAbilitys;
    public List<(string, float)> lastTimeAbilityUsed;
    private IStatsProvider statsProvider;
    private AnimationController animationController;


    private void Awake()
    {
        statsProvider = GetComponent<IStatsProvider>();
        lastTimeAbilityUsed = new List<(string, float)>();
        animationController = GetComponent<AnimationController>();
        foreach (Ability ability in learnedAbilitys)
        {
            ability.init();
        }
    }
    private void Update()
    {
        foreach (Ability ability in learnedAbilitys)
        {
            if (ability.useUpdate)
            {
                ability.OnUpdate();
            }
        }
    }
    public void CastAbility(Ability ability, AbilityData abilityData)
    {
             StartAbilityActivation(ability, abilityData);
    }

    public void StartAbilityActivation(Ability ability, AbilityData abilityData)
    {
        ability.PreActivateAbility(abilityData);
        animationController.PlayAnimation(ability.animationName);

        if (animationController.returnAnimationDelay(ability.animationName) == 0)
        {

            ability.Activate(abilityData);
        }
        else
        {

            Coroutine c = StartCoroutine(CastAfterDelay(ability, abilityData));
            ability.activationRoutine = c;
        }
    }
    public void AbortAbility(Ability ability)
    {
        if (ability.activationRoutine != null)
        {
            StopCoroutine(ability.activationRoutine);
            return;
        }
        ability.Deactivate();
    }
    public IEnumerator CastAfterDelay(Ability ability, AbilityData abilityData)
    {
        yield return new WaitForSeconds(animationController.returnAnimationDelay(ability.animationName));
        Debug.Log(animationController.returnAnimationDelay(ability.animationName) + " DELAY");
        ability.Activate(abilityData);
        yield return null;
    }
    public void AddAbility(Ability ability)
    {
        learnedAbilitys.Add(ability);
    }
    public bool checkCooldown(string abilityName, float cooldown)
    {
        foreach ((string, float) paar in lastTimeAbilityUsed)
        {
            Debug.Log(paar.Item1 + " " + abilityName);
            if (paar.Item1 == abilityName)
            {
                if (Time.time - paar.Item2 < cooldown)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void setCooldown(string abilityName, float cooldown)
    {
        bool found = false;
        for (int i = 0; i < lastTimeAbilityUsed.Count; i++)
        {
            if (lastTimeAbilityUsed[i].Item1 == abilityName)
            {
                lastTimeAbilityUsed[i] = (abilityName, Time.time);
                found = true;
            }
        }
        if (!found)
        {
            lastTimeAbilityUsed.Add((abilityName, Time.time));
        }
    }
    public void LoadFromSaveData(AbilityControllerSaveData saveData){
        learnedAbilitys = new List<Ability>();
        foreach(string abilityName in saveData.learnedAbilitys){
            learnedAbilitys.Add(AbilityManager.getAbilityByName(abilityName));
        }
        lastTimeAbilityUsed = saveData.lastTimeAbilityUsed;
    }
    public AbilityControllerSaveData GetSaveData(){
        return new AbilityControllerSaveData(this);
    }
}
[System.Serializable]
public class AbilityControllerSaveData{
    public Vector3 firePointPosition;
    public Vector3 hitPointPosition;
    public List<string> learnedAbilitys;
    public List<(string, float)> lastTimeAbilityUsed;
    public AbilityControllerSaveData(AbilityController abilityController){
        learnedAbilitys = new List<string>();
        foreach(Ability ability in abilityController.learnedAbilitys){
            learnedAbilitys.Add(ability.name);
        }
        lastTimeAbilityUsed = abilityController.lastTimeAbilityUsed;
    }
}

