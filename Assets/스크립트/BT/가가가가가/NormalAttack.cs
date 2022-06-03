using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class NormalAttack : Action
{
	InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
	InGameSceneNormalAttack inGameSceneNormalAttack;
	InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;

	CharState charState;


	public override void OnStart()
	{
		inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
		inGameSceneNormalAttack = gameObject.GetComponent<InGameSceneNormalAttack>();
		inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();

		inGameSceneCharSpineAniCon.attack();
		inGameSceneNormalAttack.attackEnemy();

	    switch(gameObject.tag)
        {
			case "playerChar":
				charState = gameObject.GetComponent<CharState>();
				break;

			case "enemyChar":
				charState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();
				break;
		}		
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}