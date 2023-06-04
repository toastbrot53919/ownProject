using System.Collections.Generic;
using UnityEngine;

public enum effectUnitPosition{
    overHead,
    underFeet,
    stomach
}

public class VisualEffectController : MonoBehaviour
{
    public VisualEffectManager visualEffectManager;

    public Transform positionOverHead;
    public Transform positionUnderFeet;
    public Transform positionStomach;

    private Transform goalTransform;
    private List<(GameObject,float)> effectInstances = new List<(GameObject,float)>();
    public void SpawnEffect(string effectName, float effectDuration = 0, effectUnitPosition effectPosition = effectUnitPosition.overHead)
    {
        if(effectPosition == effectUnitPosition.overHead){
            goalTransform = positionOverHead;
        }
        if(effectPosition == effectUnitPosition.underFeet){
            goalTransform = positionUnderFeet;
        }
        if(effectPosition == effectUnitPosition.stomach){
            goalTransform = positionStomach;
        }
        if(findEffect(effectName) != null){
            updateDurrationOfEffect(effectName,effectDuration);
            return;
        }
         GameObject effectPrefab = visualEffectManager.GetEffectPrefab(effectName);
        if (effectPrefab != null)
        {
            GameObject effectInstance = Instantiate(effectPrefab, Vector3.zero, Quaternion.identity, goalTransform);
            effectInstance.transform.localPosition = Vector3.zero;
            effectInstances.Add((effectInstance,Time.time+effectDuration));

        }
        else{
            Debug.LogWarning($"Visual effect '{effectName}' not found.");
        }
    }
    

   
     public GameObject findEffect(string effectName){
        for (int i = effectInstances.Count - 1; i >= 0; i--)
        {
            (GameObject, float) effectInstance = effectInstances[i];
            if (effectInstance.Item1.name == effectName)
            {
                return effectInstance.Item1;
            }
        }
        return null;
     }
    public void RemoveEffect(string effectName){
        for (int i = effectInstances.Count - 1; i >= 0; i--)
        {
            (GameObject, float) effectInstance = effectInstances[i];
            if (effectInstance.Item1.name == effectName)
            {
                Destroy(effectInstance.Item1);
                effectInstances.RemoveAt(i);
                Debug.Log("effect removed");
            }
        }
    }
    public void updateDurrationOfEffect(string effectName,float effectDuration){
        for (int i = effectInstances.Count - 1; i >= 0; i--)
        {
            (GameObject, float) effectInstance = effectInstances[i];
            if (effectInstance.Item1.name == effectName)
            {
                effectInstances[i] = (effectInstance.Item1,Time.time+effectDuration);
                Debug.Log("effect updated");
            }
        }
    }
    void Update(){
        for (int i = effectInstances.Count - 1; i >= 0; i--)
        {
            (GameObject, float) effectInstance = effectInstances[i];
            if (effectInstance.Item2 < Time.time)
            {
                Destroy(effectInstance.Item1);
                effectInstances.RemoveAt(i);
                Debug.Log("effect removed");
            }
        }
    }
    

}
