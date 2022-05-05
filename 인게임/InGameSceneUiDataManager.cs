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


    //���Ͱ� ������ �����մϴ�.
    public int leftEnemyCount = 0;

    //�÷��̾� ĳ���� �� ������ �����մϴ�.
    public int leftPlayerCount = 0;

    //���� ����Ʈ �� ( �ִ� 3 )
    public int battleSceneCount = 0 ;
}
