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
    // tsv�� ���� �����͸� �����ϴ°�
    // �� ������ �������� ���Ǻ�_�ִ밪�� ������
    public string[,] nodeTypeInLevel;
    string[] nodeType;

    int rowSize;
    int columSize;








    private void Start()
    {
        stageSceneUiDataManager = GameObject.Find("Manager").GetComponent<StageSceneUiDataManager>();

        //ThisNodeObjSet �� ����
        setDataAboutThisNodeObjSet();
        //ThisNodeObjSet �ȿ� �ִ�  stageTreeNode �� ����
        setDataAboutStageTreeNodes();

        // ������ �޾ƿ��� 
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
        yield return www.SendWebRequest();   // ����� ��

        string data = www.downloadHandler.text; // ������ �޾ƿ�
        print("�ޱ� ��");

        checkNameAndChange(data);
        randomCountAndSetNode();
    }


    //���� �������� �����ϸ� �ش���̰� ���° ���������� �˰� ������
    //���° ���� = ���° Ÿ���� �ϱ����� �۾�.
    void checkNameAndChange(string tsv)
    {
        string[] row = tsv.Split('\n');
        //  ���� ����
        rowSize = row.Length;
        //  ���� ����
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

    //�� ��Ʈ�� ���� �������� ������ ����.
    void randomCountAndSetNode()
    {
        // �� ó���� �� �������� ����
        //ex) ���� �������� 1�̸� 2�� , 3�� ,4���� ���� ����� �� ����

        // �� ������ ��� ��尡 �ִ°�? 
        //ex) ���� �������� 1�̸� 2���� 3�� 3���� 5�� �ִ�.
        int horizontal = 0;
        int vertical = 0;
        for (int k = 1; k < (stageTreeNode.GetLength(0)-1); k++)
        {
            // ��Ʈ�� ��ĭ�� �����ϸ� pass.
            if (nodeTypeInLevel[horizontal, vertical] == "") continue;

            // ���ΰ� �ʱ�ȭ
            vertical = 0;
            // ���ΰ� �����ϱ�
            horizontal = stageTreeNode[k].nodeLevelCount - 1;

            // ��Ʈ���� ���� ���ϸ鼭 �ش� ��Ʈ���� Ȯ���ϱ� ���� ����
            int count = 0;
            // ���� ����� �����غ���
            int randomCount = UnityEngine.Random.Range(1, Int32.Parse(nodeTypeInLevel[horizontal, nodeMaxCount]) + 1);

            // random���� �� ũ��  �ٽ� ����!
            while (count < randomCount)
            {
                count += Int32.Parse(nodeTypeInLevel[horizontal, vertical]);
                // ���� Ư���ΰ�?
                vertical++;
            }

            // ��庰 Ư�� ����
            stageTreeNode[k].thisNodeName = nodeType[vertical]; 
            stageTreeNode[k].bt.thisNodeName = nodeType[vertical]; 
        }
    }
}
