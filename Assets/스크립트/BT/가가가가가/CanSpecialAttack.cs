using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanSpecialAttack : Conditional
{
    InGameSceneIfRaidEnd inGameSceneIfRaidEnd;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;


    public override void OnStart()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        inGameSceneIfRaidEnd = GameObject.Find("Manager").GetComponent<InGameSceneIfRaidEnd>();
        charState = gameObject.GetComponent<CharState>();
    }

    public override TaskStatus OnUpdate()
	{
        if ( charState.nowState != CharState.NowState.isReadyForAttack) return TaskStatus.Failure;
        if (inGameSceneIfRaidEnd.waitForRaid == true) return TaskStatus.Failure;
        if (inGameSceneCheckTargetAndGetDistance.target == null)
        {
            charState.nowState = CharState.NowState.isIdle;
            return TaskStatus.Failure;
        }

        if ((inGameSceneCheckTargetAndGetDistance.isEnemyOutOfAttackRange() == false) &&
            (charState.skillPoint >= 100) )
        {
            return TaskStatus.Success;
        }
		return TaskStatus.Failure;
	}
}