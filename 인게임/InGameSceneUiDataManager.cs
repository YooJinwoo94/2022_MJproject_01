        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using BehaviorDesigner.Runtime;

public class InGameSceneUiDataManager : MonoBehaviour
{
    public SkeletonAnimation[] enemySkeletonAnimationSet;
    public ExternalBehaviorTree[] enemyBehaviorTreeDataSet;
    public SkeletonAnimation[] playerSkeletonAnimationSet;
    public ExternalBehaviorTree[] playerBehaviorTreeDataSet;

    public GameObject enemyObj;
    public GameObject[] enemyCharInGridPos;
    public List<GameObject> enemyObjList = new List<GameObject>();
   // public int enemyObjCount = 0;

    public GameObject playerObj;
    public GameObject[] playerCharInGridPos;
   // public int playerObjCount = 0;

    public List<GameObject> playerObjList = new List<GameObject>();

    public string[,,] enemyCharInGrid;


    //몬스터가 죽으면 감소합니다.
    public int leftEnemyCount = 0;

    //플레이어 캐릭터 가 죽으면 감소합니다.
    public int leftPlayerCount = 0;

    //현재 레이트 수 ( 최대 3 )
    public int battleSceneCount = 0 ;
}
