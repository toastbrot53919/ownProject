using UnityEngine;
public class isStunnableController : MonoBehaviour,IStunnable{

    public bool stunned;

    float timeAtStunStart;
    float stunDuration;

    bool IStunnable.stunned { get => stunned ;}

    float IStunnable.timeAtStunStart => timeAtStunStart;

    float IStunnable.stunDuration => stunDuration;

    VisualEffectController visualEffectController;

    private void Start()
    {
        visualEffectController = GetComponent<VisualEffectController>();
    }
    public void Stun(float duration){
        stunned = true;
        timeAtStunStart = Time.time;
        stunDuration = duration;
        visualEffectController.SpawnEffect("Stun",duration);

    }
    public bool isStunned(){
        
            if(Time.time - timeAtStunStart >= stunDuration){
                stunned = false;
            }
        return stunned;
        
    }
}