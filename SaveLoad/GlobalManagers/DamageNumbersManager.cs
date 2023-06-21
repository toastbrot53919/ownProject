using UnityEngine;

public static class DamageNumbersManager{
   private static GameObject objectBuffer;
    public static GameObject getDamageNumberPrefabByString(string name){
        objectBuffer = Resources.Load<GameObject>("DamageNumbers/"+name);        
        if(objectBuffer == null){
            Debug.LogError("DamageNumbersManager: getDamageNumberPrefabByString: objectBuffer is null");
        }
        return objectBuffer;
    }
}