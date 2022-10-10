using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InvenSceneDragUIManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	private Transform canvas;               // UI가 소속되어 있는 최상단의 Canvas Transform
	[SerializeField]
	public Transform previousParent;       // 해당 오브젝트가 직전에 소속되어 있었던 부모 Transfron
	[SerializeField]
	private RectTransform rect;             // UI 위치 제어를 위한 RectTransform
	[SerializeField]
	public CanvasGroup canvasGroup;        // UI의 알파값과 상호작용 제어를 위한 CanvasGroup
	[SerializeField]
     GameObject dragStartObj;

	private void Start()
	{
		canvas = FindObjectOfType<Canvas>().transform;
		rect = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	/// <summary>
	/// 현재 오브젝트를 드래그하기 시작할 때 1회 호출
	/// </summary>
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Input.GetMouseButton(1)) return;

		// 드래그 직전에 소속되어 있던 부모 Transform 정보 저장
		previousParent = transform.parent;

		// 현재 드래그중인 UI가 화면의 최상단에 출력되도록 하기 위해
		transform.SetParent(canvas);        // 부모 오브젝트를 Canvas로 설정
		transform.SetAsLastSibling();       // 가장 앞에 보이도록 마지막 자식으로 설정

		// 드래그 가능한 오브젝트가 하나가 아닌 자식들을 가지고 있을 수도 때문에 CanvasGroup으로 통제
		//  광선 충돌처리가 되지 않도록 한다
		canvasGroupManager(false);
	}

	/// <summary>
	/// 현재 오브젝트를 드래그 중일 때 매 프레임 호출
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{
		if (Input.GetMouseButton(1)) return;
		// 현재 스크린상의 마우스 위치를 UI 위치로 설정 (UI가 마우스를 쫓아다니는 상태)
		rect.position = eventData.position;
	}

	/// <summary>
	/// 현재 오브젝트의 드래그를 종료할 때 1회 호출
	/// </summary>
	public void OnEndDrag(PointerEventData eventData)
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (transform.parent == canvas)
			{
				goBack();
			}
			canvasGroupManager(true);
			return;
		}
	}


	public void goBack()
    {
		// 마지막에 소속되어있었던 previousParent의 자식으로 설정하고, 해당 위치로 설정
		transform.SetParent(previousParent);
		rect.position = previousParent.GetComponent<RectTransform>().position;

		canvasGroupManager(true);
	}

	public void canvasGroupManager(bool blockRayCast)
    {
		switch(blockRayCast)
        {
			case true:
				canvasGroup.alpha = 1.0f;
				canvasGroup.blocksRaycasts = blockRayCast;
				break;

			case false:
				canvasGroup.alpha = 0.6f;
				canvasGroup.blocksRaycasts = blockRayCast;
				break;
		}
	}
}

