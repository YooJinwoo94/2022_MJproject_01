using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanSpecialAttack : Conditional
{
	CharState charState;

    public SharedFloat distanceToTarget;


    public override void OnStart()
    {
        if (charState == null )
        {
            charState = gameObject.GetComponent<CharState>();
        }

    }

    public override TaskStatus OnUpdate()
	{
        if ((charState.attackRange >= distanceToTarget.Value) &&
            (charState.skillPoint >= 100) )
        {
            return TaskStatus.Success;
        }
		return TaskStatus.Failure;
	}
}