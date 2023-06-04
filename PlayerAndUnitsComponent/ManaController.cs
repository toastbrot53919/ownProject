using UnityEngine;

public class ManaController : MonoBehaviour
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


}
