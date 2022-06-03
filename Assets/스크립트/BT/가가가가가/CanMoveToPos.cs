using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CanMoveToPos : Conditional
{
	InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
	CharState charState;
	InGameSceneUiDataManager inGameSceneUiDataManager;



	public override void OnStart()
	{
		inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
		inGameSceneCharSpineAniCon = this.gameObject.transform.GetComponent<InGameSceneCharSpineAniCon>();
		charState = this.gameObject.transform.GetComponent<CharState>();
	}

	public override TaskStatus OnUpdate()
	{
		if(inGameSceneUiDataManager.waitForRaid == true &&
				inGameSceneUiDataManager.isBattleStart == true)
        {
			charState.nowState = CharState.NowState.isWalkToOrginPos;
			inGameSceneCharSpineAniCon.run();
		}
		else
        {
		    ////charState.nowState = CharState.NowState.isIdle;
			inGameSceneCharSpineAniCon.idle();
			return TaskStatus.Failure;
		}

		return TaskStatus.Success;
	}
}
