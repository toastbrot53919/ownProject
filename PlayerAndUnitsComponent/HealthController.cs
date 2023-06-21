using UnityEngine;

public class HealthController : MonoBehaviour,ICanStoreAndLoad<HealthControllerSaveData>
{
    private CharacterStats characterStats;
    public string Name;
    public float maxHealth;
    public float currentHealth;
    public GameObject damageTextPrefab;
    private QuestSystem questSystem;

    void UpdateMaxHealth()
    {
        maxHealth = characterStats.maxLife;
    }
    public void updateHealth()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        Debug.Log("HealthController Start");
        characterStats = GetComponent<CharacterStats>();
        characterStats.StatsChanged+=UpdateMaxHealth;
        UpdateMaxHealth();
        currentHealth = maxHealth;
        damageTextPrefab = GameObject.Find("DamageTextTemplate");
        GlobalUnitController.addUnit(gameObject);


        

    }

    public void TakeDamage(float damage,GameObject from)
    {
        currentHealth -= damage;
        ShowDamageNumbers(damage);
        if (currentHealth <= 0)
        {
            if(from.GetComponent<QuestSystem>() != null)
            {
                from.GetComponent<QuestSystem>().UpdateQuestObjective("kill:"+Name);
            }
            Die();
        }
    }

    private void Die()
    {
        // Implement death behavior, such as playing death animation, dropping loot, etc.
        GlobalUnitController.units.Remove(gameObject);
        Destroy(gameObject);
    }
    public void ShowDamageNumbers(float damage)
    {
        if (WorldSpaceCanvasController.Instance == null)
        {
            Debug.LogError("WorldSpaceCanvasController instance is not present in the scene.");
            return;
        }

        WorldSpaceCanvasController.Instance.SpawnDamageNumber(damage, transform.position + Vector3.up * 2f);
    }
    public void LoadFromSaveData(HealthControllerSaveData saveData)
    {
        currentHealth = saveData.health;
        maxHealth = saveData.maxHealth;
    }
    public HealthControllerSaveData GetSaveData()
    {
        return new HealthControllerSaveData(this);
    }

}
[System.Serializable]
public class HealthControllerSaveData{
    public float health;
    public float maxHealth;

    public HealthControllerSaveData(HealthController healthController){
        health = healthController.currentHealth;
        maxHealth = healthController.maxHealth;
    }
}