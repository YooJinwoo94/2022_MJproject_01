using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CanMove : Conditional
{
	InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
	InGameSceneCharMove inGameSceneCharMove;
	CharState charState;
	InGameSceneUiDataManager inGameSceneUiDataManager;
	PathManager pathManger;

	public override void OnStart()
	{
		inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
		inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
		charState = this.gameObject.transform.GetComponent<CharState>();
		inGameSceneCharMove = gameObject.transform.GetComponent<InGameSceneCharMove>();
		pathManger = this.gameObject.transform.GetComponentInParent<PathManager>();
	}


	public override TaskStatus OnUpdate()
	{
		if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.battleStart) return TaskStatus.Failure;

		if (charState.nowState == CharState.NowState.isFindingBush ) return TaskStatus.Failure;


		

		// 적이 멀리 있으면
		// 걸어가야지
		if (inGameSceneCheckTargetAndGetDistance.isEnemyOutOfAttackRange() == true)
        {
			charState.nowState = CharState.NowState.isWalkToEnemy;
		}
		else
		{
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}
}