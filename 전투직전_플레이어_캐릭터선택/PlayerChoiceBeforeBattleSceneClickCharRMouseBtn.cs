using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;



public class PlayerChoiceBeforeBattleSceneClickCharRMouseBtn : MonoBehaviour, IPointerClickHandler
{
    PlayerChoiceBeforeBattleSceneUiClickManager playerChoiceBeforeBattleSceneUiClickManager;
    PlayerChoiceBeforeBattleSceneSceneManager playerChoiceBeforeBattleSceneSceneManager;
    PlayerChoiceBeforeBattleSceneUIMoveManager playerChoiceBeforeBattleSceneUIMoveManager;
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;

    public GameObject charDetailUIobj;

    string clickCharInInvenName = "";

    private void Start()
    {
        playerChoiceBeforeBattleSceneSceneManager = GameObject.Find("SceneManager").GetComponent<PlayerChoiceBeforeBattleSceneSceneManager>();

        playerChoiceBeforeBattleSceneUIMoveManager = GameObject.Find("ui_Move_Manager").GetComponent<PlayerChoiceBeforeBattleSceneUIMoveManager>();

        playerChoiceBeforeBattleSceneUiClickManager = GameObject.Find("ui_Click_Manager").GetComponent<PlayerChoiceBeforeBattleSceneUiClickManager>();

        playerChoiceBeforeBattleSceneUiDataManager = GameObject.Find("ui_Data_Manager").GetComponent<PlayerChoiceBeforeBattleSceneUiDataManager>();
    }





    // �ش� ĳ���� ������ �ߵ��Ǵ� �Լ� 
    public void OnPointerClick(PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Right:

                //������ �������
                if (playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI.Count >= 1 &&
                   charDetailUIobj == playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI[0])
                {
                    clearDetailUiList();
                    return;
                }

                //�ٸ��� ���ֱ�
                if (playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI.Count >= 1)
                {
                    clearDetailUiList();
                }
               
                // uiâ ���ش�.
                playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI.Add(charDetailUIobj);
                playerCharRightClick();
                break;
        }
    }

    




    //ĳ���͸� ������ Ŭ���� ��� 
     void playerCharRightClick()
    {
        charDetailUIobj.SetActive(true);
    }

    public void playerCharPressRightClickAndDeleteBtn()
    {
        clearDetailUiList();

        GameObject clickObj = EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.parent.parent.gameObject;

        playerChoiceBeforeBattleSceneUiClickManager.clickGridNum = Int32.Parse(clickObj.name);
        int[] gridXY = playerChoiceBeforeBattleSceneSceneManager.changeNumToGrid(playerChoiceBeforeBattleSceneUiClickManager.clickGridNum);
        playerChoiceBeforeBattleSceneSceneManager.outFromGrid(gridXY);
        playerChoiceBeforeBattleSceneSceneManager.putGridDataToPlayPabServer();
    }
    public void playerCharPressRightClickchangeBtn()
    {
        clearDetailUiList();

        GameObject clickObj = EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.parent.parent.gameObject;

        playerChoiceBeforeBattleSceneUiClickManager.clickGridNum = Int32.Parse(clickObj.name);
        clickCharInInvenName = EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.gameObject.name;

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOn();
    }
    public void playerCharPressRightClickDetailBtn()
    {
        clearDetailUiList();
    }

    void clearDetailUiList()
    {
        playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI[0].SetActive(false);
        playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI.Clear();
    }
}
