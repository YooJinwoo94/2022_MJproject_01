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
    // 빨리 움직일 배경 
    [SerializeField]
    InGameSceneBGScrollManager[] inGameSceneBGScrollManager;


    private void Start()
    {
        //배경 이동
        bgMove(true);

        //카메라 세팅
        isCamGetDistance(true);

        // 스타트 버튼 안보이게 하기 
        battleStartBtn.SetActive(false);

        // 데이터를 받아오기
        PlayerChoiceBeforeBattleSceneGetStageDataFromGoogleSheet.instance.setDataToStage();

        // 받아온 데이터 이미지 보여주기
        setPlayerCharData();

        //자 이제 전투 위치로 이동해야 할 시간이야
        inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.cutScene_playerCharWalkIn;
    }


    private void Update()
    {
        if (inGameSceneUiDataManager.nowGameSceneState == 
            InGameSceneUiDataManager.NowGameSceneState.battleStart) turnOnOffPlayerCharCol(false);
        else turnOnOffPlayerCharCol(true);

        //이후 레이드가 남아있는경우 다음 레이드때 캐릭터가 이동할 수 있도록 합니다.
        if (inGameSceneUiDataManager.nowGameSceneState == 
            InGameSceneUiDataManager.NowGameSceneState.playerCharMoveForNextRaid)
        {
            //뒤로 빠꾸빠꾸 
            charMove();

            bgMove(true);
            isCamGetDistance(true);
        }

        else ifBattleStart();
    }




    // 전투 시작 버튼을 누른경우 발동!
    public void battleStart()
    {
        // enum state를 배틀로 변경
        inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.battleStart;

        //스폰후 다들 도착했는지 확인하기 위한 함수 초기화
        inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos = 0;
        inGameSceneUiDataManager.countEnemyCharArrivedToAttackPos = 0;

        //버튼 비활성화
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

         // 새로운 레이드가 시작될 예정입니다.
         if (inGameSceneUiDataManager.enemyObjList.Count == 0 && 
            inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.playerCharMoveForNextRaid)
          {
            inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.playerCharMoveForNextRaid;
          }
    }






    //=========================================================================== string을 읽어서 몬스터 및 플레이어 캐릭터들을 활성화 시킨다.
    //playfab에서는 세로로 저장되기 때문에 반대로 읽어야 한다.
    //플레이어 캐릭터의 데이터를 가져온다.
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
    //적 캐릭터의 데이터를 가져온다.
    //다음 inGameSceneUiDataManager.battleSceneCount로 되면 다시 호출해야 한다.
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












    //한명이라도 전투가 시작하면 배경이 멈춘다. // 전투가 아니면 + 이동시 배경이 멈춘다.
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


    //뒤로 빠꾸빠꾸 
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


    //캐릭터들이 원위치로 이동했음 -> 이제 적 캐릭터가 나와야 하는 시점 
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


















    //드래그 드랍 영역
    //======================================================================================================================

    public void ifDragAndDrop(int dragStartNum, int dropStartNum, GameObject dragObj)
    {
        int[] gridXYplayerWasPut = changeNumToGrid(dragStartNum);
        int[] gridXYplayerWillPut = changeNumToGrid(dropStartNum);
        
        CharState dragObjCharState = dragObj.GetComponentInChildren<CharState>();

        //원래 있던 위치로 다시 던진 경우
        if (dragStartNum == dropStartNum)
        {
            GameObjectDragManager gameObjectDragManager = dragObj.GetComponentInChildren<GameObjectDragManager>();

            gameObjectDragManager.goBackToOrginPos(dragObj);

            return;
        }

        //다른 칸에 넣은 경우
        switch (inGameScenePlayerCharGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]])
        {
            case null:
                Debug.Log("빈칸에 들어가기");
                // 그리드 data 교환
                inGameScenePlayerCharGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = null;
                inGameScenePlayerCharGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dragObjCharState.charName;

                // 부모 자식 위치를 정상적으로 다시 바꿈
                dragObj.transform.SetParent(inGameSceneUiDataManager.playerCharInGridPos[dropStartNum].GetComponent<Transform>());

                // 실제위치 교환 
                dragObj.transform.position = inGameSceneUiDataManager.playerCharMovePosBeforeBattle[dropStartNum].GetComponent<Transform>().position;

                // 캐릭터 state 상의 수치 변경
                dragObjCharState.sponPos = dropStartNum; 
                break;

            default:
                Debug.Log("다른 아이와 바꾸기");
  
                CharState dropObjCharState = inGameSceneUiDataManager.playerCharInGridPos[dropStartNum].GetComponentInChildren<CharState>();

                // 그리드 data 교환
                inGameScenePlayerCharGridData.nowGridData[gridXYplayerWillPut[0], gridXYplayerWillPut[1]] = dragObjCharState.charName;
                inGameScenePlayerCharGridData.nowGridData[gridXYplayerWasPut[0], gridXYplayerWasPut[1]] = dropObjCharState.charName;

                // 부모 자식 위치를 정상적으로 다시 바꿈
                dragObj.transform.SetParent(inGameSceneUiDataManager.playerCharInGridPos[dropStartNum].GetComponent<Transform>());
                inGameSceneUiDataManager.playerCharInGridPos[dropStartNum].transform.GetChild(0).SetParent(inGameSceneUiDataManager.playerCharInGridPos[dragStartNum].GetComponent<Transform>());

                // 실제위치 교환 
                 dragObj.transform.position = inGameSceneUiDataManager.playerCharMovePosBeforeBattle[dropStartNum].GetComponent<Transform>().position;
                 inGameSceneUiDataManager.playerCharInGridPos[dragStartNum].transform.GetChild(0).position = inGameSceneUiDataManager.playerCharMovePosBeforeBattle[dragStartNum].GetComponent<Transform>().position;

                // 캐릭터 state 상의 수치 변경
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
}
