using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CheckAttackEnd : Conditional
{
    CharState thisCharState;
    CharState enemyCharState;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    public override void OnStart()
    {
        thisCharState = gameObject.GetComponent<CharState>();
        inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();
        inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();

        if (inGameSceneCheckTargetAndGetDistance.target == null) return;

        enemyCharState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();
    }
    public override TaskStatus OnUpdate()
    {
        if (enemyCharState.nowState == CharState.NowState.isDead
             && inGameSceneCheckTargetAndGetDistance.isEnemyOutOfAttackRange() == true)
        {
             inGameSceneCharSpineAniCon.idle();
            //inGameSceneCharSpineAniCon.endAttack();

            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
