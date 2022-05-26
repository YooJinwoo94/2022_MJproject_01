using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class IsAttackStart : Conditional

{
	CharState charState;

	public override void OnStart()
	{
		charState = this.gameObject.transform.GetComponent<CharState>();
	}


	public override TaskStatus OnUpdate()
    {
        if (charState.nowState == CharState.NowState.isReadyForAttack) return TaskStatus.Failure;
      
        charState.nowState = CharState.NowState.isReadyForAttack;
        return TaskStatus.Failure;
    }
}
