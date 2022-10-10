using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CanStun : Conditional
{
	CharState charState;
	InGameSceneUiDataManager inGameSceneUiDataManager;






	public override void OnStart()
	{
		inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
		charState = this.gameObject.transform.GetComponent<CharState>();
	}

	public override TaskStatus OnUpdate()
	{
		if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.battleStart) return TaskStatus.Failure;

		if (charState.checkCharCondition(CharState.CharCondition.isUnbeatable) == true) return TaskStatus.Failure;

		if (charState.checkCharCondition(CharState.CharCondition.isStuned) == true) return TaskStatus.Success;

		return TaskStatus.Failure;
	}
}
