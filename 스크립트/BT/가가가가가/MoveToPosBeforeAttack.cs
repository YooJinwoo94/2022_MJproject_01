using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class MoveToPosBeforeAttack : Action
{
    CharState charState;

    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    InGameSceneCharMove inGameSceneCharMove;
    InGameSceneGameManager  inGameSceneGameManager;


    public override void OnStart()
    {
        inGameSceneGameManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneGameManager>();
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
        inGameSceneCharMove = gameObject.GetComponent<InGameSceneCharMove>();
        inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();

        charState = this.gameObject.transform.GetComponent<CharState>();
    }


    public override TaskStatus OnUpdate()
    {
        inGameSceneCharSpineAniCon.run();
        inGameSceneCharMove.moveToAttackPos(this.gameObject.tag);
        return TaskStatus.Success;
    }
}
