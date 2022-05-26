using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;


public class Dead : Action
{
	public Repeater repeatorrepeator;
	InGameSceneDead inGameSceneDead;
	CharState charState;


	public override void OnStart()
	{
		charState = gameObject.GetComponent<CharState>();
		inGameSceneDead = gameObject.GetComponent<InGameSceneDead>();

		charState.nowState = CharState.NowState.isDead;
		inGameSceneDead.dead();
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}