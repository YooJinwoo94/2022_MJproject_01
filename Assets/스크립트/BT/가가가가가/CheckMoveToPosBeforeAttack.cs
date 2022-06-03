using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CheckMoveToPosBeforeAttack : Conditional
{
    CharState charState;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    InGameSceneCharMove inGameSceneCharMove;
    InGameSceneGameManager inGameSceneGameManager;

    public override void OnStart()
    {
       // inGameSceneGameManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneGameManager>();
        charState = this.gameObject.transform.GetComponent<CharState>();
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
        inGameSceneCharMove = gameObject.GetComponent<InGameSceneCharMove>();
        inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();
    }



    public override TaskStatus OnUpdate()
    {
        if (charState.nowState == CharState.NowState.isWaitForBattleAndInAttackPos ||
            inGameSceneUiDataManager.isBattleStart == true)  return TaskStatus.Failure;

        switch(this.gameObject.tag)
        {
            case "playerChar":
                if (inGameSceneCharMove.IsArrivedInAttackPos("playerChar") == true)
                {
                    inGameSceneCharSpineAniCon.idle();
                    inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos += 1;
                    charState.nowState = CharState.NowState.isWaitForBattleAndInAttackPos;
                    return TaskStatus.Failure;
                }
                break;

        }
        return TaskStatus.Success;
    }
}
