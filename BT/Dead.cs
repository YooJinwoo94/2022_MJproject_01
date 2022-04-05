using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Dead : Action
{
	public Repeater repeatorrepeator;

	public override void OnStart()
	{
		repeatorrepeator.repeatForever = false;
		Debug.Log("aa");
	}

//	public override TaskStatus OnUpdate()
	//{
	//	return TaskStatus.Success;
//	}
}