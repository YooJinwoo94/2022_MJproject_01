using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
//using Pathfinding;




// 우선 적이 보일때까지 앞으로 간다
// 적과의 거리를 측정하여 거리를 계산한다.
// 만약 내 공격 범위 밖이라면 앞으로 간다. 만약 내 공격 범위 안이라면 가는것을 멈춘다.
// 일정 범위내 엄패물이 있는지 확인한다.
// 엄패물로 가서 숨는다.
// 후에 공격한다. 




public class CanMove : Conditional
{
	InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
	InGameSceneCharMove inGameSceneCharMove;
	CharState charState;
	//AIPath aipath;

	public override void OnStart()
	{
		inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
		charState = this.gameObject.transform.GetComponent<CharState>();
		inGameSceneCharMove = gameObject.transform.GetComponent<InGameSceneCharMove>();

		//aipath = this.gameObject.GetComponentInParent<AIPath>();
		//aipath.canMove = true;
	}


	public override TaskStatus OnUpdate()
	{
		if (charState.nowState == CharState.NowState.isAttack ||
	        charState.nowState == CharState.NowState.isReadyForAttack ||
			charState.nowState == CharState.NowState.isFindingBush) return TaskStatus.Failure;

		if (inGameSceneCheckTargetAndGetDistance.isEnemyOutOfAttackRange() == true)
        {
			charState.nowState = CharState.NowState.isWalk;
		}
		else
        {
			//inGameSceneCharMove.stopForward();

			return TaskStatus.Failure;
		}
		return TaskStatus.Success;



		// 공격 범위 밖인 경우
		//if (charState.attackRange <= distanceToTarget.Value)
		//  {
		//	charState.nowState = CharState.NowState.isWalk;
		//aipath.canMove = true;
		//return TaskStatus.Success;
		//}
		//else
		// {
		//aipath.canMove = false;
		//}
	}
}