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
		if (inGameSceneUiDataManager.isBattleStart == false) return TaskStatus.Failure;

		if (charState.nowState == CharState.NowState.isFindingBush ) return TaskStatus.Failure;


		

		// ���� �ָ� ������
		// �ɾ����
		if (inGameSceneCheckTargetAndGetDistance.isEnemyOutOfFindRange() == true)
        {

		   //	switch (pathManger.FinalNodeList.Count)
           // {
			//	case 0:
			//		inGameSceneCharMove.moveToIntPos();
			//		break;
            //}
			charState.nowState = CharState.NowState.isWalkToEnemy;
		}
		else
		{
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}
}