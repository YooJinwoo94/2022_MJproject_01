using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class InGameSceneCheckTargetAndGetDistance : MonoBehaviour
{
	// 플레이어 -> 몬스터 유닛 = target
	// 몬스터 -> 플레이어 유닛 = target

	public GameObject target;

	// distanceToTarget = **보다 적으면 근접 애들 기준으로 공격 할 수 있는 거리
	//                    **보다 적으면 원거리 애들 기준으로 공격 할 수 있는 거리
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


		// 우선 가장 가까운 아이를 찾는다.
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


		// 본인의 사거리에 따라 계산을 한다.
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


		// 우선 가장 가까운 아이를 찾는다.
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

		// 본인의 사거리에 따라 계산을 한다.
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
















	// 적과의 거리를 측정하여 거리를 계산한다
	public bool isEnemyOutOfAttackRange()
	{
		if (charState.attackRange < distanceToTarget)
		{	
			return true;
		}

		return false;
	}
}
