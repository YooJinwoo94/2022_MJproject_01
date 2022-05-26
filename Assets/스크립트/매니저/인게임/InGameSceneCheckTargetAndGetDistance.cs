using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneCheckTargetAndGetDistance : MonoBehaviour
{
	// �÷��̾� -> ���� ���� = target
	// ���� -> �÷��̾� ���� = target
	
	public GameObject target;

	// distanceToTarget = **���� ������ ���� �ֵ� �������� ���� �� �� �ִ� �Ÿ�
	//                    **���� ������ ���Ÿ� �ֵ� �������� ���� �� �� �ִ� �Ÿ�
	public float distanceToTarget = 0;

	InGameSceneUiDataManager inGameSceneUiDataManager;
	CharState charState;




	// Start is called before the first frame update
	void Start()
    {
		charState = gameObject.transform.GetComponent<CharState>();
		inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
    }

    private void Update()
    {
		getTarget();
	}



    void getTarget()
	{
		switch (this.gameObject.transform.tag)
		{
			case "playerChar":
				if (inGameSceneUiDataManager.enemyObjList.Count == 0) return;
				distanceToTarget = checkDistancePlayerToEnemy();
				break;

			case "enemyChar":
				if (inGameSceneUiDataManager.playerObjList.Count == 0) return;
				distanceToTarget = checkDistanceEnemyToPlayer();
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
				distanceForOrgin = distanceForSub;
				target = inGameSceneUiDataManager.enemyObjList[i];
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
				target = inGameSceneUiDataManager.playerObjList[i];
			}
		}

		return distanceForOrgin;
	}



	// ������ �Ÿ��� �����Ͽ� �Ÿ��� ����Ѵ�
	public bool isEnemyOutOfAttackRange()
	{
		if (charState.attackRange <= distanceToTarget)
		{	
			return true;
		}

		return false;
	}


	public bool isEnemyOutOfFindRange()
    {
       if (charState.findRange <= distanceToTarget)
		{
			return true;
		}

		return false;
	}
}
