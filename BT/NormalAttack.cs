using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class NormalAttack : Action
{
	InGameSceneNormalAttack inGameSceneNormalAttack;
	InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;


	public override void OnStart()
	{
		inGameSceneNormalAttack = gameObject.GetComponent<InGameSceneNormalAttack>();
		inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();


		inGameSceneCharSpineAniCon.attack();
		inGameSceneNormalAttack.attackEnemy();
 	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}