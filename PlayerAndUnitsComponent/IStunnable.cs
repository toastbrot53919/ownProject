//Interface isStunnable if GameObject can be stunned,contain bool isStunned
// Path: Assets\Scripts\PlayerAndUnitsComponent\IStunnable.cs
using UnityEngine;
public interface IStunnable : ICanStoreAndLoad<StunnableSaveData>

{
    bool stunned { get; }

   
    float timeAtStunStart{ get; }
    float stunDuration{ get ;}


    void Stun(float duration);
    public bool isStunned();

    
}
[System.Serializable]
public class StunnableSaveData{
    public bool stunned;
    public float timeAtStunStart;
    public float stunDuration;
    public StunnableSaveData(IStunnable stunnable){
        stunned = stunnable.stunned;
        timeAtStunStart = stunnable.timeAtStunStart;
        stunDuration = stunnable.stunDuration;
    }
}