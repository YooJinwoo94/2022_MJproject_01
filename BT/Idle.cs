using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Idle : Action
{
	public override void OnStart()
	{

    }




	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}