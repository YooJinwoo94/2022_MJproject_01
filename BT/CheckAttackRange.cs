using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
//using Pathfinding;




// ���� �� ���� ���� ���̶�� ������ ����. ���� �� ���� ���� ���̶�� ���°��� �����.
public class CheckAttackRange : Conditional
{
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;

    CharState charState;


    public override void OnStart()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = this.gameObject.transform.GetComponent<CharState>();
    }

    public override TaskStatus OnUpdate()
    {
        if (charState.nowState == CharState.NowState.isAttack ||
            charState.nowState == CharState.NowState.isReadyForAttack) return TaskStatus.Failure;

        if(charState.nowState == CharState.NowState.isFindingBush) return TaskStatus.Success;


        if (inGameSceneCheckTargetAndGetDistance.isEnemyOutOfAttackRange() == true)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
