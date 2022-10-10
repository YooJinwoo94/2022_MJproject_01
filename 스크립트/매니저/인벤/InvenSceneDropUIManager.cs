using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InvenSceneDropUIManager : MonoBehaviour, IDropHandler
{
	InvenSceneSocketState invenSceneSocketState;
	InvenSceneCharState charState;
	public string locationOfThisLoveSocket;
	InvenSceneDragUIManager invenSceneDragUIManager;
	InvenSceneLoveSocketManager dropSocketManager;
	InvenSceneLoveSocketManager dragScocketManager;
	InvenSceneSceneManager invenSceneSceneManager;
	private RectTransform rect;

	private void Start()
	{
		rect = GetComponent<RectTransform>();

		charState = GameObject.Find("캐릭터").GetComponentInChildren<InvenSceneCharState>();
	}





	/// <summary>
	/// 현재 아이템 슬롯 영역 내부에서 드롭을 했을 때 1회 호출
	/// </summary>
	public void OnDrop(PointerEventData eventData)
	{
		// pointerDrag는 현재 드래그하고 있는 대상(=아이템)
		if (Input.GetMouseButtonUp(0))
		{
			if (eventData.pointerDrag == null) return;
			if (eventData.pointerDrag.tag == "Ui") return;

			checkAboutSocket(eventData);
		}
	}

	//그냥 드랍한 경우 

	void checkAboutSocket(PointerEventData eventData)
    {
		invenSceneSceneManager = GameObject.Find("Manager").GetComponent<InvenSceneSceneManager>();

		dropSocketManager = GetComponentInChildren<InvenSceneLoveSocketManager>();
		dragScocketManager = eventData.pointerDrag.GetComponentInChildren<InvenSceneLoveSocketManager>();
		invenSceneDragUIManager = eventData.pointerDrag.GetComponentInChildren<InvenSceneDragUIManager>();

		if (dropSocketManager == null)
		{
			InvenSceneDropUIManager invenSceneDropUIManager = invenSceneDragUIManager.previousParent.gameObject.transform.GetComponentInChildren<InvenSceneDropUIManager>();

			if (invenSceneDropUIManager.locationOfThisLoveSocket == "호감도 소켓장착창")
            {
				giveSocketDataToChar(false, dragScocketManager.thisLoveSocketNum, eventData);
				invenSceneSceneManager.setCharData();
			}

			eventData.pointerDrag.transform.SetParent(transform);
			eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
			return;
        }

		if (dragScocketManager.thisLoveSocketNum != dropSocketManager.thisLoveSocketNum)
        {
			if (this.locationOfThisLoveSocket == "인벤창") return;
			invenSceneSceneManager.noticeOn();
			invenSceneSceneManager.noticeOff();
			invenSceneDragUIManager.goBack();
			return;
		}

		// 드래그하고 있는 대상의 부모를 현재 오브젝트로 설정하고, 위치를 현재 오브젝트 위치와 동일하게 설정
		eventData.pointerDrag.transform.SetParent(transform);
		eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

		//수치값 수정 및 조정
		giveSocketDataToChar(true, dropSocketManager.thisLoveSocketNum, eventData);
		invenSceneSceneManager.setCharData();

		invenSceneDragUIManager.canvasGroupManager(true);
	}

	void giveSocketDataToChar(bool isDataOn,int num, PointerEventData eventData = null)
    {
		switch (isDataOn)
        {
			case true:
				invenSceneSocketState = eventData.pointerDrag.GetComponentInChildren<InvenSceneSocketState>();
				charState.nameOfSocket[num] = invenSceneSocketState.thisSocketName;
				break;

			case false:
				charState.nameOfSocket[num] = null;
				break;
        }
	}
}
