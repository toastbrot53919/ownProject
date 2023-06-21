
using UnityEngine;

public static class SkillManager{

    private static Skill objectBuffer;
     public static Skill getSkillByName(string skillName)
     {

         objectBuffer =  Resources.Load<Skill>("Skills/" + skillName);
            if (objectBuffer == null)
            {
                Debug.LogError("SkillManager: getSkillByName: objectBuffer is null");
            }
            Debug.Log("SkillManager: getSkillByName: " + objectBuffer.name);
            return objectBuffer;
     }

}