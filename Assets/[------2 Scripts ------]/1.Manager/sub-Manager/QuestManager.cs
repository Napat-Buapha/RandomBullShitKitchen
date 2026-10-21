using UnityEngine;

public class QuestManager : MonoBehaviour
{
    
    GameManager _gm;
    [Header("Reward")]
    [field:SerializeField] public int baseGoldReward {get; private set;} = 15;
    [field:SerializeField] public int bonusTurnReward {get; private set;} = 3; //Per Turn


    [Header("Level Customizer")]
    [field: SerializeField] public int baseTargetScore { get; private set; }
    [field: SerializeField] public float scoreIncreaseRate { get; private set; }
    public void Init(GameManager gm)
    {
        _gm = gm;
    }



    public float CalculateLevelTargetScore(int baseValue, int level, float rate)
    {
        //Expotential Graph
        return baseValue * Mathf.Pow(1f + rate, level - 1);
    }

    public void CalculateReward()
    {
        RewardData rewardData = new RewardData
        {
            goldAmout = baseGoldReward,
            turnLeft = _gm.TurnManager.turnLimit - _gm.TurnManager.currentTurn,
            bonusMultipler = bonusTurnReward
        };

        _gm.UiManager.EnableWinPanel(rewardData);
        _gm.ResourceManager.ModifyGoldAmout(rewardData.goldAmout + rewardData.turnLeft * rewardData.bonusMultipler);
    }
}

public struct RewardData
{
    public int goldAmout;
    public int turnLeft;
    public int bonusMultipler;
}