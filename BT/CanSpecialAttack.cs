using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanSpecialAttack : Conditional
{
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;


    public override void OnStart()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();

        charState = gameObject.GetComponent<CharState>();
    }

    public override TaskStatus OnUpdate()
	{
        if ( charState.nowState != CharState.NowState.isReadyForAttack) return TaskStatus.Failure;

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