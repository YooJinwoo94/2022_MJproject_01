using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
//using Pathfinding;




public class CheckHideOnBush : Conditional
{
    InGameSceneCheckBush inGameSceneCheckBush;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;


    public override void OnStart()
    {
        inGameSceneCheckBush = gameObject.transform.GetComponent<InGameSceneCheckBush>();
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = this.gameObject.transform.GetComponent<CharState>();
    }

    public override TaskStatus OnUpdate()
    {
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
