using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine;
using Spine.Unity;


public class PlayerChoiceBeforeBattleSceneSceneManager : MonoBehaviour
{
    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiClickManager playerChoiceBeforeBattleSceneUiClickManager;
    [SerializeField]
    PlayerChoiceBeforeBattleSceneUIMoveManager playerChoiceBeforeBattleSceneUIMoveManager;

    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;

   //[HideInInspector]
   public int nowGridNum = 0;

    // 현재 그리드의 캐릭의 수
    public int playerCharChoiceCount = 0;


    private void Start()
    {
        setGridDataFromServer();
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
        if (playerCharChoiceCount >= 1)
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
        if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXY[0], gridXY[1]] == null)
        {
            playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
            return;
        }

        turnOffPlayerChar((gridXY[1] * PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0)) + gridXY[0]);
        PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXY[0], gridXY[1]] = null;

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }
    //+ 넣기
    void inFromGrid(int[] gridXY, string name)
    {
        PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXY[0], gridXY[1]] = name;
        turnOnPlayerChar((gridXY[1] * PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0)) + gridXY[0], name);

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }
    // 캐릭터 생성하기
    void turnOnPlayerChar(int num, string name)
    {
        GameObject playerCharInGrid = Instantiate(playerChoiceBeforeBattleSceneUiDataManager.playerCharInGrid, playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform);
        playerCharInGrid.name = name;

        SkeletonGraphic skeletonGraphic = playerCharInGrid.transform.GetComponentInChildren<SkeletonGraphic>();
        int count = 0;

        switch (name)
        {
            case "가가가가가":
                count = 0;
                setPlayerAni(count, "character_001_normal", skeletonGraphic);
                break;
            case "나나나나나":
                count = 1;
                setPlayerAni(count, "character_002_normal", skeletonGraphic);
                break;
            case "다다다다다":
                 count = 2;
                setPlayerAni(count, "character_003_normal", skeletonGraphic);
                break;
            case "라라라라라":
                 count = 3;
                setPlayerAni(count, "character_004_normal", skeletonGraphic);
                break;
            case "마마마마마":
                 count = 4;
                setPlayerAni(count, "character_005_normal", skeletonGraphic);
                break;
            case "바바바바바":
                 count = 5;
                setPlayerAni(count, "character_006_normal", skeletonGraphic);
                break;
            case "사사사사사":
                count = 6;
                setPlayerAni(count, "character_007_normal", skeletonGraphic);
                break;
            case "아아아아아":
                count = 7;
                setPlayerAni(count, "character_008_normal", skeletonGraphic);
                break;
        }
    }
    // 캐릭터 삭제하기
    void turnOffPlayerChar(int num)
    {
        if (playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform.childCount != 0)
        {
            Destroy(playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform.GetChild(0).gameObject);
        }
    }

    void setPlayerAni(int num, string characterSkinName ,SkeletonGraphic skeletonGraphic)
    {
        skeletonGraphic.skeletonDataAsset = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].skeletonDataAsset;
        skeletonGraphic.startingAnimation = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].startingAnimation;

        skeletonGraphic.initialSkinName = characterSkinName + "/1_normal";
        skeletonGraphic.Initialize(true);
    }




    bool checkIsFull()
    { 
        if (playerCharChoiceCount >= 5) return false;
        return true;
    }
    // 현재 그리드의 수를 계산함
    public void countGridState()
    {
        int count = 0;

        for (int i = 0; i < PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0); i++)
        {
            for (int k = 0; k < PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(1); k++)
            {
                if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[i, k] != null)
                {
                    count++;
                }
            }
        }

        playerCharChoiceCount = count;
    }
    int [] findIntInGrid(string name)
    {
        int[] gridXY = new int[2];
        for (int i = 0; i < PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0); i++)
        {
            for (int k = 0; k < PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(1); k++)
            {
                if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[i, k] == name)
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
        for (int i = 0; i< PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0); i++)
        {
            for (int k = 0; k < PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(1); k++)
            {
                if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[i, k] == name)
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
            if (num - PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0) < 0)
            {
                break;
            }
            else
            {
                count++;
                num -= PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0);
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
                if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
                {
                    int[] gridXYOfCharPast = findIntInGrid(clickedCharName);

                    outFromGrid(gridXYOfCharPast);
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                // 바꿔주기
                else
                {
                    int[] gridXYplayerWillPut = findIntInGrid(clickedCharName);

                    string wasString = PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
                    string willString = PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]];

                    outFromGrid(gridXYplayerWillPut);
                    outFromGrid(gridXYOfClicked);
                    inFromGrid(gridXYOfClicked, wasString);
                    inFromGrid(gridXYplayerWillPut, willString);
                }
                break;

            // 그리드에 중복이 없는 경우 
            case false:
                //그냥 넣기
                if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
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
    }

    //드래그 드랍으로 할 경우 
    public void ifDragAndDrop(int dragStartNum,int dropStartNum , string dropCharName)
    {
        //그리드 상의 숫자 바꿔주기

        int[] gridXYplayerWasPut = changeNumToGrid(dragStartNum);
        int[] gridXYplayerWillPut = changeNumToGrid(dropStartNum);


        if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] == null)
        {
            //기존에 있던 자리값 없애주기
            PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = null;
            // 현재 위치값 넣어주기 
            PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dropCharName;
            countGridState();
        }
        else
        {
            //누른곳이랑 눌린곳 바꿔주기              
            string wasString = PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
            string willString = PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]];

            outFromGrid(gridXYplayerWillPut);
            inFromGrid(gridXYplayerWasPut, wasString);

            // 현재 위치값 넣어주기 
            PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = wasString;
            // 현재 위치값 넣어주기 
            PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = willString;
            countGridState();
        }

        putGridDataToPlayPabServer();
    }





















//===========================================================================================================
    public void putGridDataToPlayPabServer()
    {
        putNowGridDataToAllGridData();

        SaveLoadDataManager saveLoadDataManager = GameObject.Find("Manager").GetComponentInChildren<SaveLoadDataManager>();
        saveLoadDataManager.savePlayerData();
        // PlayFabManager playFabManager = GameObject.Find("PlayPabManager").GetComponent<PlayFabManager>();
        //playFabManager.putNowGridData();
        //playFabManager.putAllGridData();
    }
    // 현재 그리드의 정보값을 전체 그리드 값에 넣어주기
     void putNowGridDataToAllGridData()
    {
        for (int i  = 0; i< PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0);i++)
        {
            for (int k = 0; k < PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(1); k++)
            {
                PlayerDataJsonManager.instance.playerData.charGridData.allGridData[nowGridNum, i,k] = PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[i, k];
            }
        }    
    }

    // ALL그리드에서 값을 가져와 현재 그리드 값을 바꿔주며 
    // 이미지 또한 바꿔주는 작업을 해야 한다.
    public void setGridDataFromServer()
    {
        turnOffAllCharObj();

        for (int i = 0; i< PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0);i++)
        {
            for (int k = 0; k < PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(1); k++)
            {
               PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[i, k] = PlayerDataJsonManager.instance.playerData.charGridData.allGridData[nowGridNum, i, k];

                if (PlayerDataJsonManager.instance.playerData.charGridData.allGridData[nowGridNum, i, k] == null) continue;

                int[] gridXY = new int[2];
                gridXY[0] = i;
                gridXY[1] = k;

                inFromGrid(gridXY, PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[i, k]);
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
}
