using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Abilities/DefaultDashAbility")]
public class DefaultDashAbility : DefaultAbility
{
    public float dashDistance = 10f;


    public float dashSpeed = 1f;

    private Vector3 dashDirection;
    private CorotineController corotineController;

    private InputController inputController;
    public new void Awake()
    {
        base.Awake();
        abilityName = "Default Dash Ability";
        abilityDescription = "This is a default dash ability";
        BaseAbilityStats.cooldown = 1;
        BaseAbilityStats.intelligenceScaling = 0;
    }
    public override void Activate(AbilityData abilityData)
    {
        base.Activate(abilityData);
        inputController = abilityData.casterStats.GetComponent<InputController>();
        corotineController = abilityData.casterStats.GetComponent<CorotineController>();
        dashDirection = abilityData.casterStats.transform.forward;
        // Set the dash direction based on the caster's forward direction
        if (inputController.getLeftPressed())
        {
            dashDirection = -abilityData.casterStats.transform.right;
        }
        else if (inputController.getRightPressed())
        {
            dashDirection = abilityData.casterStats.transform.right;
        }
        else if (inputController.getUpPressed())
        {
            dashDirection = abilityData.casterStats.transform.forward;
        }
        else if (inputController.getDownPressed())
        {
            dashDirection = -abilityData.casterStats.transform.forward;
        }
        else
        {
            dashDirection = abilityData.casterStats.transform.forward;
        }

        Debug.Log("Dash Ability"+dashDirection);
        // Start the dash
        corotineController.removeCorotine("Dash");
        corotineController.addCorotine(Dash(abilityData.casterStats.transform), "Dash");//NEEEEDS DELAY

    }

    private IEnumerator Dash(Transform casterTransform)
    {

        float distanceAlreadyTraveld =  0f;
        while (distanceAlreadyTraveld < dashDistance)
        {

            // Calculate the end position of the dash
            casterTransform.position += dashDirection * dashSpeed * Time.deltaTime;
            distanceAlreadyTraveld += dashSpeed * Time.deltaTime;
            yield return null;
        }
        
        // Calculate the end position of the dash
    
    }

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        // This ability does not interact with other objects upon hitting them
    }
}
