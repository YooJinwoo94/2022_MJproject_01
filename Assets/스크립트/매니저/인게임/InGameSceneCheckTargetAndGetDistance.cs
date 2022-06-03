using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

			//기존에 가지고 있는 값보다 더 작다.
			//더 가까운 존재.
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

			//기존에 가지고 있는 값보다 더 작다.
			//더 가까운 존재.
			if (distanceForOrgin > distanceForSub)
			{
				distanceForOrgin = distanceForSub;
				target = inGameSceneUiDataManager.playerObjList[i];
			}
		}

		return distanceForOrgin;
	}



	// 적과의 거리를 측정하여 거리를 계산한다
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
