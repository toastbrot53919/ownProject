//Interface isStunnable if GameObject can be stunned,contain bool isStunned
// Path: Assets\Scripts\PlayerAndUnitsComponent\IStunnable.cs
using UnityEngine;
public interface IStunnable
{
    bool stunned { get; }

   
    float timeAtStunStart{ get; }
    float stunDuration{ get ;}


    void Stun(float duration);
    public bool isStunned();
}

