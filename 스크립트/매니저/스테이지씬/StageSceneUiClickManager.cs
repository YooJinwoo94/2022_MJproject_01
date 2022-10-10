using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSceneUiClickManager : MonoBehaviour
{
	[SerializeField]
	StageSceneDialogueManager stageSceneDialogueManager;
	[SerializeField]
	StageSceneSceneManager stageSceneSceneManager;
	[SerializeField]
	StageSceneUiDataManager stageSceneUiDataManager;
	[SerializeField]
	StageSceneQuestManager stageSceneQuestManager;
	[SerializeField]
	TypingTextCon typingTextCon;
	[SerializeField]
	StageSceneTextManager stageSceneTextManager;


	public void nodeClick()
	{
		stageSceneSceneManager.settingDataAboutMapTreeNode();
		string clickObjName = EventSystem.current.currentSelectedGameObject.name;
		StageTreeNode stageTreeNode = stageSceneUiDataManager.stageSceneThisNodeTreeData.thisTreeObj.transform.GetChild(Int32.Parse(clickObjName)).GetComponentInChildren<StageTreeNode>();

		if (stageTreeNode.leftObj.thisNodeCount != stageTreeNode.thisNodeCount)
		{
			int num = stageTreeNode.leftObj.thisNodeCount;

			Button btn = stageSceneUiDataManager.stageSceneThisNodeTreeData.thisNodeObjSet[num].transform.GetComponentInChildren<Button>();

			btn.interactable = true;
		}
		if (stageTreeNode.rightObj.thisNodeCount != stageTreeNode.thisNodeCount)
		{
			int num = stageTreeNode.rightObj.thisNodeCount;

			Button btn = stageSceneUiDataManager.stageSceneThisNodeTreeData.thisNodeObjSet[num].transform.GetComponentInChildren<Button>();
			
			btn.interactable = true;
		}
		if (stageTreeNode.midObj.thisNodeCount != stageTreeNode.thisNodeCount)
		{
			int num = stageTreeNode.midObj.thisNodeCount;

			Button btn = stageSceneUiDataManager.stageSceneThisNodeTreeData.thisNodeObjSet[num].transform.GetComponentInChildren<Button>();

			btn.interactable = true;
		}
		
		//������ ������ ��Ȱ��ȭ ���� �Ѵ�.
		makeCantUseBtn(EventSystem.current.currentSelectedGameObject);

		//���� ��带 �����غ���
		stageSceneQuestManager.getNodeNameAndSetRandomNum(stageTreeNode.bt.thisNodeName);
	}

	public void eventClick(int num )
    {
		stageSceneDialogueManager.count = 1;
		typingTextCon.textState = TextState.textReady;

		stageSceneUiDataManager.clickUiObj.SetActive(false);
		stageSceneUiDataManager.eventObj.SetActive(false);

		Debug.Log("aa");
		Debug.Log(stageSceneDialogueManager.count);
	}



	//�ٸ� ���̸� ���������� ������ ���̸� ��Ȱ��ȭ? ���Ѿ� �ڴ� 
	//�׷��� �������� ����
	void makeCantUseBtn(GameObject clickObj)
	{
		StageSceneThisNodeTreeData stageSceneThisNodeTreeData = GameObject.FindWithTag("Map").GetComponent<StageSceneThisNodeTreeData>();

		StageTreeNode stageTreeNode = clickObj.GetComponent<StageTreeNode>();


		for (int i =0; i< stageSceneThisNodeTreeData.tree.GetLength(0); i++)
        {
			if (i != stageTreeNode.nodeLevelCount) continue;

			for (int k = 0; k < stageSceneThisNodeTreeData.tree[i].thisLevelNode.GetLength(0); k++)
			{
				if (stageSceneThisNodeTreeData.tree[i].thisLevelNode[k].transform.name == clickObj.name) continue;

				Button btn = stageSceneThisNodeTreeData.tree[i].thisLevelNode[k].GetComponent<Button>();
				btn.interactable = false;
			}
		}
	}
}
