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





    // 해당 캐릭터 누르면 발동되는 함수 
    public void OnPointerClick(PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Right:

                //같은거 누른경우
                if (playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI.Count >= 1 &&
                   charDetailUIobj == playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI[0])
                {
                    clearDetailUiList();
                    return;
                }

                //다른거 켜주기
                if (playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI.Count >= 1)
                {
                    clearDetailUiList();
                }
               
                // ui창 켜준다.
                playerChoiceBeforeBattleSceneUiDataManager.clickCharRMouseBtnDetailUI.Add(charDetailUIobj);
                playerCharRightClick();
                break;
        }
    }

    




    //캐릭터를 오른쪽 클릭한 경우 
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
