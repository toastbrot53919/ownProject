using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Buff : ScriptableObject
{
    public Buff()
    {
      statModifier = new StatsModifier();
    }
    public StatsModifier statModifier;
    public string buffName;
    public float duration;
    public bool stackable;
    public int maxStacks;

    public event System.Action<BuffInstance,GameObject> OnApply;
    public event System.Action<BuffInstance,GameObject> OnFade;
    public event System.Action<BuffInstance,GameObject> OnHit;



    //Delete? Buff are not static, events needed to be added every time
    public virtual void InvokeOnApply(BuffInstance buffInstance,GameObject interactTarget)
    {
        OnApply?.Invoke(buffInstance,interactTarget);

    }
    public virtual void AddInvokeOnApply(System.Action<BuffInstance,GameObject> action)
    {
        OnApply += action;
    }
    public virtual void InvokeOnFade(BuffInstance buffInstance,GameObject interactTarget)
    {
        OnFade?.Invoke(buffInstance,interactTarget);
    }
    public virtual void AddInvokeOnFade(System.Action<BuffInstance,GameObject> action)
    {
        OnFade += action;
    }
    public virtual void InvokeOnHit(BuffInstance buffInstance,GameObject interactTarget)
    {
        OnHit?.Invoke(buffInstance,interactTarget);
    }
    public virtual void AddInvokeOnHit(System.Action<BuffInstance,GameObject> action)
    {
        OnHit += action;
    }
  
}
