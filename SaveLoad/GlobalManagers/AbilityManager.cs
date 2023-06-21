
using UnityEngine;
public static class AbilityManager
{

    private static Ability objectBuffer;
    public static Ability getAbilityByName(string abilityName)
    {

        objectBuffer = Resources.Load<Ability>("Abilitys/" + abilityName);
        if (objectBuffer == null)
        {
            Debug.LogError("AbilityManager: getAbilityByName: objectBuffer is null | " + abilityName + " not found");
            return null;
        }
        else{
            Debug.Log("AbilityManager: getAbilityByName: " + objectBuffer.name);

        }
        
        return objectBuffer;
    }

}