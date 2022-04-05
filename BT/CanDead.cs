using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanDead : Conditional
{
	CharState charState;
    //


    public override void OnStart()
    {
		if (charState == null) charState = GetComponent<CharState>();


	}



    public override TaskStatus OnUpdate()
	{
		//
		// ���� ���¶��
		if (charState.hpPoint <= 0)
        {
			return TaskStatus.Success;
		}
		//
		// ���� ���°� �ƴϸ�
		else
		{
			return TaskStatus.Failure;
		}
	}
}