using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;


public class CheckEnemy : Conditional
{
    InGameSceneIfRaidEnd inGameSceneIfRaidEnd;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    CharState charState;
    GameObject pastObj;


    public override void OnStart()
    {
        inGameSceneIfRaidEnd = GameObject.Find("Manager").GetComponent<InGameSceneIfRaidEnd>();
        inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        charState = gameObject.GetComponent<CharState>();
    }


    public override TaskStatus OnUpdate()
	{
        if (inGameSceneIfRaidEnd.waitForRaid == true)
        {
            charState.nowState = CharState.NowState.isWalkToOrginPos;
            inGameSceneCharSpineAniCon.run();
            return TaskStatus.Failure;
        }

        // 만약 적이 없는 경우라면 
        switch (this.gameObject.transform.tag)
        {
            case "playerChar":
                if (inGameSceneUiDataManager.enemyObjList.Count == 0 )
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