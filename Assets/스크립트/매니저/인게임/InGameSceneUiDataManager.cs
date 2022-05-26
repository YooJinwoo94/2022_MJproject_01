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

    public GameObject playerObj;
    public GameObject[] playerCharInGridPos;

    public List<GameObject> playerObjList = new List<GameObject>();

    public string[,,] enemyCharInGrid;

    //현재 레이트 수 ( 최대 3 )
    public int battleSceneCount = 0 ;
}
