using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
//using Pathfinding;


public class CheckDistance : Action
{
	public InGameSceneUiDataManager inGameSceneUiDataManager;



	public override void OnStart()
	{
		inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
	}


	public override TaskStatus OnUpdate()
	{
		switch (this.gameObject.transform.tag)
		{
			case "playerChar":
				if (inGameSceneUiDataManager.enemyObjList.Count == 0) return TaskStatus.Failure;
				break;

			case "enemyChar":
				if (inGameSceneUiDataManager.playerObjList.Count == 0) return TaskStatus.Failure;
				break;
		}

		return TaskStatus.Success;
	}
}