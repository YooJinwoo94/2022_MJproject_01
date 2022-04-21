using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;



//�Լ��� �Ϲ������δ� ������ �ִ� ���� Ž�� 
// colliderbox�� ��ġ�Ͽ� ã�� 
// �ش� �Ÿ� ����ϱ� 
// �ش� �Ÿ����� ���� �̳��̸� ���� �ൿ
// �ƴϸ� �ش� ��ұ��� �̵� �ൿ
// ���� ���� ��ã���� idle �ൿ


// ���Ŀ� �� �� ����
// ���ݷ� ���� ���� 
// ���� ���� ���� 
// ������ �� ���� 
// 


public class CanNormalAttack : Conditional
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
        if (charState.nowState != CharState.NowState.isReadyForAttack ) return TaskStatus.Failure;


        // ���� ���� �̳��� ���
        if ((charState.attackRange >= inGameSceneCheckTargetAndGetDistance.distanceToTarget)   
            && (charState.skillPoint <= 100))
        {
           // Debug.Log("���� ���� �ȿ� �ִٴ�! + �Ϲ� ���� �Ѵ�!");
           return TaskStatus.Success;
        }
		return TaskStatus.Failure;
	}
}