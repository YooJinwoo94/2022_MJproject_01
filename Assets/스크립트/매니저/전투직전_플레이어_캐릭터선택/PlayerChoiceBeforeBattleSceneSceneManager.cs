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
        if (PlayersCharGridDataManager.instance.playerCharChoiceCount >= 1)
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
    //+ �ֱ�
    void inFromGrid(int[] gridXY, string name)
    {
        PlayersCharGridDataManager.instance.nowGridData[gridXY[0], gridXY[1]] = name;
        turnOnPlayerChar((gridXY[1] * PlayersCharGridDataManager.instance.nowGridData.GetLength(0)) + gridXY[0], name);

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
        countGridState();
    }
    // ĳ���� �����ϱ�
    void turnOnPlayerChar(int num, string name)
    {
        GameObject playerCharInGrid = Instantiate(playerChoiceBeforeBattleSceneUiDataManager.playerCharInGrid, playerChoiceBeforeBattleSceneUiDataManager.playerCharInGridPosNum[num].transform);
        playerCharInGrid.name = name;
    }
    // ĳ���� �����ϱ�
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
    // ���� �׸����� ���� �����
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
            //playerCharChoiceData.GetLength(0) == ���߹迭�� �� ( ������ ) 
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
                if (PlayersCharGridDataManager.instance.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
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

                    string wasString = PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
                    string willString = PlayersCharGridDataManager.instance.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]];

                    outFromGrid(gridXYplayerWillPut);
                    outFromGrid(gridXYOfClicked);
                    inFromGrid(gridXYOfClicked, wasString);
                    inFromGrid(gridXYplayerWillPut, willString);
                }
                break;

            // �׸��忡 �ߺ��� ���� ��� 
            case false:
                //�׳� �ֱ�
                if (PlayersCharGridDataManager.instance.nowGridData[gridXYOfClicked[0], gridXYOfClicked[1]] == null)
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

        putGridDataToPlayPabServer();
    }

    //�巡�� ������� �� ��� 
    public void ifDragAndDrop(int dragStartNum,int dropStartNum , string dropCharName)
    {
        //�׸��� ���� ���� �ٲ��ֱ�

        int[] gridXYplayerWasPut = changeNumToGrid(dragStartNum);
        int[] gridXYplayerWillPut = changeNumToGrid(dropStartNum);

        if (PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] == null)
        {
            //������ �ִ� �ڸ��� �����ֱ�
            PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = null;
            // ���� ��ġ�� �־��ֱ� 
            PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dropCharName;
            countGridState();
        }
        else
        {
            //�������̶� ������ �ٲ��ֱ�              
            string wasString = PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]];
            string willString = PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]];

            outFromGrid(gridXYplayerWillPut);
            inFromGrid(gridXYplayerWasPut, wasString);

            // ���� ��ġ�� �־��ֱ� 
            PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = wasString;
            // ���� ��ġ�� �־��ֱ� 
            PlayersCharGridDataManager.instance.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = willString;
            countGridState();
        }

        putGridDataToPlayPabServer();
    }





// playpab ����
//===========================================================================================================
    // playpab�� �׸��� �� �����ϱ�
    public void putGridDataToPlayPabServer()
    {
        putNowGridDataToAllGridData();
        PlayFabManager playFabManager = GameObject.Find("PlayPabManager").GetComponent<PlayFabManager>();
        playFabManager.putNowGridData();
        playFabManager.putAllGridData();
    }
    // ���� �׸����� �������� ��ü �׸��� ���� �־��ֱ�
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

    // ALL�׸��忡�� ���� ������ ���� �׸��� ���� �ٲ��ָ� 
    // �̹��� ���� �ٲ��ִ� �۾��� �ؾ� �Ѵ�.
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




    // ���� ��۰��� �����Ǿ� nowGridNum�� �ٲ�� 
    // �ش� num�� �׸��� ���� ��������.
}
