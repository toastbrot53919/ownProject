using UnityEngine;

public static class ItemManager{
    private static Item objectBuffer;
    public static Item getItemyString(string name){
        objectBuffer = Resources.Load<Item>("Items/"+name);        
        if(objectBuffer == null){
            Debug.LogError("ItemManager: getItemPrefabByString: objectBuffer is null");
        }
        Debug.Log("ItemManager: getItemPrefabByString: "+objectBuffer.name);
        return objectBuffer;
    }
}