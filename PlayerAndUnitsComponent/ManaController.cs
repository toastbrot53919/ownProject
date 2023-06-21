using UnityEngine;
[System.Serializable]
public class ManaController : MonoBehaviour,ICanStoreAndLoad<ManaControllerSaveData>
{
    public float maxMana;
    public float currentMana;
    private CharacterStats characterStats;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        characterStats.StatsChanged += updateMaxMana;
        
        currentMana = maxMana;

    }
    private void updateMaxMana()
    {
     maxMana = characterStats.maxMana;
    }
    public void updateMana()
    {
        currentMana = maxMana;
    }
    public void UseMana(float manaCost)
    {
        if (HasSufficientMana(manaCost))
        {
            currentMana -= manaCost;
        }
    }

    public bool HasSufficientMana(float manaCost)
    {
        return currentMana >= manaCost;
    }

    public void RegenerateMana(float manaAmount)
    {
        currentMana += manaAmount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }
    public ManaControllerSaveData GetSaveData()
    {
        return new ManaControllerSaveData(this);
    }
    public void LoadFromSaveData(ManaControllerSaveData saveData)
    {
        currentMana = saveData.mana;
        maxMana = saveData.maxMana;
    }


}
[System.Serializable]
public class ManaControllerSaveData
{
    public float mana;
    public float maxMana;
    public ManaControllerSaveData(ManaController manaController)
    {
        mana = manaController.currentMana;
        maxMana = manaController.maxMana;
    }
}
