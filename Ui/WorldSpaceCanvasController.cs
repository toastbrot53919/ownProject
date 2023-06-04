using UnityEngine;

public class WorldSpaceCanvasController : MonoBehaviour
{
    public static WorldSpaceCanvasController Instance;

    public GameObject damageNumberPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnDamageNumber(float damage, Vector3 position)
    {
        if (damageNumberPrefab == null)
        {
            Debug.LogError("DamageNumberPrefab is not assigned in the WorldSpaceCanvasController component.");
            return;
        }

        GameObject damageNumberInstance = Instantiate(damageNumberPrefab, position, Quaternion.identity, transform);
        damageNumberInstance.gameObject.SetActive(true);
        DamageNumberController damageNumberController = damageNumberInstance.GetComponent<DamageNumberController>();


        if (damageNumberController != null)
        {
            damageNumberController.SetDamageValue(damage);
        }
        else
        {
            Debug.LogError("DamageNumberController component is missing on the DamageNumberPrefab.");
            Destroy(damageNumberInstance);
        }
    }
}
