using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanNormalAttack : Conditional
{
    public SharedFloat distanceToTarget;

    CharState charState;

    //적을 찾는 함수 
    // 만약 true값을 반환하면 
    // 공격 시작! 

    //함수는 일반적으로는 가까이 있는 적을 탐색 
    // colliderbox를 설치하여 찾기 
    // 해당 거리 계산하기 
    // 해당 거리값이 일정 이내이면 공격 행동
    // 아니면 해당 장소까지 이동 행동
    // 만약 적을 못찾으면 idle 행동

    //공격력 높은 아이 
    // 나를 때린 아이 
    // 도발을 건 아이 
    // 



    public override void OnStart()
    {
        if(charState == null)
        {
            charState = this.gameObject.transform.GetComponent<CharState>();
        }
    }


    public override TaskStatus OnUpdate()
	{
        // 공격 범위 이내일 경우
        if ((charState.attackRange >= distanceToTarget.Value)   
            && (charState.skillPoint <= 100))
        {
           // Debug.Log("공격 범위 안에 있다다! + 일반 공격 한다!");
           return TaskStatus.Success;
        }
		return TaskStatus.Failure;
	}
}