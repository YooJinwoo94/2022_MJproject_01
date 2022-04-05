using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;


public class PlayerChoiceBeforeBattleSceneUiClickManager : MonoBehaviour
{
    [SerializeField]
    PlayerChoiceBeforeBattleSceneSceneManager playerChoiceBeforeBattleSceneSceneManager;

    [SerializeField]
    PlayerChoiceBeforeBattleSceneUIMoveManager playerChoiceBeforeBattleSceneUIMoveManager;

    [HideInInspector]
    public int clickGridNum = 0;
    string clickCharInInvenName = "";



    private void Start()
    {
        if (playerChoiceBeforeBattleSceneSceneManager == null) playerChoiceBeforeBattleSceneSceneManager = GameObject.Find("SceneManager").GetComponent<PlayerChoiceBeforeBattleSceneSceneManager>();

        if (playerChoiceBeforeBattleSceneUIMoveManager == null) playerChoiceBeforeBattleSceneUIMoveManager = GameObject.Find("ui_Move_Manager").GetComponent<PlayerChoiceBeforeBattleSceneUIMoveManager>();
    }


    // 전투전 플레이어 캐릭터 선택씬에서 해당 그리드 칸을 선택한 경우
    public void playerBattleSceneClickPlayerChaArea()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        clickGridNum = Int32.Parse(clickObj.name);
        playerChoiceBeforeBattleSceneSceneManager.playerChoiceGrid();
    }

    // 전투전 플레이어 캐릭터 선택씬에서 인벤에 있는 캐릭을 눌렀을 경우
    public void playerInvenUiClickPlayerCha()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        clickCharInInvenName = clickObj.transform.parent.gameObject.name;
        playerChoiceBeforeBattleSceneSceneManager.playerChoiceInvenUiCharArea(clickGridNum , clickCharInInvenName);
    }

    //전투전 플레이어 캐릭터 선택씬에서 인벤종료 버튼을 누른 경우
    public void playerInvenUiClickTurnOff()
    {
        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
    }

    //전투전 플레이어 캐릭터 선택씬에서 해당 캐릭터 빼기 버튼을 눌렀을 경우
    public void playerInvenUiClickCharOffBtn()
    {
       int [] gridXY =  playerChoiceBeforeBattleSceneSceneManager.changeNumToGrid(clickGridNum);

        playerChoiceBeforeBattleSceneSceneManager.outFromGrid(gridXY);
        playerChoiceBeforeBattleSceneSceneManager.putGridDataToPlayPabServer();
    }


    public void clickToggleBtnOfGrid(int num)
    {
        playerChoiceBeforeBattleSceneUIMoveManager.clickToggleBtnOfGrid(num);

        playerChoiceBeforeBattleSceneSceneManager.nowGridNum = num;
        playerChoiceBeforeBattleSceneSceneManager.setGridDataFromServer();
    }

    public void moveToBattleScene()
    {
        SceneManager.LoadScene("InGameScene");
    }
}
