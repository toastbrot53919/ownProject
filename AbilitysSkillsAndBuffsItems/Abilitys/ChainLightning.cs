using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/ChainLightningAbility")]
public class ChainLightning : DefaultAbility
{
    public int maxBounceCount = 5;
    public float bounceRange = 5f;
    public GameObject chainLightningPrefab;
    public Material lineMaterial;

    [SerializeField]
    private float ChainLightningTickness = 1f;

    public override void Activate(AbilityData abilityData)
    {
        Transform firePoint = abilityData.casterStats.GetComponent<AbilityController>().firePoint;

        AbilityObject abilityObject = Instantiate(chainLightningPrefab, firePoint.transform.position, firePoint.transform.rotation).GetComponent<AbilityObject>();
        if(abilityObject == null)
        {
            Debug.LogError("AbilityObject is null");
        }
        abilityObject.data = abilityData;
        abilityObject.ParentAbility = this;
    }

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        VisualEffectController visualEffectController = target.GetComponent<VisualEffectController>();
        HealthController healthController = target.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(abilityObject.data.damage, abilityObject.data.casterStats.gameObject);
            visualEffectController.SpawnEffect("LightningHitWeak",1.4f,effectUnitPosition.underFeet);
        }

        List<GameObject> hitTargets = new List<GameObject> { target };
        Vector3 lastPosition = abilityObject.transform.position;

        for (int i = 0; i < maxBounceCount; i++)
        {
            Collider[] colliders = Physics.OverlapSphere(lastPosition, bounceRange);
            GameObject newTarget = null;
            foreach (Collider collider in colliders)
            {
                if (!hitTargets.Contains(collider.gameObject) && collider.gameObject.GetComponent<HealthController>() != null && collider.gameObject != abilityObject.data.casterStats.gameObject)
                {
                    newTarget = collider.gameObject;
                    hitTargets.Add(newTarget);
                    break;
                }
            }

            if (newTarget != null)
            {
                target = newTarget;
                healthController = target.GetComponent<HealthController>();
                 visualEffectController = target.GetComponent<VisualEffectController>();
                if (healthController != null)
                {
                    healthController.TakeDamage(abilityObject.data.damage, abilityObject.data.casterStats.gameObject);
                    visualEffectController.SpawnEffect("LightningHitWeak",1.4f,effectUnitPosition.underFeet);
                }

                GameObject lineObject = new GameObject();
                LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.material = lineMaterial;
                lineRenderer.startWidth = ChainLightningTickness;
                lineRenderer.endWidth = ChainLightningTickness;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, lastPosition);
                lineRenderer.SetPosition(1, visualEffectController.positionStomach.position);
                Destroy(lineObject, 0.3f);

                lastPosition = visualEffectController.positionStomach.position;
            }
            else
            {
                break;
            }
        }
    }
}
