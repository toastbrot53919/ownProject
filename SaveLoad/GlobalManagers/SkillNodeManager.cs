using UnityEngine;

public static class SkillNodeManger{
    private static GameObject objectBuffer;
    public static GameObject getSkillNodePrefabByString(string name){
        objectBuffer = Resources.Load<GameObject>("SkillNodes/"+name);        
        if(objectBuffer == null){
            Debug.LogError("SkillNodeManger: getSkillNodePrefabByString: objectBuffer is null");
        }
        return objectBuffer;
    }
}