using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SpecialAttack : Action
{
	InGameSceneNormalAttack inGameSceneNormalAttack;
	InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
	CharState charState;

	public override void OnStart()
	{
		inGameSceneNormalAttack = gameObject.GetComponent<InGameSceneNormalAttack>();
		inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();
		charState = gameObject.GetComponent<CharState>();

		inGameSceneCharSpineAniCon.attack();
		inGameSceneNormalAttack.attackEnemy();

		//charState.nowState = CharState.NowState.isIdle;
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}