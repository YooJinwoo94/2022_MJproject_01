using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class StageSceneThisNodeTreeData : MonoBehaviour
{
    StageSceneUiDataManager stageSceneUiDataManager;

    [System.Serializable]
    public class Level
    {
        public GameObject[] thisLevelNode;
    }
    public Level[] tree;

    public GameObject[] thisNodeObjSet;
    public GameObject thisTreeObj;

    public StageTreeNode[] stageTreeNode;

    const int nodeMaxCount = 5;
    // tsv로 받은 데이터를 저장하는곳
    // 각 층마다 스테이지 발판별_최대값을 저장함
    public string[,] nodeTypeInLevel;
    string[] nodeType;

    int rowSize;
    int columSize;








    private void Start()
    {
        stageSceneUiDataManager = GameObject.Find("Manager").GetComponent<StageSceneUiDataManager>();

        //ThisNodeObjSet 값 설정
        setDataAboutThisNodeObjSet();
        //ThisNodeObjSet 안에 있는  stageTreeNode 값 설정
        setDataAboutStageTreeNodes();

        // 데이터 받아오기 
        StartCoroutine(GetDataAboutNodeFromCSV());
    }







    void setDataAboutThisNodeObjSet()
    {
        thisNodeObjSet = new GameObject[this.gameObject.transform.childCount];

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            thisNodeObjSet[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }
    void setDataAboutStageTreeNodes()
    {
        stageTreeNode = new StageTreeNode[thisNodeObjSet.GetLength(0)];
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            stageTreeNode[i] = thisNodeObjSet[i].GetComponentInChildren<StageTreeNode>();
        }
    }




    

    IEnumerator GetDataAboutNodeFromCSV()
    {
        UnityWebRequest www = UnityWebRequest.Get(stageSceneUiDataManager.url);
        yield return www.SendWebRequest();   // 통신을 함

        string data = www.downloadHandler.text; // 데이터 받아옴
        print("받기 끝");

        checkNameAndChange(data);
        randomCountAndSetNode();
    }


    //이후 랜덤으로 설정하면 해당아이가 몇번째 아이인지만 알고 있으니
    //몇번째 아이 = 몇번째 타입을 하기위한 작업.
    void checkNameAndChange(string tsv)
    {
        string[] row = tsv.Split('\n');
        //  세로 개수
        rowSize = row.Length;
        //  가로 개수
        columSize = row[0].Split('\t').Length;

        nodeTypeInLevel = new string[columSize - 1, rowSize - 1];
        string[] column = new string[columSize - 1];
        nodeType = new string[20];


        for (int k = 1; k< columSize; k++)
        {
            for (int i = 1; i < rowSize; i++)
            {
                column = row[i].Split('\t');
                nodeTypeInLevel[k-1, i - 1] = column[ k ];
            }
        }

        for (int k = 0; k < 1; k++)
        {
            for (int i = 0; i < rowSize; i++)
            {
                column = row[i].Split('\t');
                nodeType[i] = column[k];
            }
        }
    }

    //각 시트별 값을 랜덤으로 나오게 설정.
    void randomCountAndSetNode()
    {
        // 맨 처음과 맨 마지막은 제외
        //ex) 만약 스테이지 1이면 2층 , 3층 ,4층에 대한 계산을 할 예정

        // 각 층마다 몇개의 노드가 있는가? 
        //ex) 만약 스테이지 1이면 2층은 3개 3층은 5개 있다.
        int horizontal = 0;
        int vertical = 0;
        for (int k = 1; k < (stageTreeNode.GetLength(0)-1); k++)
        {
            // 시트상 빈칸에 도착하면 pass.
            if (nodeTypeInLevel[horizontal, vertical] == "") continue;

            // 세로값 초기화
            vertical = 0;
            // 가로값 세팅하기
            horizontal = stageTreeNode[k].nodeLevelCount - 1;

            // 시트값을 점차 더하면서 해당 시트인지 확인하기 위한 변수
            int count = 0;
            // 랜덤 계산을 시작해보자
            int randomCount = UnityEngine.Random.Range(1, Int32.Parse(nodeTypeInLevel[horizontal, nodeMaxCount]) + 1);

            // random값이 더 크면  다시 돌려!
            while (count < randomCount)
            {
                count += Int32.Parse(nodeTypeInLevel[horizontal, vertical]);
                // 다음 특성인가?
                vertical++;
            }

            // 노드별 특성 설정
            stageTreeNode[k].thisNodeName = nodeType[vertical]; 
            stageTreeNode[k].bt.thisNodeName = nodeType[vertical]; 
        }
    }
}
