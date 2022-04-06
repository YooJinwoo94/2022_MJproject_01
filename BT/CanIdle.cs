using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CanIdle : Conditional
{
	public SharedBool isEnemyOn;


	public override TaskStatus OnUpdate()
	{
		if (isEnemyOn.Value == false)
		{
			Debug.Log("���� ���������� �Ѿ�簡 �ƴϸ� ���� ����");
			return TaskStatus.Failure;
		}
		else if (isEnemyOn.Value == true)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Success;
	}
}
