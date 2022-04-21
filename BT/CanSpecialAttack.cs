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

        if (inGameSceneCheckTargetAndGetDistance.distanceToTarget == 0) return TaskStatus.Failure;

        if ((charState.attackRange >= inGameSceneCheckTargetAndGetDistance.distanceToTarget) &&
            (charState.skillPoint >= 100) )
        {
            return TaskStatus.Success;
        }
		return TaskStatus.Failure;
	}
}