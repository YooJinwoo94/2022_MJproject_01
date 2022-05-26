using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class InGameSceneIfRaidEnd : MonoBehaviour
{
    [SerializeField]
    InGameSceneGameManager inGameSceneGameManager;
    [SerializeField]
    InGameSceneUiDataManager inGameSceneUiDataManager;
    [SerializeField]
    GameObject[] cinemachineVirtualCamera;
    // 빨리 움직일 배경 
    [SerializeField]
    InGameSceneBGScrollManager[] inGameSceneBGScrollManager;


    public bool waitForRaid = false;




    private void Start()
    {
        isCamGetDistance(true);
    }


    private void Update()
    {
        if (waitForRaid ==  true)
        {
            charMove();

            bgMove(true);
            isCamGetDistance(true);
        }
        else
        {
            ifBattleStart();
        }

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

        for (int i = 0; i< inGameSceneUiDataManager.playerObjList.Count; i++)
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

        for (int i = 0; i< inGameSceneUiDataManager.playerObjList.Count; i++)
        {
            inGameSceneCharSpineAniCon[i] = inGameSceneUiDataManager.playerObjList[i].GetComponentInChildren<InGameSceneCharSpineAniCon>();
            charState[i] = inGameSceneUiDataManager.playerObjList[i].GetComponentInChildren<CharState>();

            charState[i].nowState = CharState.NowState.isWalkToOrginPos;
            inGameSceneCharSpineAniCon[i].run();
        }
    }



    public void nextRaidStart()
    {
        waitForRaid = false;

        inGameSceneUiDataManager.battleSceneCount++;
        inGameSceneGameManager.setEnemyCharData();
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
}
