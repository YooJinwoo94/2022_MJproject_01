using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanSpecialAttack : Conditional
{
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;
    InGameSceneUiDataManager inGameSceneUiDataManager;

    public override void OnStart()
    {
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = gameObject.GetComponent<CharState>();
    }

    public override TaskStatus OnUpdate()
    {
        if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.battleStart) return TaskStatus.Failure;

        if ( charState.nowState != CharState.NowState.isReadyForAttack) return TaskStatus.Failure; 
        if (inGameSceneCheckTargetAndGetDistance.target == null) return TaskStatus.Failure;


        if ((inGameSceneCheckTargetAndGetDistance.isEnemyOutOfAttackRange() == false) &&
            (charState.skillPoint >= 100)) return TaskStatus.Success;

        return TaskStatus.Failure;
	}
}