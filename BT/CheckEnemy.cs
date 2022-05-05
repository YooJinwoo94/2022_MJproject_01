using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;


public class CheckEnemy : Conditional
{
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    CharState charState;
    GameObject pastObj;


    public override void OnStart()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        charState = gameObject.GetComponent<CharState>();
    }


    public override TaskStatus OnUpdate()
	{
        // 만약 적이 없는 경우라면 
        switch (this.gameObject.transform.tag)
        {
            case "playerChar":
                if (inGameSceneUiDataManager.enemyObjList.Count == 0)
                {
                    charState.nowState = CharState.NowState.isIdle;
                    inGameSceneCharSpineAniCon.idle();
                    return TaskStatus.Failure;
                }
                break;

            case "enemyChar":
                if (inGameSceneUiDataManager.playerObjList.Count == 0)
                {
                    charState.nowState = CharState.NowState.isIdle;
                    inGameSceneCharSpineAniCon.idle();
                    return TaskStatus.Failure;
                }
                break;
        }

        if (pastObj != inGameSceneCheckTargetAndGetDistance.target)
        {
            charState.nowState = CharState.NowState.isIdle;
        }
        pastObj = inGameSceneCheckTargetAndGetDistance.target;

        return TaskStatus.Success;
    }
}