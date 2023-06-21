using UnityEngine;

public static class BuffManager{
    private static Buff objectBuffer;
    public static Buff getBuffPrefabByString(string name){
        objectBuffer = Resources.Load<Buff>("Buffs/"+name);        
        if(objectBuffer == null){
            Debug.LogError("BuffManager: getBuffPrefabByString: objectBuffer is null");
        }
        Debug.Log("BuffManager: getBuffPrefabByString: "+objectBuffer.name);
        return objectBuffer;
    }
}