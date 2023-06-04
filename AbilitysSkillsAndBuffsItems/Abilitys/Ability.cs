using UnityEngine;
//TODO , make removeOnCollision flag to Ability.Abilityobject Universal
public abstract class Ability : ScriptableObject
{
    [Header("General")]
    public string abilityName;
    public string abilityDescription;
    public Sprite icon;
    [Header("Stats")]
    public AbilityStats BaseAbilityStats;
    public AbilityStats TotalAbilityStats;
    //manacost not implemented yet
    //Alles hier druntetr soll in ABiltiyStats verschoben werden


    public float stunDuration;


    [Header("Specific")]
    public AbilityModifierManager abilityModifierManager;
    public string animationName;
    public float lastTimeUsed = 0;
    public bool useUpdate = false;
    public AnimingMode animingMode = AnimingMode.Default;


    public void updateAbilityStats()
    {
        TotalAbilityStats.setZero();
        TotalAbilityStats.addStats(BaseAbilityStats);
        if(abilityModifierManager != null)
        {
            TotalAbilityStats.addStats(abilityModifierManager.CalculateModifiedValue());
        }
    }
    public void AddModifier(AbilityModifiers modifiers)
    {
        if(abilityModifierManager == null)
        {
            abilityModifierManager = new AbilityModifierManager();
        }
        abilityModifierManager.AddModifier(modifiers);
        updateAbilityStats();
    }
    public void RemoveModifier(AbilityModifiers modifier)
    {
        if(abilityModifierManager == null)
        {
            abilityModifierManager = new AbilityModifierManager();
        }
        abilityModifierManager.RemoveModifier(modifier);
        updateAbilityStats();
    }
    public Coroutine activationRoutine ;
    public abstract void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target);
    public virtual void OnAbilityObjectRemoved(){}
    public abstract void Activate(AbilityData abilityData);

    public virtual void PreActivateAbility(AbilityData abilityData){

    }
    public virtual void Deactivate(){
     
    }

    public virtual void OnUpdate(){}

     public delegate void AbilityEvent(Ability ability);
     public delegate void AbilityObjectEvent(AbilityObject abilityObject, GameObject target);
     public event AbilityObjectEvent OnAbilityObjectSpawnedEvent;
     public event AbilityObjectEvent OnAbilityObjectHitEvent;
    public event AbilityEvent OnAbilityActivated;

    public float getLastTimeUsed()
    {
        return lastTimeUsed;
    }
    public float setLastTimeUsed(float time)
    {
        return lastTimeUsed = time;
    }
    protected void RaiseOnObjectSpawned(AbilityObject abilityObject,GameObject target)
    {
        OnAbilityObjectSpawnedEvent?.Invoke(abilityObject,null);
    }
   
    protected void RaiseOnObjectHit(AbilityObject abilityObject, GameObject target)
    {
        OnAbilityObjectHitEvent?.Invoke(abilityObject, target);
    }
    protected void RaiseOnAbilityActivated()
    {
        OnAbilityActivated?.Invoke(this);
    }
    

}

public class AbilityData
{
    public GameObject Target;
    public CharacterStats CasterStats;
    public AbilityController CasterController;
    public CharacterCombatController CasterCombatController;

    public Vector3 TargetDirection;
    public Vector3 TargetPosition;
    public float damage;
    public float projectileSpeed;
    public float stunDuration;
    public float OnHitInterval = 0.5f;
    AbilityData clone()
    {
        return (AbilityData)MemberwiseClone();
    }

}
public enum AnimingMode
{
 Default,
 PrePositionPlacement,


}