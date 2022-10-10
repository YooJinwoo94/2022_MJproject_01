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

    // ���� �׸����� ĳ���� ��
    public int playerCharChoiceCount = 0;


    private void Start()
    {
        setGridDataFromServer();
    }








    // ĳ���� �׸��� ���ý� �κ�â�� �������� ȣ��Ǵ� �Լ�
    public void playerChoiceGrid()
    {
        // �ִ� ���� ������ ĳ������ ���� 5���Դϴ�.
        if (checkIsFull() == false) return;

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOn();
    }
    // �κ�â���� Ŭ���� ���
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








    //- ����
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
    //+ �ֱ�
    void inFromGrid(int[] gridXY, string name)
    {
        PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXY[0], gridXY[1]] = name;
        turnOnPlayerChar((gridXY[1] * PlayerDataJsonManager.instance.playerData.charGridData.nowGridData.GetLength(0)) + gridXY[0], name);

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }
    // ĳ���� �����ϱ�
    void turnOnPlayerChar(int num, string name)
    {
        GameObject playerCharInGrid = Instantiate(playerChoiceBeforeBattleSceneUiDataManager.playerCharInGrid, playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform);
        playerCharInGrid.name = name;

        SkeletonGraphic skeletonGraphic = playerCharInGrid.transform.GetComponentInChildren<SkeletonGraphic>();
        int count = 0;

        switch (name)
        {
            case "����������":
                count = 0;
                setPlayerAni(count, "character_001_normal", skeletonGraphic);
                break;
            case "����������":
                count = 1;
                setPlayerAni(count, "character_002_normal", skeletonGraphic);
                break;
            case "�ٴٴٴٴ�":
                 count = 2;
                setPlayerAni(count, "character_003_normal", skeletonGraphic);
                break;
            case "������":
                 count = 3;
                setPlayerAni(count, "character_004_normal", skeletonGraphic);
                break;
            case "����������":
                 count = 4;
                setPlayerAni(count, "character_005_normal", skeletonGraphic);
                break;
            case "�ٹٹٹٹ�":
                 count = 5;
                setPlayerAni(count, "character_006_normal", skeletonGraphic);
                break;
            case "������":
                count = 6;
                setPlayerAni(count, "character_007_normal", skeletonGraphic);
                break;
            case "�ƾƾƾƾ�":
                count = 7;
                setPlayerAni(count, "character_008_normal", skeletonGraphic);
                break;
        }
    }
    // ĳ���� �����ϱ�
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
    // ���� �׸����� ���� �����
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
            //playerCharChoiceData.GetLength(0) == ���߹迭�� �� ( ������ ) 
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










    //�Ϲ����� Ŭ������ �� ���
    void checkIsOverLap(int numOfClickedGrid, string clickedCharName)
    {
        //�׸��� ���� ���� �ٲ��ֱ�
        int[] gridXYOfClicked = changeNumToGrid(numOfClickedGrid);

        switch (findBoolInGrid(clickedCharName))
        {
            // �׸��忡 �ߺ��� �ִ� ��� 
            case true:
                // ���� ��ġ���� ������� �̵��ϴ� ���
                if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
                {
                    int[] gridXYOfCharPast = findIntInGrid(clickedCharName);

                    outFromGrid(gridXYOfCharPast);
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                // �ٲ��ֱ�
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

            // �׸��忡 �ߺ��� ���� ��� 
            case false:
                //�׳� �ֱ�
                if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
                {
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                //������ �ֱ�
                else
                {
                    outFromGrid(gridXYOfClicked);
                    inFromGrid(gridXYOfClicked, clickedCharName);
                }
                break;
        }
    }

    //�巡�� ������� �� ��� 
    public void ifDragAndDrop(int dragStartNum,int dropStartNum , string dropCharName)
    {
        //�׸��� ���� ���� �ٲ��ֱ�

        int[] gridXYplayerWasPut = changeNumToGrid(dragStartNum);
        int[] gridXYplayerWillPut = changeNumToGrid(dropStartNum);


        if (PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] == null)
        {
            //������ �ִ� �ڸ��� �����ֱ�
            PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = null;
            // ���� ��ġ�� �־��ֱ� 
            PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dropCharName;
            countGridState();
        }
        else
        {
            //�������̶� ������ �ٲ��ֱ�              
            string wasString = PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
            string willString = PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]];

            outFromGrid(gridXYplayerWillPut);
            inFromGrid(gridXYplayerWasPut, wasString);

            // ���� ��ġ�� �־��ֱ� 
            PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = wasString;
            // ���� ��ġ�� �־��ֱ� 
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
    // ���� �׸����� �������� ��ü �׸��� ���� �־��ֱ�
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

    // ALL�׸��忡�� ���� ������ ���� �׸��� ���� �ٲ��ָ� 
    // �̹��� ���� �ٲ��ִ� �۾��� �ؾ� �Ѵ�.
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
    // ���� �׸��忡 �ִ� ��� ���̵��� �� �о� ����
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
