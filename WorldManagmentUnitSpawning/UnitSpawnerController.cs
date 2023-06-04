using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class UnitSpawnerController : MonoBehaviour
{
    // Variables
    public GameObject unitPrefab; // Prefab of the unit to spawn
    public float spawnRange; // Range at which to spawn the unit
    public CharacterStats stats; // The stats for the spawned unit
    public Ability[] abilities; // The abilities for the spawned unit
    public AIState aiState; // The AI state for the spawned unit
    public EquipManager equipManager; // The equip manager for the spawned unit
    public AIController aiController; // The AI controller for the spawned unit

    private Transform playerTransform; // Player transform to check distance

    void Start()
    {
        // Get the player transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool spawned = false;
    void Update()
    {
        // Check if player is within spawn range
        if(playerTransform == null){
            return;
        }
        if(spawned){
            return;
        }
        if(Vector3.Distance(transform.position, playerTransform.position) <= spawnRange)
        {
            // Spawn the unit
            GameObject unit = Instantiate(unitPrefab, transform.position, transform.rotation);
            
            Destroy(this.gameObject);
        }
    }
}
