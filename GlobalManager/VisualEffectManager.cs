using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualEffectManager", menuName = "ScriptableObjects/VisualEffectManager", order = 1)]
public class VisualEffectManager : ScriptableObject
{
    [System.Serializable]
    public struct VisualEffect
    {
        public string effectName;
        public GameObject effectPrefab;
    }

    public List<VisualEffect> visualEffects;

    public GameObject GetEffectPrefab(string effectName)
    {
        foreach (var effect in visualEffects)
        {
            if (effect.effectName == effectName)
            {
                return effect.effectPrefab;
            }
        }
        Debug.LogError($"No effect with name {effectName} found!");
        return null;
    }
}
