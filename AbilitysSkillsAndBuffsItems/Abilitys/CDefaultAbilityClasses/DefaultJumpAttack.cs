using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/DefaultJumpAttack")]
public class DefaultJumpAttack : DefaultAbility
{
    public float dashDistance = 20f;


    public float dashSpeed = 40f;
    public GameObject abilityObjectPrefab;
    public GameObject instanstiatedObject;
    private Vector3 dashDirection;
    private CorotineController corotineController;
    private MovementController movementController;
    private InputController inputController;
    public override void Activate(AbilityData abilityData)
    {
        base.Activate(abilityData);
        if(instanstiatedObject != null){
            Destroy(instanstiatedObject);
        }
        movementController = abilityData.casterStats.GetComponent<MovementController>();
        inputController = abilityData.casterStats.GetComponent<InputController>();
        corotineController = abilityData.casterStats.GetComponent<CorotineController>();
        dashDirection = abilityData.casterStats.transform.forward;
        corotineController.removeCorotine("JumpAttack");
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
        instanstiatedObject = Instantiate(abilityObjectPrefab,abilityData.casterStats.transform.position,abilityData.casterStats.transform.rotation);
        AbilityObject abilityObject = instanstiatedObject.GetComponent<AbilityObject>();
        targetsHit = new List<GameObject>();
        abilityObject.transform.SetParent(abilityData.casterStats.transform);
        abilityObject.transform.localPosition = new Vector3(0,0,0);
        abilityObject.transform.localRotation = Quaternion.identity;
        abilityObject.data = abilityData;
        abilityObject.ParentAbility = this;
        // Start the dash
        movementController.StartDash(dashSpeed,dashDistance,dashDirection,"JumpAttack");

    }


    List<GameObject> targetsHit;
    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        // Keep list of targets hit


      
        base.OnAbilityObjectHit(abilityObject, target);
        Debug.Log("Jump Attack Hit");
        if (target.GetComponent<HealthController>() != null)
        {
            target.GetComponent<HealthController>().TakeDamage(abilityObject.data.damage, abilityObject.data.casterStats.gameObject);
            corotineController.addCorotine(DestroyAfterTime(abilityObject,0.1f),"DestroyAfterTimeJumpAttack");
        }

    }
    //Corotine to remove the ability object after a certain amount of time
    public IEnumerator DestroyAfterTime(AbilityObject abilityObject,float time)
    {
        yield return new WaitForSeconds(time);
        if(abilityObject != null){
            Destroy(abilityObject.gameObject);
        }

        movementController.StopDash("JumpAttack");
        abilityObject.data.casterStats.GetComponent<CorotineController>().removeCorotine("DestroyAfterTimeJumpAttack");
        

    }
}
