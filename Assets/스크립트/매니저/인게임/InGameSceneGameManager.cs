using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;




public class InGameSceneGameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject battleStartBtn;
    [SerializeField]
    InGameSceneUiDataManager inGameSceneUiDataManager;
    [SerializeField]
    InGameScenePlayerCharGridData inGameScenePlayerCharGridData;
    [SerializeField]
    GameObject[] cinemachineVirtualCamera;
    // ���� ������ ��� 
    [SerializeField]
    InGameSceneBGScrollManager[] inGameSceneBGScrollManager;


    private void Start()
    {
        //��� �̵�
        bgMove(true);

        //ī�޶� ����
        isCamGetDistance(true);

        // ��ŸƮ ��ư �Ⱥ��̰� �ϱ� 
        battleStartBtn.SetActive(false);

        // �����͸� �޾ƿ���
        PlayerChoiceBeforeBattleSceneGetStageDataFromGoogleSheet.instance.setDataToStage();

        // �޾ƿ� ������ �̹��� �����ֱ�
        setPlayerCharData();

        //�� ���� ���� ��ġ�� �̵��ؾ� �� �ð��̾�
        inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.cutScene_playerCharWalkIn;
    }


    private void Update()
    {
        if (inGameSceneUiDataManager.nowGameSceneState == 
            InGameSceneUiDataManager.NowGameSceneState.battleStart) turnOnOffPlayerCharCol(false);
        else turnOnOffPlayerCharCol(true);

        //���� ���̵尡 �����ִ°�� ���� ���̵嶧 ĳ���Ͱ� �̵��� �� �ֵ��� �մϴ�.
        if (inGameSceneUiDataManager.nowGameSceneState == 
            InGameSceneUiDataManager.NowGameSceneState.playerCharMoveForNextRaid)
        {
            //�ڷ� ���ٺ��� 
            charMove();

            bgMove(true);
            isCamGetDistance(true);
        }

        else ifBattleStart();
    }




    // ���� ���� ��ư�� ������� �ߵ�!
    public void battleStart()
    {
        // enum state�� ��Ʋ�� ����
        inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.battleStart;

        //������ �ٵ� �����ߴ��� Ȯ���ϱ� ���� �Լ� �ʱ�ȭ
        inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos = 0;
        inGameSceneUiDataManager.countEnemyCharArrivedToAttackPos = 0;

        //��ư ��Ȱ��ȭ
        battleStartBtn.SetActive(false);

        StartCoroutine("SettingStart");
    }
     IEnumerator SettingStart()
    {
        isCamGetDistance(false);
        bgMove(true);
        yield return null;
    }



    public void ifDeadCheckLeftbattleSceneCount()
    {
         if (inGameSceneUiDataManager.battleSceneCount == 2) return;

         // ���ο� ���̵尡 ���۵� �����Դϴ�.
         if (inGameSceneUiDataManager.enemyObjList.Count == 0 && 
            inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.playerCharMoveForNextRaid)
          {
            inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.playerCharMoveForNextRaid;
          }
    }






    //=========================================================================== string�� �о ���� �� �÷��̾� ĳ���͵��� Ȱ��ȭ ��Ų��.
    //playfab������ ���η� ����Ǳ� ������ �ݴ�� �о�� �Ѵ�.
    //�÷��̾� ĳ������ �����͸� �����´�.
    void setPlayerCharData()
    {
        for (int i = 0; i < PlayersCharGridDataManager.instance.nowGridData.GetLength(0); i++)
        {
            for (int k = 0; k < PlayersCharGridDataManager.instance.nowGridData.GetLength(1); k++)
            {
                if (PlayersCharGridDataManager.instance.nowGridData[i, k] == null)
                {
                    continue;
                }
                int num = 0;
                num = k * PlayersCharGridDataManager.instance.nowGridData.GetLength(0) + i;

                turnOnPlayerChar(num, PlayersCharGridDataManager.instance.nowGridData[i, k]);
            }
        }
    }
    //�� ĳ������ �����͸� �����´�.
    //���� inGameSceneUiDataManager.battleSceneCount�� �Ǹ� �ٽ� ȣ���ؾ� �Ѵ�.
    public void setEnemyCharData()
    {
        for (int i = 0; i < inGameSceneUiDataManager.enemyCharInGrid.GetLength(1); i++)
        {
            for (int k = 0; k < inGameSceneUiDataManager.enemyCharInGrid.GetLength(2); k++)
            {
                if (inGameSceneUiDataManager.enemyCharInGrid[inGameSceneUiDataManager.battleSceneCount, i, k] == "null" ||
                    inGameSceneUiDataManager.enemyCharInGrid[inGameSceneUiDataManager.battleSceneCount, i, k] == null)
                {
                    continue;
                }
                int num = 0;
                num = k * inGameSceneUiDataManager.enemyCharInGrid.GetLength(1) + i;

                turnOnEnemyChar(num, inGameSceneUiDataManager.enemyCharInGrid[inGameSceneUiDataManager.battleSceneCount, i, k]);
            }
        }
    }

    void turnOnPlayerChar(int num, string name)
    {
        GameObject playerCharInGrid = Instantiate(inGameSceneUiDataManager.playerObj, inGameSceneUiDataManager.playerCharInGridPos[num].transform);
        playerCharInGrid.name = name;

        CharState charState = playerCharInGrid.transform.GetComponentInChildren<CharState>();
        charState.sponPos = num;

        inGameSceneUiDataManager.playerObjList.Add(playerCharInGrid);
    }
    void turnOnEnemyChar(int num, string name)
    {
        GameObject enemyChatObj = Instantiate(inGameSceneUiDataManager.enemyObj, inGameSceneUiDataManager.enemyCharInGridPos[num].transform);
        enemyChatObj.name = name;
        CharState charState = enemyChatObj.transform.GetComponentInChildren<CharState>();
        charState.sponPos = num;
        inGameSceneUiDataManager.enemyObjList.Add(enemyChatObj);
    }












    //�Ѹ��̶� ������ �����ϸ� ����� �����. // ������ �ƴϸ� + �̵��� ����� �����.
    public void ifBattleStart()
    {
        for (int i = 0; i < inGameSceneUiDataManager.playerObjList.Count; i++)
        {
            CharState charState = inGameSceneUiDataManager.playerObjList[i].transform.GetComponentInChildren<CharState>();

            if (charState.nowState == CharState.NowState.isReadyForAttack || charState.nowState == CharState.NowState.isFindingBush)
            {
                bgMove(false);
                isCamGetDistance(false);
                return;
            }
        }
    }
    public void ifRunStart()
    {
        for (int i = 0; i < inGameSceneUiDataManager.playerObjList.Count; i++)
        {
            CharState charState = inGameSceneUiDataManager.playerObjList[i].transform.GetComponentInChildren<CharState>();

            if (charState.nowState == CharState.NowState.isWalkToEnemy)
            {
                bgMove(true);
                isCamGetDistance(true);
                return;
            }
        }
    }


    //�ڷ� ���ٺ��� 
    public void charMove()
    {
        setCharAniMove();

        for (int i = 0; i < inGameSceneUiDataManager.playerObjList.Count; i++)
        {
            CharState charState = inGameSceneUiDataManager.playerObjList[i].transform.GetComponentInChildren<CharState>();

            Vector2 movePos = new Vector2(inGameSceneUiDataManager.playerCharInGridPos[charState.sponPos].transform.position.x,
                                          inGameSceneUiDataManager.playerCharInGridPos[charState.sponPos].transform.position.y);

            inGameSceneUiDataManager.playerObjList[i].transform.position = Vector3.MoveTowards(inGameSceneUiDataManager.playerObjList[i].transform.position,
                                                                                               movePos, charState.moveSpeed * Time.deltaTime);

            if (inGameSceneUiDataManager.playerObjList[i].transform.position == inGameSceneUiDataManager.playerCharInGridPos[charState.sponPos].transform.position)
            {
                readyIsEndForNextRaid();
                return;
            }
        }
    }

    void setCharAniMove()
    {
        InGameSceneCharSpineAniCon[] inGameSceneCharSpineAniCon;
        CharState[] charState;

        charState = new CharState[inGameSceneUiDataManager.playerObjList.Count];
        inGameSceneCharSpineAniCon = new InGameSceneCharSpineAniCon[inGameSceneUiDataManager.playerObjList.Count];

        for (int i = 0; i < inGameSceneUiDataManager.playerObjList.Count; i++)
        {
            inGameSceneCharSpineAniCon[i] = inGameSceneUiDataManager.playerObjList[i].GetComponentInChildren<InGameSceneCharSpineAniCon>();
            charState[i] = inGameSceneUiDataManager.playerObjList[i].GetComponentInChildren<CharState>();

            charState[i].nowState = CharState.NowState.isWaitForCutScene;
            inGameSceneCharSpineAniCon[i].run();
        }
    }


    //ĳ���͵��� ����ġ�� �̵����� -> ���� �� ĳ���Ͱ� ���;� �ϴ� ���� 
    void readyIsEndForNextRaid()
    {
        inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.cutScene_playerCharWalkIn;

        inGameSceneUiDataManager.battleSceneCount++;
    }

    public void bgMove(bool isMove)
    {
        for (int i = 0; i < inGameSceneBGScrollManager.Length; i++)
        {
            inGameSceneBGScrollManager[i].moveStart = isMove;
        }
    }
    public void isCamGetDistance(bool isDistance)
    {
        if (isDistance == true)
        {
            cinemachineVirtualCamera[0].SetActive(true);
            cinemachineVirtualCamera[1].SetActive(false);
        }
        else
        {
            cinemachineVirtualCamera[0].SetActive(false);
            cinemachineVirtualCamera[1].SetActive(true);
        }
    }

    public void turnOnOffPlayerCharCol(bool isTurnOn)
    {
        BoxCollider2D[] box2D = new BoxCollider2D[inGameSceneUiDataManager.playerObjList.Count];

        for (int i = 0; i < inGameSceneUiDataManager.playerObjList.Count; i++)
        {
            box2D[i] = inGameSceneUiDataManager.playerObjList[i].GetComponentInChildren<BoxCollider2D>();
            box2D[i].isTrigger = isTurnOn;
        }
    }


















    //�巡�� ��� ����
    //======================================================================================================================

    public void ifDragAndDrop(int dragStartNum, int dropStartNum, GameObject dragObj)
    {
        int[] gridXYplayerWasPut = changeNumToGrid(dragStartNum);
        int[] gridXYplayerWillPut = changeNumToGrid(dropStartNum);
        
        CharState dragObjCharState = dragObj.GetComponentInChildren<CharState>();

        //���� �ִ� ��ġ�� �ٽ� ���� ���
        if (dragStartNum == dropStartNum)
        {
            GameObjectDragManager gameObjectDragManager = dragObj.GetComponentInChildren<GameObjectDragManager>();

            gameObjectDragManager.goBackToOrginPos(dragObj);

            return;
        }

        //�ٸ� ĭ�� ���� ���
        switch (inGameScenePlayerCharGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]])
        {
            case null:
                Debug.Log("��ĭ�� ����");
                // �׸��� data ��ȯ
                inGameScenePlayerCharGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = null;
                inGameScenePlayerCharGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dragObjCharState.charName;

                // �θ� �ڽ� ��ġ�� ���������� �ٽ� �ٲ�
                dragObj.transform.SetParent(inGameSceneUiDataManager.playerCharInGridPos[dropStartNum].GetComponent<Transform>());

                // ������ġ ��ȯ 
                dragObj.transform.position = inGameSceneUiDataManager.playerCharMovePosBeforeBattle[dropStartNum].GetComponent<Transform>().position;

                // ĳ���� state ���� ��ġ ����
                dragObjCharState.sponPos = dropStartNum; 
                break;

            default:
                Debug.Log("�ٸ� ���̿� �ٲٱ�");
  
                CharState dropObjCharState = inGameSceneUiDataManager.playerCharInGridPos[dropStartNum].GetComponentInChildren<CharState>();

                // �׸��� data ��ȯ
                inGameScenePlayerCharGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dragObjCharState.charName;
                inGameScenePlayerCharGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = dropObjCharState.charName;

                // �θ� �ڽ� ��ġ�� ���������� �ٽ� �ٲ�
                dragObj.transform.SetParent(inGameSceneUiDataManager.playerCharInGridPos[dropStartNum].GetComponent<Transform>());
                inGameSceneUiDataManager.playerCharInGridPos[dropStartNum].transform.GetChild(0).SetParent(inGameSceneUiDataManager.playerCharInGridPos[dragStartNum].GetComponent<Transform>());

                // ������ġ ��ȯ 
                 dragObj.transform.position = inGameSceneUiDataManager.playerCharMovePosBeforeBattle[dropStartNum].GetComponent<Transform>().position;
                 inGameSceneUiDataManager.playerCharInGridPos[dragStartNum].transform.GetChild(0).position = inGameSceneUiDataManager.playerCharMovePosBeforeBattle[dragStartNum].GetComponent<Transform>().position;

                // ĳ���� state ���� ��ġ ����
                dragObjCharState.sponPos = dropStartNum;
                dropObjCharState.sponPos = dragStartNum;
                break;
        }
    }

    int[] changeNumToGrid(int num)
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
}
