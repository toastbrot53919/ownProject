using UnityEngine;
[CreateAssetMenu(fileName = "FrostBoltMastery", menuName = "Skill/FrostBoltMastery", order = 1)]
public class FrostBoltMastery : Skill
{
    [SerializeField]
    private Buff frozenBuff;

    public void OnEnable(){
        skillName = "FrostBoltMastery";
        frozenBuff = BuffFactory.CreateBuff("Frozen", 5f, true, 3, new StatsModifier());
        frozenBuff.AddInvokeOnApply(OnApplyFrozen);
    }
    public void OnApplyFrozen(BuffInstance instance,GameObject target){
        
        Debug.Log("OnApplyFrozen"+skillName);
        IStunnable stunnable = target.GetComponent<IStunnable>();
        if(stunnable != null){
            stunnable.Stun(10f);
        }
    }
    public override void ApplySkill(CharacterStats playerStats)
    {
        base.ApplySkill(playerStats);
       playerStats.GetComponent<BuffSystem>().AddEventForBuff("Chill","OnApply",OnApplyCheckForChillStacks);

    }

    public override void RemoveSkill(CharacterStats playerStats)
    {
        base.RemoveSkill(playerStats);
               playerStats.GetComponent<BuffSystem>().RemoveEventForBuff("Chill","OnApply",OnApplyCheckForChillStacks);

    }

    public void OnApplyCheckForChillStacks(BuffInstance instance,GameObject target){
        Debug.Log("OnApplyCheckForChillStacks");
        
        if(instance.buff.buffName == "Chill"){
            if(instance.currentStacks >= instance.buff.maxStacks){
                Debug.Log("OnApplyCheckForChillStacks"+"Triggerd");
                instance.currentStacks = 0;
                instance.target.GetComponent<BuffSystem>().RemoveBuff(instance.buff,instance.buffSystemCaster);
                instance.target.GetComponent<BuffSystem>().AddBuff(frozenBuff, instance.target,instance.buffSystemCaster);
            }
        }
    }
    
   
}
