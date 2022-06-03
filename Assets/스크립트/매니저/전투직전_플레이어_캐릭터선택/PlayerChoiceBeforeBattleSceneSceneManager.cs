using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class PlayerChoiceBeforeBattleSceneSceneManager : MonoBehaviour
{
    [SerializeField]
    PlayFabManager playFabManager;
    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiClickManager playerChoiceBeforeBattleSceneUiClickManager;
    [SerializeField]
    PlayerChoiceBeforeBattleSceneUIMoveManager playerChoiceBeforeBattleSceneUIMoveManager;

    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;

   //[HideInInspector]
   public int nowGridNum = 0;




    private void Start()
    {
        playFabManager.setPlayerAndEnemyData();
    }





    // 캐릭터 그리드 선택시 인벤창이 나오도록 호출되는 함수
    public void playerChoiceGrid()
    {
        // 최대 선택 가능한 캐릭터의 수는 5명입니다.
        if (checkIsFull() == false) return;

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOn();
    }
    // 인벤창에서 클릭한 경우
    public void playerChoiceInvenUiCharArea(int dragNum, string name)
    {
        if (PlayersCharGridDataManager.instance.playerCharChoiceCount >= 1)
        {
            checkIsOverLap(dragNum, name);
            return;
        }
        int[] gridXY = changeNumToGrid(dragNum);
        inFromGrid(gridXY, name);
    }








    //- 빼기
    public void outFromGrid(int[] gridXY)
    {
        if (PlayersCharGridDataManager.instance.nowGridData[gridXY[0], gridXY[1]] == null)
        {
            playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
            return;
        }

        turnOffPlayerChar((gridXY[1] * PlayersCharGridDataManager.instance.nowGridData.GetLength(0)) + gridXY[0]);
        PlayersCharGridDataManager.instance.nowGridData[gridXY[0], gridXY[1]] = null;

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }
    //+ 넣기
    void inFromGrid(int[] gridXY, string name)
    {
        PlayersCharGridDataManager.instance.nowGridData[gridXY[0], gridXY[1]] = name;
        turnOnPlayerChar((gridXY[1] * PlayersCharGridDataManager.instance.nowGridData.GetLength(0)) + gridXY[0], name);

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }
    // 캐릭터 생성하기
    void turnOnPlayerChar(int num, string name)
    {
        GameObject playerCharInGrid = Instantiate(playerChoiceBeforeBattleSceneUiDataManager.playerCharInGrid, playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform);
        playerCharInGrid.name = name;
    }
    // 캐릭터 삭제하기
    void turnOffPlayerChar(int num)
    {
        if (playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform.childCount != 0)
        {
            Destroy(playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform.GetChild(0).gameObject);
        }
    }
    
    




    bool checkIsFull()
    { 
        if (PlayersCharGridDataManager.instance.playerCharChoiceCount >= 5) return false;
        return true;
    }
    // 현재 그리드의 수를 계산함
    void countGridState()
    {
        int count = 0;

        for (int i = 0; i < PlayersCharGridDataManager.instance.nowGridData.GetLength(0); i++)
        {
            for (int k = 0; k < PlayersCharGridDataManager.instance.nowGridData.GetLength(1); k++)
            {
                if (PlayersCharGridDataManager.instance.nowGridData[i, k] != null)
                {
                    count++;
                }
            }
        }

        PlayersCharGridDataManager.instance.playerCharChoiceCount = count;
    }
    int [] findIntInGrid(string name)
    {
        int[] gridXY = new int[2];
        for (int i = 0; i < PlayersCharGridDataManager.instance.nowGridData.GetLength(0); i++)
        {
            for (int k = 0; k < PlayersCharGridDataManager.instance.nowGridData.GetLength(1); k++)
            {
                if (PlayersCharGridDataManager.instance.nowGridData[i, k] == name)
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
        for (int i = 0; i< PlayersCharGridDataManager.instance.nowGridData.GetLength(0); i++)
        {
            for (int k = 0; k < PlayersCharGridDataManager.instance.nowGridData.GetLength(1); k++)
            {
                if (PlayersCharGridDataManager.instance.nowGridData[i, k] == name)
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
            if (num - PlayersCharGridDataManager.instance.nowGridData.GetLength(0) < 0)
            {
                break;
            }
            else
            {
                count++;
                num -= PlayersCharGridDataManager.instance.nowGridData.GetLength(0);
            }
        }

        gridXY[0] = num;
        gridXY[1] = count;

        return gridXY;
    }










    //일반적인 클릭으로 할 경우
    void checkIsOverLap(int numOfClickedGrid, string clickedCharName)
    {
        //그리드 상의 숫자 바꿔주기
        int[] gridXYOfClicked = changeNumToGrid(numOfClickedGrid);

        switch (findBoolInGrid(clickedCharName))
        {
            // 그리드에 중복이 있는 경우 
            case true:
                // 현재 위치에서 빈공간에 이동하는 경우
                if (PlayersCharGridDataManager.instance.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
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

                    string wasString = PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
                    string willString = PlayersCharGridDataManager.instance.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]];

                    outFromGrid(gridXYplayerWillPut);
                    outFromGrid(gridXYOfClicked);
                    inFromGrid(gridXYOfClicked, wasString);
                    inFromGrid(gridXYplayerWillPut, willString);
                }
                break;

            // 그리드에 중복이 없는 경우 
            case false:
                //그냥 넣기
                if (PlayersCharGridDataManager.instance.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
                {
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                //삭제후 넣기
                else
                {
                    outFromGrid(gridXYOfClicked);
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                break;
        }

        putGridDataToPlayPabServer();
    }

    //드래그 드랍으로 할 경우 
    public void ifDragAndDrop(int dragStartNum,int dropStartNum , string dropCharName)
    {
        //그리드 상의 숫자 바꿔주기

        int[] gridXYplayerWasPut = changeNumToGrid(dragStartNum);
        int[] gridXYplayerWillPut = changeNumToGrid(dropStartNum);

        if (PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] == null)
        {
            //기존에 있던 자리값 없애주기
            PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = null;
            // 현재 위치값 넣어주기 
            PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dropCharName;
            countGridState();
        }
        else
        {
            //누른곳이랑 눌린곳 바꿔주기              
            string wasString = PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
            string willString = PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]];

            outFromGrid(gridXYplayerWillPut);
            inFromGrid(gridXYplayerWasPut, wasString);

            // 현재 위치값 넣어주기 
            PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = wasString;
            // 현재 위치값 넣어주기 
            PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = willString;
            countGridState();
        }

        putGridDataToPlayPabServer();
    }





// playpab 관련
//===========================================================================================================
    // playpab에 그리드 값 저장하기
    public void putGridDataToPlayPabServer()
    {
        putNowGridDataToAllGridData();
        PlayFabManager playFabManager = GameObject.Find("PlayPabManager").GetComponent<PlayFabManager>();
        playFabManager.putNowGridData();
        playFabManager.putAllGridData();
    }
    // 현재 그리드의 정보값을 전체 그리드 값에 넣어주기
     void putNowGridDataToAllGridData()
    {
        for (int i  = 0; i< PlayersCharGridDataManager.instance.nowGridData.GetLength(0);i++)
        {
            for (int k = 0; k < PlayersCharGridDataManager.instance.nowGridData.GetLength(1); k++)
            {
                PlayersCharGridDataManager.instance.allGridData[nowGridNum, i,k] = PlayersCharGridDataManager.instance.nowGridData[i, k];
            }
        }    
    }

    // ALL그리드에서 값을 가져와 현재 그리드 값을 바꿔주며 
    // 이미지 또한 바꿔주는 작업을 해야 한다.
    public void setGridDataFromServer()
    {
        turnOffAllCharObj();
        for (int i = 0; i< PlayersCharGridDataManager.instance.nowGridData.GetLength(0);i++)
        {
            for (int k = 0; k < PlayersCharGridDataManager.instance.nowGridData.GetLength(1); k++)
            {
                PlayersCharGridDataManager.instance.nowGridData[i, k] = PlayersCharGridDataManager.instance.allGridData[nowGridNum, i, k];

                if (PlayersCharGridDataManager.instance.allGridData[nowGridNum, i, k] == null) continue;

                int[] gridXY = new int[2];
                gridXY[0] = i;
                gridXY[1] = k;

                inFromGrid(gridXY, PlayersCharGridDataManager.instance.nowGridData[i, k]);
            }
        }
    }
    // 현재 그리드에 있는 모든 아이들을 다 밀어 버림
    void turnOffAllCharObj()
    {
        for (int i = 0; i < playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum.Length; i++)
        {
            if (playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[i].transform.childCount != 0)
            {
                turnOffPlayerChar(i);
            }        
        }
    }




    // 만약 토글값이 변동되어 nowGridNum이 바뀌면 
    // 해당 num의 그리드 값을 가져오기.
}
