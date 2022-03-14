using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceBeforeBattleSceneSceneManager : MonoBehaviour
{
    [SerializeField]
    PlayerChoiceBeforeBattleSceneUIMoveManager playerChoiceBeforeBattleSceneUIMoveManager;

    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;




    public void playerChoiceInvenUiCharArea(int dragNum,string name)
    {
        if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceCount >= 1)
        {
            checkIsOverLap(dragNum, name);
            return;
        }
        int[] gridXY = changeNumToGrid(dragNum);
        inFromGrid(gridXY, name);
    }




    void turnOnPlayerChar(int num , string name)
    {
        GameObject playerCharInGrid = Instantiate(playerChoiceBeforeBattleSceneUiDataManager.playerCharInGrid, playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform);
        playerCharInGrid.name = name;
    }
    void turnOffPlayerChar(int num)
    {
       Destroy(playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform.GetChild(0).gameObject);
    }






    // ĳ���� �׸��� ���ý� ȣ��Ǵ� �Լ�
    public void playerChoiceGrid()
    {
        // �ִ� ���� ������ ĳ������ ���� 5���Դϴ�.
        if (checkIsFull() == false) return;

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOn();
    }


    bool checkIsFull()
    { 
        if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceCount >= 5) return false;
        return true;
    }
    bool checkIsEmpty(int num)
    {
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            //playerCharChoiceData.GetLength(0) == ���߹迭�� �� ( ������ ) 
            if (num - playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0) < 0)
            {
                switch(playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[count, (num)])
                {
                    case null:
                        return true;

                    default:
                        return false;
                }
            }
            else 
            {
                count++;
                num -= playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0);
            }
        }

        return false;
    }


    int [] findIntInGrid(string name)
    {
        int[] gridXY = new int[2];
        for (int i = 0; i < playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0); i++)
        {
            for (int k = 0; k < playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(1); k++)
            {
                if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[i, k] == name)
                {
                    gridXY[0] = i;
                    gridXY[1] = k;
                    return gridXY;
                }
            }
        }
        return gridXY;
    }
    bool findBoolInGrid(string name)
    {
        for (int i = 0; i< playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0); i++)
        {
            for (int k = 0; k < playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(1); k++)
            {
                if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[i, k] == name)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int [] changeNumToGrid(int num)
    {
        int[] gridXY = new int[2];

        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            //playerCharChoiceData.GetLength(0) == ���߹迭�� �� ( ������ ) 
            if (num - playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0) < 0)
            {
                break;
            }
            else
            {
                count++;
                num -= playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0);
            }
        }

        gridXY[0] = num;
        gridXY[1] = count;

        return gridXY;
    }



    //- ����
    public void outFromGrid(int[] gridXY)
    {
        if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXY[0], gridXY[1]] == null)
        {
            playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
            return;
        }

        turnOffPlayerChar((gridXY[1] * playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0)) + gridXY[0]);
        playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXY[0], gridXY[1]] = null;

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }
    //+ �ֱ�
    void inFromGrid(int[] gridXY,string name)
    {
     //   Debug.Log(gridXY[0] + " " + gridXY[1] + " �� " + name + "�� �־����ϴ�. ");

        playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXY[0], gridXY[1]] = name;
        turnOnPlayerChar((gridXY[1] * playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0)) + gridXY[0], name);

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }


    // ���� �׸����� ���� �����
     void countGridState()
    {
        int count = 0;

        for(int i=0; i < playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0); i++)
        {
            for (int k = 0; k < playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(1); k++)
            {
                if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[i,k] != null)
                {
                    count++;
                }
            }
        }

        playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceCount = count;
    }



    //�ߺ��̴�?
    void checkIsOverLap(int numOfClickedGrid, string clickedCharName)
    {
        //�׸��� ���� ���� �ٲ��ֱ�
        int[] gridXYOfClicked = changeNumToGrid(numOfClickedGrid);

        switch (findBoolInGrid(clickedCharName))
        {
            // �׸��忡 �ߺ��� �ִ� ��� 
            case true:
                // ���� ��ġ���� ������� �̵��ϴ� ���
                if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
                {
                    int[] gridXYOfCharPast = findIntInGrid(clickedCharName);

                    outFromGrid(gridXYOfCharPast);
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                // �ٲ��ֱ�
                else
                {
                    // gridXYOfClicked == ���� ��ġ
                    // clickedCharName == ���� ������ �̸� 
                    // playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] == �̹� �ִ� ������ �̸�
                    // playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]]) == ���� ������ ��ġ

                    int[] gridXYplayerWillPut = findIntInGrid(clickedCharName);

                    string wasString = playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
                    string willString = playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYOfClicked[0], gridXYOfClicked[1]];

                    outFromGrid(gridXYplayerWillPut);
                    outFromGrid(gridXYOfClicked);
                    inFromGrid(gridXYOfClicked, wasString);
                    inFromGrid(gridXYplayerWillPut, willString);
                }
                return;

            // �׸��忡 �ߺ��� ���� ��� 
            case false:
                //�׳� �ֱ�
                if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
                {
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                //������ �ֱ�
                else
                {
                    outFromGrid(gridXYOfClicked);
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                return;
        }
    }




    public void ifDragAndDrop(int dragStartNum,int dropStartNum , string dropCharName)
    {
        //�׸��� ���� ���� �ٲ��ֱ�

        int[] gridXYplayerWasPut = changeNumToGrid(dragStartNum);
        int[] gridXYplayerWillPut = changeNumToGrid(dropStartNum);

        if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] == null)
        {
            //������ �ִ� �ڸ��� �����ֱ�
            playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = null;
            // ���� ��ġ�� �־��ֱ� 
            playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dropCharName;
            countGridState();
        }
        else
        {
            //�������̶� ������ �ٲ��ֱ�              
            string wasString = playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
            string willString = playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]];

            outFromGrid(gridXYplayerWillPut);
            inFromGrid(gridXYplayerWasPut, wasString);

            // ���� ��ġ�� �־��ֱ� 
            playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = wasString;
            // ���� ��ġ�� �־��ֱ� 
            playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = willString;
            countGridState();
        }
    }
}
