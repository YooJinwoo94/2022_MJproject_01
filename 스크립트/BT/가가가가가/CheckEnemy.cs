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
   // GameObject pastObj;


    public override void OnStart()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        charState = gameObject.GetComponent<CharState>();
    }


    public override TaskStatus OnUpdate()
	{
        if (inGameSceneUiDataManager.nowGameSceneState == InGameSceneUiDataManager.NowGameSceneState.cutScene_playerCharWalkIn ||
            inGameSceneUiDataManager.nowGameSceneState == InGameSceneUiDataManager.NowGameSceneState.cutScene_enemyCharWalkIn) return TaskStatus.Success;

        if (inGameSceneUiDataManager.nowGameSceneState == InGameSceneUiDataManager.NowGameSceneState.playerCharMoveForNextRaid)
        {
            charState.nowState = CharState.NowState.isWaitForCutScene;
            inGameSceneCharSpineAniCon.run();
            return TaskStatus.Failure;
        }


        // 만약 적이 없는 경우라면 
        switch (this.gameObject.transform.tag)
        {
            case "playerChar":
                if (inGameSceneUiDataManager.enemyObjList.Count == 0 )
                {
                    inGameSceneCharSpineAniCon.idle();
                    return TaskStatus.Failure;
                }
                break;

            case "enemyChar":
                if (inGameSceneUiDataManager.playerObjList.Count == 0)
                {
                    inGameSceneCharSpineAniCon.idle();
                    return TaskStatus.Failure;
                }
                break;
        }

        return TaskStatus.Success;
    }
}