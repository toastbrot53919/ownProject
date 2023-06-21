using UnityEngine;

public static class QuestManager{
    private static Quest objectBuffer;
    public static Quest getQuestPrefabByString(string name){
        objectBuffer = Resources.Load<Quest>("Quests/"+name);        
        if(objectBuffer == null){
            Debug.LogError("QuestManager: getQuestPrefabByString: objectBuffer is null");
        }
        Debug.Log("QuestManager: getQuestPrefabByString: "+objectBuffer.name);
        return objectBuffer;
    }
}