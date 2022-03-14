using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class DragUIManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Transform canvas;               // UI�� �ҼӵǾ� �ִ� �ֻ���� Canvas Transform
	private Transform previousParent;       // �ش� ������Ʈ�� ������ �ҼӵǾ� �־��� �θ� Transfron
	private RectTransform rect;             // UI ��ġ ��� ���� RectTransform
	private CanvasGroup canvasGroup;        // UI�� ���İ��� ��ȣ�ۿ� ��� ���� CanvasGroup

	[SerializeField]
	PlayerChoiceBeforeBattleSceneClickCharRMouseBtn playerChoiceBeforeBattleSceneClickCharRMouseBtn;

	PlayerChoiceBeforeBattleSceneSceneManager playerChoiceBeforeBattleSceneSceneManager;
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

		if (playerChoiceBeforeBattleSceneClickCharRMouseBtn.charDetailUIobj.activeInHierarchy == true)
        {
			playerChoiceBeforeBattleSceneClickCharRMouseBtn.charDetailUIobj.SetActive(false);
		}

		dragStartObj = EventSystem.current.currentSelectedGameObject.transform.gameObject;

		// �巡�� ������ �ҼӵǾ� �ִ� �θ� Transform ���� ����
		previousParent = transform.parent;

		// ���� �巡������ UI�� ȭ���� �ֻ�ܿ� ��µǵ��� �ϱ� ����
		transform.SetParent(canvas);        // �θ� ������Ʈ�� Canvas�� ����
		transform.SetAsLastSibling();       // ���� �տ� ���̵��� ������ �ڽ����� ����

		// �巡�� ������ ������Ʈ�� �ϳ��� �ƴ� �ڽĵ��� ������ ���� ���� ������ CanvasGroup���� ����
		//  ���� �浹ó���� ���� �ʵ��� �Ѵ�
		canvasGroup.blocksRaycasts = false;
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
			// �巡�׸� �����ϸ� �θ� canvas�� �����Ǳ� ������
			// �巡�׸� ������ �� �θ� canvas�̸� ������ ������ �ƴ� ������ ����
			// ����� �ߴٴ� ���̱� ������ �巡�� ������ �ҼӵǾ� �ִ� ������ �������� ������ �̵�

			if (transform.parent == canvas)
			{
				// �������� �ҼӵǾ��־��� previousParent�� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
				transform.SetParent(previousParent);
				rect.position = previousParent.GetComponent<RectTransform>().position;
			}
			else
			{
				playerChoiceBeforeBattleSceneSceneManager = GameObject.Find("SceneManager").GetComponent<PlayerChoiceBeforeBattleSceneSceneManager>();

				GameObject dropStartObj = this.gameObject.transform.parent.gameObject;

				int dropStartNum = Int32.Parse(dropStartObj.name);
				int dragStartNum = Int32.Parse(dragStartObj.name);

				playerChoiceBeforeBattleSceneSceneManager.ifDragAndDrop(dragStartNum, dropStartNum, this.gameObject.name);
			}
			// ���İ��� 1�� �����ϰ�, ���� �浹ó���� �ǵ��� �Ѵ�
			canvasGroup.alpha = 1.0f;
			canvasGroup.blocksRaycasts = true;
			return;
		}
	}
}
