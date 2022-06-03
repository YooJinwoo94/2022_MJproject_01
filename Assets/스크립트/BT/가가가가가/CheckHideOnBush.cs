using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
//using Pathfinding;




public class CheckHideOnBush : Conditional
{
    InGameSceneCheckBush inGameSceneCheckBush;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;
    InGameSceneUiDataManager inGameSceneUiDataManager;

    public override void OnStart()
    {
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
        inGameSceneCheckBush = gameObject.transform.GetComponent<InGameSceneCheckBush>();
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = this.gameObject.transform.GetComponent<CharState>();
    }

    public override TaskStatus OnUpdate()
    {
        if (inGameSceneUiDataManager.waitForRaid == true) return TaskStatus.Failure;
        if (inGameSceneUiDataManager.isBattleStart == false) return TaskStatus.Failure;
       
        if (charState.nowState == CharState.NowState.isReadyForAttack ) return TaskStatus.Failure;
        if (charState.nowState == CharState.NowState.isFindingBush) return TaskStatus.Success;

        if (inGameSceneCheckBush.isBushNear() == true)
        {
            charState.nowState = CharState.NowState.isFindingBush;
            return TaskStatus.Success;
        }
        
        charState.nowState = CharState.NowState.isReadyForAttack;
        return TaskStatus.Failure;
    }
}
