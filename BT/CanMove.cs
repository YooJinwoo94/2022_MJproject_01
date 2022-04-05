using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;


public class CanMove : Conditional
{
	public SharedFloat distanceToTarget;

	CharState charState;
	AIPath aipath;

	public override void OnStart()
	{
		if (charState == null)
		{
			charState = this.gameObject.transform.GetComponent<CharState>();
		}

		if (aipath == null)
        {
			aipath = this.gameObject.GetComponentInParent<AIPath>();
		}

		aipath.canMove = true;
	}


	public override TaskStatus OnUpdate()
	{
		// 공격 범위 밖인 경우
		if (charState.attackRange <= distanceToTarget.Value)
        {
			aipath.canMove = true;
			return TaskStatus.Success;
		}
		else
        {
			aipath.canMove = false;
		}
		return TaskStatus.Failure;
	}
}