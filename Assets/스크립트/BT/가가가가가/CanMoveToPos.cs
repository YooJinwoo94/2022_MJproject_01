using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CanMoveToPos : Conditional
{
	InGameSceneIfRaidEnd inGameSceneIfRaidEnd;
	InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
	CharState charState;

	


	public override void OnStart()
	{
		inGameSceneCharSpineAniCon = this.gameObject.transform.GetComponent<InGameSceneCharSpineAniCon>();
		inGameSceneIfRaidEnd = GameObject.Find("Manager").GetComponent<InGameSceneIfRaidEnd>();
		charState = this.gameObject.transform.GetComponent<CharState>();
	}

	public override TaskStatus OnUpdate()
	{
		if(inGameSceneIfRaidEnd.waitForRaid == true)
        {
			charState.nowState = CharState.NowState.isWalkToOrginPos;
			inGameSceneCharSpineAniCon.run();
		}
		else
        {
			charState.nowState = CharState.NowState.isIdle;
			inGameSceneCharSpineAniCon.idle();
			return TaskStatus.Failure;
		}

		return TaskStatus.Success;
	}
}
