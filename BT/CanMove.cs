using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CanMove : Conditional
{
	InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
	InGameSceneCharMove inGameSceneCharMove;
	CharState charState;

	public override void OnStart()
	{
		inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
		charState = this.gameObject.transform.GetComponent<CharState>();
		inGameSceneCharMove = gameObject.transform.GetComponent<InGameSceneCharMove>();
	}


	public override TaskStatus OnUpdate()
	{
		if (charState.nowState == CharState.NowState.isFindingBush ||
			charState.nowState == CharState.NowState.isReadyForAttack) return TaskStatus.Failure;

		// 적이 멀리 있으면
		// 걸어가야지
		if (inGameSceneCheckTargetAndGetDistance.isEnemyOutOfFindRange() == true)
        {
			charState.nowState = CharState.NowState.isWalk;
		}
		else
        {
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}
}