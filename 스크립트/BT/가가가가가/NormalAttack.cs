using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class NormalAttack : Action
{
	InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
	InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;




	public override void OnStart()
	{
		inGameSceneCheckTargetAndGetDistance = gameObject.GetComponentInChildren<InGameSceneCheckTargetAndGetDistance>();
		inGameSceneCharSpineAniCon = gameObject.GetComponentInChildren<InGameSceneCharSpineAniCon>();

		inGameSceneCharSpineAniCon.attack();
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}