using UnityEngine;
public static class BuffFactory
{
    public static Buff CreateBuff(string buffName = "Default Buff", float duration = 5f, bool stackable = true, int maxStacks = 3, StatsModifier statModifier = null)
    {
        Buff newBuff = ScriptableObject.CreateInstance<Buff>();
        newBuff.buffName = buffName;
        newBuff.duration = duration;
        newBuff.stackable = stackable;
        newBuff.maxStacks = maxStacks;
        newBuff.statModifier = statModifier ?? new StatsModifier();

        return newBuff;
    }
}
