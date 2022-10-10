using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;



public class GameObjectDragManager : MonoBehaviour
{
    private Transform topCanvas;               // �ֻ���� Transform
    private Transform previousParent;       // �ش� ������Ʈ�� ������ �ҼӵǾ� �־��� �θ� Transfron

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

        // �巡�� ������ �ҼӵǾ� �ִ� �θ� Transform ���� ����
        previousParent = transform.parent.parent.parent;

        // ���� �巡������ UI�� ȭ���� �ֻ�ܿ� ��µǵ��� �ϱ� ����
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
                // �ٽ� ����ġ�� ���ư��� �ϴ� ���.
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

        if (collision.gameObject.transform.parent.name == "�÷��̾�_����2") colObj = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (inGameSceneUiDataManager.nowGameSceneState != 
            InGameSceneUiDataManager.NowGameSceneState.canMoveCharBeforeBattle) return;

        if (collision.gameObject.transform.parent.name == "�÷��̾�_����2") colObj = collision.gameObject;
    }
}
