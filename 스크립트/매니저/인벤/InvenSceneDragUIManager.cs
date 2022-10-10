using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InvenSceneDragUIManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	private Transform canvas;               // UI�� �ҼӵǾ� �ִ� �ֻ���� Canvas Transform
	[SerializeField]
	public Transform previousParent;       // �ش� ������Ʈ�� ������ �ҼӵǾ� �־��� �θ� Transfron
	[SerializeField]
	private RectTransform rect;             // UI ��ġ ��� ���� RectTransform
	[SerializeField]
	public CanvasGroup canvasGroup;        // UI�� ���İ��� ��ȣ�ۿ� ��� ���� CanvasGroup
	[SerializeField]
     GameObject dragStartObj;

	private void Start()
	{
		canvas = FindObjectOfType<Canvas>().transform;
		rect = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	/// <summary>
	/// ���� ������Ʈ�� �巡���ϱ� ������ �� 1ȸ ȣ��
	/// </summary>
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Input.GetMouseButton(1)) return;

		// �巡�� ������ �ҼӵǾ� �ִ� �θ� Transform ���� ����
		previousParent = transform.parent;

		// ���� �巡������ UI�� ȭ���� �ֻ�ܿ� ��µǵ��� �ϱ� ����
		transform.SetParent(canvas);        // �θ� ������Ʈ�� Canvas�� ����
		transform.SetAsLastSibling();       // ���� �տ� ���̵��� ������ �ڽ����� ����

		// �巡�� ������ ������Ʈ�� �ϳ��� �ƴ� �ڽĵ��� ������ ���� ���� ������ CanvasGroup���� ����
		//  ���� �浹ó���� ���� �ʵ��� �Ѵ�
		canvasGroupManager(false);
	}

	/// <summary>
	/// ���� ������Ʈ�� �巡�� ���� �� �� ������ ȣ��
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{
		if (Input.GetMouseButton(1)) return;
		// ���� ��ũ������ ���콺 ��ġ�� UI ��ġ�� ���� (UI�� ���콺�� �Ѿƴٴϴ� ����)
		rect.position = eventData.position;
	}

	/// <summary>
	/// ���� ������Ʈ�� �巡�׸� ������ �� 1ȸ ȣ��
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
		// �������� �ҼӵǾ��־��� previousParent�� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
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

