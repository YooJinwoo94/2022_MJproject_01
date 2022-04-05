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
			Debug.Log("다음 스테이지로 넘어가든가 아니면 전투 종료");
			return TaskStatus.Success;
		}
		else if (isEnemyOn.Value == true)
		{
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}
}