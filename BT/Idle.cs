using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Idle : Action
{
	public SharedBool isEnemyOn;


	public override TaskStatus OnUpdate()
	{
		if (isEnemyOn.Value == false)
		{
			Debug.Log("���� ���������� �Ѿ�簡 �ƴϸ� ���� ����");
			return TaskStatus.Success;
		}
		else if (isEnemyOn.Value == true)
		{
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}
}