using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InGameSceneGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject battleStartBtn;
    [SerializeField]
    InGameSceneUiDataManager inGameSceneUiDataManager;

    [SerializeField]
    GameObject[] cinemachineVirtualCamera;
    // 빨리 움직일 배경 
    [SerializeField]
    InGameSceneBGScrollManager[] inGameSceneBGScrollManager;

    private void FixedUpdate()
    {
       if (inGameSceneUiDataManager.waitForRaid == true) inGameSceneUiDataManager.isBattleStart = false;

       if (battleStartBtn.activeInHierarchy == true ||
           inGameSceneUiDataManager.isBattleStart == true ||
           inGameSceneUiDataManager.waitForRaid == true) return;

       if (inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos ==
           inGameSceneUiDataManager.playerObjList.Count)
        {
            bgMove(false);
            battleStartBtn.SetActive(true);
        }
    }

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
    }


    private void Update()
    {
        if (inGameSceneUiDataManager.isBattleStart == true) turnOnOffPlayerCharCol(false);
        else turnOnOffPlayerCharCol(true);


        if (inGameSceneUiDataManager.waitForRaid == true)
        {
            charMove();

            bgMove(true);
            isCamGetDistance(true);
        }
        else ifBattleStart();
    }




    // 전투 시작 버튼을 누른경우 발동!
    public void battleStart()
    {
        inGameSceneUiDataManager.isBattleStart = true;
        inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos = 0;
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
         if (inGameSceneUiDataManager.enemyObjList.Count == 0 && inGameSceneUiDataManager.waitForRaid == false)
          {
            inGameSceneUiDataManager.waitForRaid = true;
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



    public void charMove()
    {
        setCharAniMove();

        for (int i = 0; i < inGameSceneUiDataManager.playerObjList.Count; i++)
        {
            CharState charState = inGameSceneUiDataManager.playerObjList[i].transform.GetComponentInChildren<CharState>();

            Vector2 movePos = new Vector2(inGameSceneUiDataManager.playerCharInGridPos[charState.sponPos].transform.position.x,
                                          inGameSceneUiDataManager.playerCharInGridPos[charState.sponPos].transform.position.y);

            inGameSceneUiDataManager.playerObjList[i].transform.position = Vector3.MoveTowards(inGameSceneUiDataManager.playerObjList[i].transform.position,
                                                                                               movePos, 1f * Time.deltaTime);

            if (inGameSceneUiDataManager.playerObjList[i].transform.position == inGameSceneUiDataManager.playerCharInGridPos[charState.sponPos].transform.position)
            {
                nextRaidStart();
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

            charState[i].nowState = CharState.NowState.isWalkToOrginPos;
            inGameSceneCharSpineAniCon[i].run();
        }
    }



    public void nextRaidStart()
    {
        inGameSceneUiDataManager.waitForRaid = false;

        inGameSceneUiDataManager.battleSceneCount++;
        setEnemyCharData();
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
            box2D[i] = inGameSceneUiDataManager.playerObjList[i].GetComponent<BoxCollider2D>();
            box2D[i].isTrigger = isTurnOn;
        }
    }
}
