using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanDead : Conditional
{
	CharState charState;





    public override void OnStart()
    {
		charState = gameObject.GetComponent<CharState>();
	}



    public override TaskStatus OnUpdate()
	{
		//
		// 죽은 상태라면
		if (charState.hp <= 0)
        {
			return TaskStatus.Success;
		}
		//
		// 죽은 상태가 아니면
		else
		{
			return TaskStatus.Failure;
		}
	}
}