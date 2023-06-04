using UnityEngine;

public class SkillTreeMenuController : MonoBehaviour
{
    public GameObject[] skillTrees;
    public int currentSkillTree = 0;
    private void Start()
    {
         skillTrees[currentSkillTree].SetActive(true);
    }

    public void SwitchSkillTree(int index)
    {
        if (index < 0 || index >= skillTrees.Length) return;

        skillTrees[currentSkillTree].SetActive(false);
        skillTrees[index].SetActive(true);
        currentSkillTree = index;
    }
}
