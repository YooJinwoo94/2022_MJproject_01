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

    public GameObject charDetailUIobj;

    string clickCharInInvenName = "";

    private void Start()
    {
        if (playerChoiceBeforeBattleSceneSceneManager == null) playerChoiceBeforeBattleSceneSceneManager = GameObject.Find("SceneManager").GetComponent<PlayerChoiceBeforeBattleSceneSceneManager>();

        if (playerChoiceBeforeBattleSceneUIMoveManager == null) playerChoiceBeforeBattleSceneUIMoveManager = GameObject.Find("ui_Move_Manager").GetComponent<PlayerChoiceBeforeBattleSceneUIMoveManager>();

        if (playerChoiceBeforeBattleSceneUiClickManager == null) playerChoiceBeforeBattleSceneUiClickManager = GameObject.Find("ui_Click_Manager").GetComponent<PlayerChoiceBeforeBattleSceneUiClickManager>();
    }






    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            playerCharRightClick();
        }
    }






    //캐릭터를 오른쪽 클릭한 경우 
     void playerCharRightClick()
    {
        if (charDetailUIobj.activeInHierarchy == true) charDetailUIobj.SetActive(false);
        else charDetailUIobj.SetActive(true);
    }

    public void playerCharPressRightClickAndDeleteBtn()
    {
        if (charDetailUIobj.activeInHierarchy == true) charDetailUIobj.SetActive(false);

        GameObject clickObj = EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.parent.parent.gameObject;

        playerChoiceBeforeBattleSceneUiClickManager.clickGridNum = Int32.Parse(clickObj.name);
        int[] gridXY = playerChoiceBeforeBattleSceneSceneManager.changeNumToGrid(playerChoiceBeforeBattleSceneUiClickManager.clickGridNum);
        playerChoiceBeforeBattleSceneSceneManager.outFromGrid(gridXY);
        playerChoiceBeforeBattleSceneSceneManager.putGridDataToPlayPabServer();
    }
    public void playerCharPressRightClickchangeBtn()
    {
        if (charDetailUIobj.activeInHierarchy == true) charDetailUIobj.SetActive(false);

        GameObject clickObj = EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.parent.parent.gameObject;

        playerChoiceBeforeBattleSceneUiClickManager.clickGridNum = Int32.Parse(clickObj.name);
        clickCharInInvenName = EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.gameObject.name;

        playerChoiceBeforeBattleSceneUIMoveManager.selectCharUiSetOn();
    }
    public void playerCharPressRightClickDetailBtn()
    {
        if (charDetailUIobj.activeInHierarchy == true) charDetailUIobj.SetActive(false);
    }
}
