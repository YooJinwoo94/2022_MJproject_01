using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InvenSceneClickRMouseBtnToLoveSocket : MonoBehaviour, IPointerClickHandler
{
    public GameObject list;

    InvenSceneDataManager invenSceneDataManager;

    private void Start()
    {
        invenSceneDataManager = GameObject.Find("Manager").GetComponentInChildren<InvenSceneDataManager>();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Right:

                if (invenSceneDataManager.countOfDetailUI.Count == 0)
                {
                    // uiâ ���ش�.
                    turnOnDetailUi();
                    return;
                }
                    if (invenSceneDataManager.countOfDetailUI[0] == list &&
                    invenSceneDataManager.countOfDetailUI.Count >= 1)
                {
                    Debug.Log("aac");

                    clearDetailUi();
                    return;
                }

                //�ٸ��� ���ֱ�
                if (invenSceneDataManager.countOfDetailUI.Count >= 1)
                {

                    invenSceneDataManager.countOfDetailUI[0].SetActive(false);
                    invenSceneDataManager.countOfDetailUI.RemoveAt(0);
                }

                // uiâ ���ش�.
                turnOnDetailUi();
                break;
        }
    }



    void clearDetailUi()
    {
        invenSceneDataManager.countOfDetailUI.Remove(list);
        list.SetActive(false);
    }
    void turnOnDetailUi()
    {
        invenSceneDataManager.countOfDetailUI.Add(list);
        list.SetActive(true);
    }
}
