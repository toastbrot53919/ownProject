using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public Dictionary<string, BuffInstance> activeBuffs;
    public List<string> buffsToRemove;
    public StatsModifier TotalstatsModifier;

    // These Events are callbacks of buffs that this BuffSystem Added to another BuffSystem
    public Dictionary<string, List<Action<BuffInstance,GameObject>>> onBuffFromBuffSystemApplying;
    public Dictionary<string, List<Action<BuffInstance,GameObject>>> onBuffFromBuffSystemFading;
    public Dictionary<string, List<Action<BuffInstance,GameObject>>> onBuffFromBuffSystemHitting;


    //onBuffApplyThisBuffSystm
    //onBuffFadeThisBuffSystm
    //onBuffHitThisBuffSystm
    public Dictionary<string, List<Action<BuffInstance>>> onBuffApplyingOnThisBuffSystem;
    public Dictionary<string, List<Action<BuffInstance>>> onBuffFadingOnThisBuffSystem;
    public Dictionary<string, List<Action<BuffInstance>>> onBuffHittingThisBuffSystem;
    //mybe unnecessary

    private void Awake()
    {
        buffsToRemove = new List<string>();
        activeBuffs = new Dictionary<string, BuffInstance>();
        onBuffFromBuffSystemApplying = new Dictionary<string, List<Action<BuffInstance,GameObject>>>();
        onBuffFromBuffSystemFading = new Dictionary<string, List<Action<BuffInstance,GameObject>>>();
        onBuffFromBuffSystemHitting = new Dictionary<string, List<Action<BuffInstance,GameObject>>>();
    }

    private void Update()
    {
        foreach (BuffInstance buffInstance in activeBuffs.Values)
        {
            buffInstance.Update();
        }
        RemoveBuffs();
    }

    private void RemoveBuffs()
    {
        foreach (string buffName in buffsToRemove)
        {
            if (!activeBuffs.ContainsKey(buffName))
            {
                Debug.LogError("buffName not found");
                continue;
            }
            BuffInstance buffInstance = activeBuffs[buffName];
            activeBuffs.Remove(buffName);
            CallEventFromBuff(buffName,"OnFade",buffInstance,null);
            buffInstance.OnBuffFade();
            RemoveEventsForBuff(buffName);
        }
        buffsToRemove.Clear();
    }

    public void AddBuff(Buff buff, GameObject target,BuffSystem caster)
    {
        if (buff == null)
        {
            Debug.LogError("buff is null");
            return;
        }

        if (activeBuffs.ContainsKey(buff.buffName))
        {
            BuffInstance existingBuff = activeBuffs[buff.buffName];
            if (buff.stackable && existingBuff.currentStacks < buff.maxStacks)
            {
                Debug.Log("AddStack" + existingBuff.currentStacks + " " + buff.maxStacks + " " + buff.buffName);
                existingBuff.AddStack();
                existingBuff.Refresh(buff.duration);
                existingBuff.OnBuffApply();

            }
            else
            {
                Debug.Log("Refresh" + buff.buffName);
                existingBuff.Refresh(buff.duration);
                existingBuff.OnBuffApply();
                CallEventFromBuff(buff.buffName,"OnApply",existingBuff,target);
            }
        }
        else
        {
            Debug.Log("AddBuff" + buff.buffName);
            BuffInstance newBuff = new BuffInstance(buff, target,gameObject, 1, buff.duration);
            newBuff.buffSystemCaster = caster;
            activeBuffs.Add(buff.buffName, newBuff);
            newBuff.OnBuffApply();
        }
    }

    public void RemoveBuff(Buff buff,BuffSystem caster)
    {
        Debug.Log("RemoveBuff" + buff.buffName);
        if(activeBuffs.ContainsKey(buff.buffName))
        {
            if(buffsToRemove.Contains(buff.buffName))
            {
                Debug.Log("buffToRemove already contains buffName");
                return;
            }
            buffsToRemove.Add(buff.buffName);
        }

    }

    public BuffInstance GetBuffInstance(string buffName)
    {
        if (activeBuffs.ContainsKey(buffName))
        {
            return activeBuffs[buffName];
        }
        return null;
    }

    public Buff GetBuff(string buffName)
    {
        if (activeBuffs.ContainsKey(buffName))
        {
            return activeBuffs[buffName].buff;
        }
        return null;
    }

    public void AddEventForBuff(string buffName, string eventType, Action<BuffInstance,GameObject> eventCallback)
    {
        if (eventCallback == null)
        {
            Debug.LogError("eventCallback is null");
            return;
        }

        if (eventType == "OnApply")
        {
            if (!onBuffFromBuffSystemApplying.ContainsKey(buffName))
            {
                onBuffFromBuffSystemApplying[buffName] = new List<Action<BuffInstance,GameObject>>();
            }
            onBuffFromBuffSystemApplying[buffName].Add(eventCallback);
        }
        else if (eventType == "OnFade")
        {
            if (!onBuffFromBuffSystemFading.ContainsKey(buffName))
            {
                onBuffFromBuffSystemFading[buffName] = new List<Action<BuffInstance,GameObject>>();
            }
            onBuffFromBuffSystemFading[buffName].Add(eventCallback);
        }
        else if (eventType == "OnHit")
        {
            if (!onBuffFromBuffSystemHitting.ContainsKey(buffName))
            {
                onBuffFromBuffSystemHitting[buffName] = new List<Action<BuffInstance,GameObject>>();
            }
            onBuffFromBuffSystemHitting[buffName].Add(eventCallback);
        }
        else
        {
            Debug.LogError("Invalid eventType");
        }
    }

    public void RemoveEventForBuff(string buffName, string eventType, Action<BuffInstance,GameObject> eventCallback)
    {
        if (eventType == "OnApply")
        {
            if (onBuffFromBuffSystemApplying.ContainsKey(buffName))
            {
                onBuffFromBuffSystemApplying[buffName].Remove(eventCallback);
            }
        }
        else if (eventType == "OnFade")
        {
            if (onBuffFromBuffSystemFading.ContainsKey(buffName))
            {
                onBuffFromBuffSystemFading[buffName].Remove(eventCallback);
            }
        }
        else if (eventType == "OnHit")
        {
            if (onBuffFromBuffSystemHitting.ContainsKey(buffName))
            {
                onBuffFromBuffSystemHitting[buffName].Remove(eventCallback);
            }
        }
        else
        {
            Debug.LogError("Invalid eventType");
        }
    }
    public void RemoveEventsForBuff(string buffName)
    {
        if (onBuffFromBuffSystemApplying.ContainsKey(buffName))
        {
            onBuffFromBuffSystemApplying.Remove(buffName);
        }
        if (onBuffFromBuffSystemFading.ContainsKey(buffName))
        {
            onBuffFromBuffSystemFading.Remove(buffName);
        }
        if (onBuffFromBuffSystemHitting.ContainsKey(buffName))
        {
            onBuffFromBuffSystemHitting.Remove(buffName);
        }
    }
    public void CallEventFromBuff(string buffName, string eventType, BuffInstance buffInstance,GameObject interactTarget)
    {
        if (eventType == "OnApply")
        {
            if (onBuffFromBuffSystemApplying.ContainsKey(buffName))
            {
                foreach (Action<BuffInstance,GameObject> action in onBuffFromBuffSystemApplying[buffName])
                {
                    action(buffInstance,interactTarget);
                }
            }
        }
        else if (eventType == "OnFade")
        {
            if (onBuffFromBuffSystemFading.ContainsKey(buffName))
            {
                foreach (Action<BuffInstance,GameObject> action in onBuffFromBuffSystemFading[buffName])
                {
                    action(buffInstance,interactTarget);
                }
            }
        }
        else if (eventType == "OnHit")
        {
            if (onBuffFromBuffSystemHitting.ContainsKey(buffName))
            {
                foreach (Action<BuffInstance,GameObject> action in onBuffFromBuffSystemHitting[buffName])
                {
                    action(buffInstance,interactTarget);
                }
            }
        }
        else
        {
            Debug.LogError("Invalid eventType");
        }
    }

    
}
