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






    // 캐릭터 그리드 선택시 호출되는 함수
    public void playerChoiceGrid()
    {
        // 최대 선택 가능한 캐릭터의 수는 5명입니다.
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
            //playerCharChoiceData.GetLength(0) == 이중배열의 행 ( 가로줄 ) 
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
            //playerCharChoiceData.GetLength(0) == 이중배열의 행 ( 가로줄 ) 
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



    //- 빼기
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
    //+ 넣기
    void inFromGrid(int[] gridXY,string name)
    {
     //   Debug.Log(gridXY[0] + " " + gridXY[1] + " 에 " + name + "를 넣었습니다. ");

        playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXY[0], gridXY[1]] = name;
        turnOnPlayerChar((gridXY[1] * playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData.GetLength(0)) + gridXY[0], name);

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }


    // 현재 그리드의 숫를 계산함
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



    //중복이니?
    void checkIsOverLap(int numOfClickedGrid, string clickedCharName)
    {
        //그리드 상의 숫자 바꿔주기
        int[] gridXYOfClicked = changeNumToGrid(numOfClickedGrid);

        switch (findBoolInGrid(clickedCharName))
        {
            // 그리드에 중복이 있는 경우 
            case true:
                // 현재 위치에서 빈공간에 이동하는 경우
                if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
                {
                    int[] gridXYOfCharPast = findIntInGrid(clickedCharName);

                    outFromGrid(gridXYOfCharPast);
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                // 바꿔주기
                else
                {
                    // gridXYOfClicked == 누른 위치
                    // clickedCharName == 누른 아이의 이름 
                    // playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] == 이미 있는 아이의 이름
                    // playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]]) == 누른 아이의 위치

                    int[] gridXYplayerWillPut = findIntInGrid(clickedCharName);

                    string wasString = playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
                    string willString = playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYOfClicked[0], gridXYOfClicked[1]];

                    outFromGrid(gridXYplayerWillPut);
                    outFromGrid(gridXYOfClicked);
                    inFromGrid(gridXYOfClicked, wasString);
                    inFromGrid(gridXYplayerWillPut, willString);
                }
                return;

            // 그리드에 중복이 없는 경우 
            case false:
                //그냥 넣기
                if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
                {
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                //삭제후 넣기
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
        //그리드 상의 숫자 바꿔주기

        int[] gridXYplayerWasPut = changeNumToGrid(dragStartNum);
        int[] gridXYplayerWillPut = changeNumToGrid(dropStartNum);

        if (playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] == null)
        {
            //기존에 있던 자리값 없애주기
            playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = null;
            // 현재 위치값 넣어주기 
            playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dropCharName;
            countGridState();
        }
        else
        {
            //누른곳이랑 눌린곳 바꿔주기              
            string wasString = playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
            string willString = playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]];

            outFromGrid(gridXYplayerWillPut);
            inFromGrid(gridXYplayerWasPut, wasString);

            // 현재 위치값 넣어주기 
            playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = wasString;
            // 현재 위치값 넣어주기 
            playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = willString;
            countGridState();
        }
    }
}
