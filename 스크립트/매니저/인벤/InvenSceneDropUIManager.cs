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

		charState = GameObject.Find("ĳ����").GetComponentInChildren<InvenSceneCharState>();
	}





	/// <summary>
	/// ���� ������ ���� ���� ���ο��� ����� ���� �� 1ȸ ȣ��
	/// </summary>
	public void OnDrop(PointerEventData eventData)
	{
		// pointerDrag�� ���� �巡���ϰ� �ִ� ���(=������)
		if (Input.GetMouseButtonUp(0))
		{
			if (eventData.pointerDrag == null) return;
			if (eventData.pointerDrag.tag == "Ui") return;

			checkAboutSocket(eventData);
		}
	}

	//�׳� ����� ��� 

	void checkAboutSocket(PointerEventData eventData)
    {
		invenSceneSceneManager = GameObject.Find("Manager").GetComponent<InvenSceneSceneManager>();

		dropSocketManager = GetComponentInChildren<InvenSceneLoveSocketManager>();
		dragScocketManager = eventData.pointerDrag.GetComponentInChildren<InvenSceneLoveSocketManager>();
		invenSceneDragUIManager = eventData.pointerDrag.GetComponentInChildren<InvenSceneDragUIManager>();

		if (dropSocketManager == null)
		{
			InvenSceneDropUIManager invenSceneDropUIManager = invenSceneDragUIManager.previousParent.gameObject.transform.GetComponentInChildren<InvenSceneDropUIManager>();

			if (invenSceneDropUIManager.locationOfThisLoveSocket == "ȣ���� ��������â")
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
			if (this.locationOfThisLoveSocket == "�κ�â") return;
			invenSceneSceneManager.noticeOn();
			invenSceneSceneManager.noticeOff();
			invenSceneDragUIManager.goBack();
			return;
		}

		// �巡���ϰ� �ִ� ����� �θ� ���� ������Ʈ�� �����ϰ�, ��ġ�� ���� ������Ʈ ��ġ�� �����ϰ� ����
		eventData.pointerDrag.transform.SetParent(transform);
		eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

		//��ġ�� ���� �� ����
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
