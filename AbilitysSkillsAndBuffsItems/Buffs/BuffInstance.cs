using UnityEngine;

public class BuffInstance : ICanStoreAndLoad<BuffInstanceSaveData>
{
    public Buff buff;
    public GameObject target;
    public int currentStacks;
    public float remainingDuration;
    StatsModifier characterStatsTarget;

    public BuffSystem buffSystemCaster;

    public BuffInstance(Buff buff, GameObject target, GameObject caster, int initialStacks, float initialDuration)
    {
        this.buff = buff;
        this.target = target;
        this.currentStacks = initialStacks;
        this.remainingDuration = initialDuration;
        if (characterStatsTarget == null)
        {
            characterStatsTarget = target.GetComponent<BuffSystem>().TotalstatsModifier;
        }
        else
        {
            Debug.LogError("characterStatsTarget is null");
        }
        if (buffSystemCaster != null)
        {
            buffSystemCaster = caster.GetComponent<BuffSystem>();
        }
    }


    public void Update()
    {
        remainingDuration -= Time.deltaTime;

        if (remainingDuration <= 0)
        {
            OnBuffFade();
            target.GetComponent<BuffSystem>().RemoveBuff(buff, buffSystemCaster); // add this line
            return;
        }

        // Perform any other update logic specific to the buff
    }

    public void Refresh(float duration)
    {
        remainingDuration = duration;
    }

    public void AddStack()
    {
        currentStacks++;
        OnBuffApply();
    }



    public void OnBuffApply()
    {
        // Perform any actions or apply stat changes when the buff is applied
        if (buff.statModifier != null)
        {
            characterStatsTarget.Add(buff.statModifier);
            target.GetComponent<CharacterStats>().UpdateSubStats();
        }
        buff.InvokeOnApply(this, target);
        buffSystemCaster.CallEventFromBuff(buff.buffName, "OnApply", this, target);

    }

    public void OnBuffFade()
    {
        // Perform any actions or apply stat changes when the buff is applied
        if (buff.statModifier != null)
        {
            characterStatsTarget.Sub(buff.statModifier);
            target.GetComponent<CharacterStats>().UpdateSubStats();
        }
        buff.InvokeOnFade(this, target);
        buffSystemCaster.CallEventFromBuff(buff.buffName, "OnFade", this, target);

    }

    public void OnBuffHit()
    {
        // Perform any actions or apply effects when the buff "hits" (e.g., dealing damage or applying a debuff)
        buff.InvokeOnHit(this, target);
        buffSystemCaster.CallEventFromBuff(buff.buffName, "OnHit", this, target);
    }
    public BuffInstanceSaveData GetSaveData()
    {
        return new BuffInstanceSaveData(this);
    }
    public void LoadFromSaveData(BuffInstanceSaveData saveData)
    {
        buff = BuffManager.getBuffPrefabByString(saveData.buffName);
        currentStacks = saveData.currentStacks;
        remainingDuration = saveData.remainingDuration;
        target = GameObject.Find(saveData.targetName);
        buffSystemCaster = GameObject.Find(saveData.casterName).GetComponent<BuffSystem>();
        if (target == null)
        {
            Debug.LogError("target not found!!!! MAYBE YOU NEED TO LOAD SCENE FIRST");
        }

    }
}
[System.Serializable]
public class BuffInstanceSaveData
{
    public string buffName;
    public int currentStacks;
    public float remainingDuration;
    public string targetName;
    public string casterName;

    public BuffInstanceSaveData(BuffInstance buffInstance)
    {
        buffName = buffInstance.buff.buffName;
        currentStacks = buffInstance.currentStacks;
        remainingDuration = buffInstance.remainingDuration;
        targetName = buffInstance.target.name;
        casterName = buffInstance.buffSystemCaster.name;
    }
}