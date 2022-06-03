using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;



//함수는 일반적으로는 가까이 있는 적을 탐색 
// colliderbox를 설치하여 찾기 
// 해당 거리 계산하기 
// 해당 거리값이 일정 이내이면 공격 행동
// 아니면 해당 장소까지 이동 행동
// 만약 적을 못찾으면 idle 행동


// 추후에 들어갈 수 있음
// 공격력 높은 아이 
// 나를 때린 아이 
// 도발을 건 아이 
// 


public class CanNormalAttack : Conditional
{
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;
    InGameSceneUiDataManager inGameSceneUiDataManager;




    public override void OnStart()
    {
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = this.gameObject.transform.GetComponent<CharState>();
    }


    public override TaskStatus OnUpdate()
    {
        if (inGameSceneUiDataManager.isBattleStart == false) return TaskStatus.Failure;
        if (charState.nowState != CharState.NowState.isReadyForAttack ) return TaskStatus.Failure;
        if (inGameSceneUiDataManager.waitForRaid == true) return TaskStatus.Failure;

        if (inGameSceneCheckTargetAndGetDistance.target == null) return TaskStatus.Failure;


        // 공격 범위 이내일 경우
        if ((inGameSceneCheckTargetAndGetDistance.isEnemyOutOfAttackRange() == false)   
            && (charState.skillPoint <= 100)) return TaskStatus.Success;

        return TaskStatus.Failure;
	}
}