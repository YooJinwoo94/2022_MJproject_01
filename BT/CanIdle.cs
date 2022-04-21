using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CanIdle : Conditional
{
	public bool isEnemyOn = true;
	CharState charState;

	


	public override void OnStart()
	{
		charState = this.gameObject.transform.GetComponent<CharState>();
	}

	public override TaskStatus OnUpdate()
	{
		if (isEnemyOn == false)
		{
			Debug.Log("���� ���������� �Ѿ�簡 �ƴϸ� ���� ����");

			charState.nowState = CharState.NowState.isIdle;
		}
		else if (isEnemyOn == true)
		{
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}
}
