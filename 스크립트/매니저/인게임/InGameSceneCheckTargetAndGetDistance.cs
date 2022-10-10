using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

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
	[HideInInspector]
	public bool ifSkillUsedOnceChangeTarget;


	float distanceForSub = 0;
	float distanceForOrgin = 9999;





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











	float checkDistancePlayerToEnemy()
	{
		checkTargetPlayerToEnemy();

		return distanceForOrgin;
	}

	float checkDistanceEnemyToPlayer()
	{
		checkTargetEnemyToPlayer();

		return distanceForOrgin;
	}













    public void checkTargetPlayerToEnemy()
    {
		if (charState.skillPoint >= 100 &&
	     target != null &&
	     ifSkillUsedOnceChangeTarget == true) return;


		// �켱 ���� ����� ���̸� ã�´�.
		int checkNumForFirst = 0;
		int numForFirst = 0;
		distanceForOrgin = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.enemyObjList[0].transform.position);
		foreach (GameObject obj in inGameSceneUiDataManager.enemyObjList)
		{
			distanceForSub = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.enemyObjList[numForFirst].transform.position);

			if (distanceForOrgin < distanceForSub) continue;

			distanceForOrgin = distanceForSub;
			checkNumForFirst = numForFirst;

			numForFirst++;
		}


		// ������ ��Ÿ��� ���� ����� �Ѵ�.
		int checkNUm = checkNumForFirst;
		int num = 0;
		switch (charState.atkPriority)
        {
			case "close":
				foreach (GameObject obj in inGameSceneUiDataManager.enemyObjList)
				{
					distanceForSub = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.enemyObjList[num].transform.position);

					if (charState.attackRange < distanceForSub) continue;
					if (distanceForOrgin < distanceForSub) continue;

					distanceForOrgin = distanceForSub;
					checkNUm = num;

					num++;
				}
				break;

			case "long":
				foreach (GameObject obj in inGameSceneUiDataManager.enemyObjList)
				{
					distanceForSub = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.enemyObjList[num].transform.position);

					if (charState.attackRange < distanceForSub) continue;
					if (distanceForOrgin > distanceForSub) continue;

					distanceForOrgin = distanceForSub;
					checkNUm = num;

					num++;
				}
				break;
        }

		target = inGameSceneUiDataManager.enemyObjList[checkNUm];
	}














	public void checkTargetEnemyToPlayer()
	{
		if (charState.skillPoint >= 100 &&
			target != null &&
			ifSkillUsedOnceChangeTarget == true) return;


		// �켱 ���� ����� ���̸� ã�´�.
		int checkNumForFirst = 0;
		int numForFirst = 0;
		distanceForOrgin = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.playerObjList[0].transform.position);
		foreach (GameObject obj in inGameSceneUiDataManager.playerObjList)
		{
			distanceForSub = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.playerObjList[numForFirst].transform.position);

			if (distanceForOrgin < distanceForSub) continue;

			distanceForOrgin = distanceForSub;
			checkNumForFirst = numForFirst;

			numForFirst++;
		}

		// ������ ��Ÿ��� ���� ����� �Ѵ�.
		int checkNUm = checkNumForFirst;
		int num = 0;
		switch (charState.atkPriority)
		{
			case "close":
				foreach (GameObject obj in inGameSceneUiDataManager.playerObjList)
				{
					distanceForSub = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.playerObjList[num].transform.position);

					if (charState.attackRange < distanceForSub) continue;
					if (distanceForOrgin < distanceForSub) continue;

					distanceForOrgin = distanceForSub;
					checkNUm = num;

					num++;
				}
				break;

			case "long":
				foreach (GameObject obj in inGameSceneUiDataManager.playerObjList)
				{
					distanceForSub = Vector2.Distance(this.transform.position, inGameSceneUiDataManager.playerObjList[num].transform.position);

					if (charState.attackRange < distanceForSub) continue;
					if (distanceForOrgin > distanceForSub) continue;

					distanceForOrgin = distanceForSub;
					checkNUm = num;

					num++;
				}
				break;
		}
		target = inGameSceneUiDataManager.playerObjList[checkNUm];
	}
















	// ������ �Ÿ��� �����Ͽ� �Ÿ��� ����Ѵ�
	public bool isEnemyOutOfAttackRange()
	{
		if (charState.attackRange < distanceToTarget)
		{	
			return true;
		}

		return false;
	}
}
