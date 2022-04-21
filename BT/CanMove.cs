using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
//using Pathfinding;




// �켱 ���� ���϶����� ������ ����
// ������ �Ÿ��� �����Ͽ� �Ÿ��� ����Ѵ�.
// ���� �� ���� ���� ���̶�� ������ ����. ���� �� ���� ���� ���̶�� ���°��� �����.
// ���� ������ ���й��� �ִ��� Ȯ���Ѵ�.
// ���й��� ���� ���´�.
// �Ŀ� �����Ѵ�. 




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



		// ���� ���� ���� ���
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