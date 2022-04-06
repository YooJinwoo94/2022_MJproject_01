using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanNormalAttack : Conditional
{
    public SharedFloat distanceToTarget;
    CharState charState;

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



    public override void OnStart()
    {
        charState = this.gameObject.transform.GetComponent<CharState>();
    }


    public override TaskStatus OnUpdate()
	{
        // ���� ���� �̳��� ���
        if ((charState.attackRange >= distanceToTarget.Value)   
            && (charState.skillPoint <= 100))
        {
            Debug.Log("���� ���� �ȿ� �ִٴ�! + �Ϲ� ���� �Ѵ�!");





           return TaskStatus.Success;
        }
		return TaskStatus.Failure;
	}
}