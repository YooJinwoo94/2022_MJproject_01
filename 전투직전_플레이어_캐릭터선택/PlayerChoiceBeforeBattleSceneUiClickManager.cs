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


    // ������ �÷��̾� ĳ���� ���þ����� �ش� �׸��� ĭ�� ������ ���
    public void playerBattleSceneClickPlayerChaArea()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        clickGridNum = Int32.Parse(clickObj.name);
        playerChoiceBeforeBattleSceneSceneManager.playerChoiceGrid();
    }

    // ������ �÷��̾� ĳ���� ���þ����� �κ��� �ִ� ĳ���� ������ ���
    public void playerInvenUiClickPlayerCha()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        clickCharInInvenName = clickObj.transform.parent.gameObject.name;
        playerChoiceBeforeBattleSceneSceneManager.playerChoiceInvenUiCharArea(clickGridNum , clickCharInInvenName);
    }

    //������ �÷��̾� ĳ���� ���þ����� �κ����� ��ư�� ���� ���
    public void playerInvenUiClickTurnOff()
    {
        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOff();
    }

    //������ �÷��̾� ĳ���� ���þ����� �ش� ĳ���� ���� ��ư�� ������ ���
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
