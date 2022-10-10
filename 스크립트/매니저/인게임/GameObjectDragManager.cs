using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;



public class GameObjectDragManager : MonoBehaviour
{
    private Transform topCanvas;               // 최상단의 Transform
    private Transform previousParent;       // 해당 오브젝트가 직전에 소속되어 있었던 부모 Transfron

    InGameScenePlayerCharGridData inGameScenePlayerCharGridData;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneGameManager inGameSceneGameManager;
    GameObject dragStartObj;
    Vector3 mouseDragStartPos;
    Vector3 spriteDragStartPos;

    GameObject colObj;

    private void Start()
    {
        inGameScenePlayerCharGridData = GameObject.Find("Manager").GetComponent<InGameScenePlayerCharGridData>();
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        inGameSceneGameManager = GameObject.Find("Manager").GetComponent<InGameSceneGameManager>();
        topCanvas = FindObjectOfType<GameObject>().transform;
    }





    private void OnMouseDown()
    {
        if (Input.GetMouseButton(1)) return;
        if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.canMoveCharBeforeBattle) return;

        mouseDragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPos = transform.position;

        // 드래그 직전에 소속되어 있던 부모 Transform 정보 저장
        previousParent = transform.parent.parent.parent;

        // 현재 드래그중인 UI가 화면의 최상단에 출력되도록 하기 위해
        transform.parent.parent.SetParent(topCanvas);       
        transform.parent.parent.SetAsLastSibling();

        colObj = null;
    }

    private void OnMouseDrag()
    {
        if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.canMoveCharBeforeBattle) return;
        if (Input.GetMouseButton(1)) return;

        transform.parent.parent.position = spriteDragStartPos + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPos );
    }

    private void OnMouseUp()
    {
        if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.canMoveCharBeforeBattle) return;
        if (Input.GetMouseButton(1)) return;

        if (Input.GetMouseButtonUp(0))
        {
            GameObject dragObj = this.gameObject.transform.parent.parent.gameObject;

            CharState charState = dragObj.transform.GetComponentInChildren<CharState>();

            switch (colObj)
            {
                // 다시 원위치로 돌아가야 하는 경우.
                case null:
                    goBackToOrginPos(dragObj);
                    break;
                   
                default:
                    int dropStartNum = Int32.Parse(colObj.gameObject.transform.name);
                    int dragStartNum = charState.sponPos;
  
                    inGameSceneGameManager.ifDragAndDrop(dragStartNum, dropStartNum, dragObj);
                    break;
            }
            colObj = null;
        }
    }


   public void goBackToOrginPos(GameObject dragObj)
    {
        int numa = Int32.Parse(previousParent.name);
        dragObj.transform.SetParent(previousParent);
        dragObj.transform.position = inGameSceneUiDataManager.playerCharMovePosBeforeBattle[numa].GetComponent<Transform>().position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (inGameSceneUiDataManager.nowGameSceneState != 
            InGameSceneUiDataManager.NowGameSceneState.canMoveCharBeforeBattle) return;

        if (collision.gameObject.transform.parent.name == "플래이어_진영2") colObj = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (inGameSceneUiDataManager.nowGameSceneState != 
            InGameSceneUiDataManager.NowGameSceneState.canMoveCharBeforeBattle) return;

        if (collision.gameObject.transform.parent.name == "플래이어_진영2") colObj = collision.gameObject;
    }
}
