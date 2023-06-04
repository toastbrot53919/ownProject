using UnityEngine;

[System.Serializable]
public class Reward
{
    public string rewardId;
    public string rewardName;
    public int quantity;

    public Reward(string rewardId, string rewardName, int quantity)
    {
        this.rewardId = rewardId;
        this.rewardName = rewardName;
        this.quantity = quantity;
    }
}
