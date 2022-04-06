using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;


public class CheckDistance : Action
{
	
	// �÷��̾� -> ���� ���� = target
	// ���� -> �÷��̾� ���� = target
	public SharedGameObject target;

	// distanceToTarget = **���� ������ ���� �ֵ� �������� ���� �� �� �ִ� �Ÿ�
	//                    **���� ������ ���Ÿ� �ֵ� �������� ���� �� �� �ִ� �Ÿ�
	public SharedFloat distanceToTarget = 0;
	public SharedBool isEnemyOn;


	public InGameSceneUiDataManager inGameSceneUiDataManager;
	AIDestinationSetter aIDestinationSetter;


	

	public override void OnStart()
	{
		inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
		aIDestinationSetter = this.gameObject.GetComponentInParent<AIDestinationSetter>();

		getTarget();
	}

	// ���� �븱 ��븦 ã�´�.
	void getTarget()
    {
		switch (this.gameObject.transform.tag)
        {
			case "playerChar":
				if (inGameSceneUiDataManager.enemyObjList.Count == 0) return;
				distanceToTarget.Value = checkDistancePlayerToEnemy();
				break;

			case "enemyChar":
				if (inGameSceneUiDataManager.playerObjList.Count == 0) return;
				distanceToTarget.Value = checkDistanceEnemyToPlayer();
				break;
        }
    }

	// 
	float checkDistancePlayerToEnemy()
    {
		float distanceForSub = 0;
		float distanceForOrgin = 99999;

		for (int i = 0; i < inGameSceneUiDataManager.enemyObjList.Count; i++)
		{
			distanceForSub = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.enemyObjList[i].transform.position);

			//������ ������ �ִ� ������ �� �۴�.
			//�� ����� ����.
			if (distanceForOrgin > distanceForSub)
			{

				Debug.Log("aa");


				distanceForOrgin = distanceForSub;
				target.Value = inGameSceneUiDataManager.enemyObjList[i];
				aIDestinationSetter.target = inGameSceneUiDataManager.enemyObjList[i].transform;
			}
		}
		return distanceForOrgin;
	}
	// 
	float checkDistanceEnemyToPlayer()
	{
		float distanceForSub = 0;
		float distanceForOrgin = 99999;

		for (int i = 0; i < inGameSceneUiDataManager.playerObjList.Count; i++)
		{
			distanceForSub = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.playerObjList[i].transform.position);

			//������ ������ �ִ� ������ �� �۴�.
			//�� ����� ����.
			if (distanceForOrgin > distanceForSub)
			{
				distanceForOrgin = distanceForSub;
                target.Value = inGameSceneUiDataManager.playerObjList[i];
				aIDestinationSetter.target = inGameSceneUiDataManager.playerObjList[i].transform;
			}
        }

		return distanceForOrgin;
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