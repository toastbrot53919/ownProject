using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator meshAnimator;
    [SerializeField] public List<VisualEffectData> visualEffectDataList;

    [SerializeField] public List<animationDelay> animationCastDelays;
    [SerializeField] public List<animationDelay> animationLockTimings;
    public const string attackAnimationName = "attack";
    public const string OneHandSwordLightAttack1AnimationName = "1HandSwordLightAttack1";
    public const string OneHandSwordLightAttack2AnimationName = "1HandSwordLightAttack2";
    public const string OneHandSwordLightAttack3AnimationName = "1HandSwordLightAttack3";

    public const string idleAnimationName = "idle";


    private Dictionary<string, GameObject> visualEffects;

    private void Awake()
    {
        // Initialize the visualEffects dictionary.
        initAnimationDelays();
        initAnimationLocks();
        visualEffects = new Dictionary<string, GameObject>();
        foreach (VisualEffectData effectData in visualEffectDataList)
        {
            visualEffects.Add(effectData.name, effectData.visualEffectPrefab);
        }
    }
    public void setAnimatorVariavle(string name, float value)
    {
        meshAnimator.SetFloat(name, value);
    }
    public void setAnimatorVariavle(string name, bool value)
    {
        meshAnimator.SetBool(name, value);
    }
    public void PlayAnimation(string animationName)
    {
        // Play the specified animation.
        if (animationName == "attack")
        {
            meshAnimator.SetTrigger("attack");
            return;
        }
        Debug.Log("PlayAnimation: " + animationName);
        meshAnimator.Play(animationName);
    }

    public void ApplyVisualEffect(string effectName, Vector3 position, Quaternion rotation)
    {
        // Instantiate the specified visual effect at the given position and rotation.
        if (visualEffects.TryGetValue(effectName, out GameObject effectPrefab))
        {
            Instantiate(effectPrefab, position, rotation);
        }
        else
        {
            Debug.LogWarning($"Visual effect '{effectName}' not found.");
        }
    }
    public void initAnimationDelays()
    {
        if (animationCastDelays == null)
        {
            animationCastDelays = new List<animationDelay>();
        }
        if (returnAnimationDelay("attack") == 0)
        {
            animationCastDelays.Add(new animationDelay { animationName = "attack", delay = 0.1f });
        }
        if (returnAnimationDelay("1HandSwordLightAttack1") == 0)
        {
            animationCastDelays.Add(new animationDelay { animationName = "1HandSwordLightAttack1", delay = 0.1f });
        }
        if (returnAnimationDelay("1HandSwordLightAttack2") == 0)
        {
            animationCastDelays.Add(new animationDelay { animationName = "1HandSwordLightAttack2", delay = 0.1f });
        }
        if (returnAnimationDelay("1HandSwordLightAttack3") == 0)
        {
            animationCastDelays.Add(new animationDelay { animationName = "1HandSwordLightAttack3", delay = 0.1f });
        }

    }
    public void initAnimationLocks(){
        if(animationLockTimings == null){
            animationLockTimings = new List<animationDelay>();
        }
        if(returnAnimationLockTiming("attack") == 0){
            animationLockTimings.Add(new animationDelay{animationName = "attack", delay = 0.4f});
        }
    }

    public float returnAnimationDelay(string animationName)
    {
        foreach (animationDelay paar in animationCastDelays)
        {
            if (paar.animationName == animationName)
            {
                return paar.delay;
            }
        }
        return 0;
    }
    public float returnAnimationLockTiming(string animationName)
    {
        foreach (animationDelay paar in animationLockTimings)
        {
            if (paar.animationName == animationName)
            {
                return paar.delay;
            }
        }
        return 0;
    }

}

[System.Serializable]
public class VisualEffectData
{
    public string name;
    public GameObject visualEffectPrefab;
}
[System.Serializable]
public class animationDelay
{
    public string animationName;
    public float delay;
}
