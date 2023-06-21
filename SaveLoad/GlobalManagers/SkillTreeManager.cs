using UnityEngine;

public static class SkillTreeManager{
    private static SkillTree objectBuffer;
    public static SkillTree getSkillTreePrefabByString(string name){
        objectBuffer = Resources.Load<SkillTree>("SkillTrees/"+name);        
        if(objectBuffer == null){
            Debug.LogError("SkillTreeManager: getSkillTreePrefabByString: objectBuffer is null");
        }
        return objectBuffer;
    }
}