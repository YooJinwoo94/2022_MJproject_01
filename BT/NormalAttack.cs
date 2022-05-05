using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class NormalAttack : Action
{
	InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
	InGameSceneNormalAttack inGameSceneNormalAttack;
	InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
	CharState enemyState;
	CharState playerState;


	public override void OnStart()
	{
		inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
		inGameSceneNormalAttack = gameObject.GetComponent<InGameSceneNormalAttack>();
		inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();

		inGameSceneCharSpineAniCon.attack();
		inGameSceneNormalAttack.attackEnemy();


		playerState = gameObject.GetComponent<CharState>();
		enemyState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();

	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}