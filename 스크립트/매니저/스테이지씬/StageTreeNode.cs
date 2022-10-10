using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageTreeNode : MonoBehaviour
{
	public StageTreeNode leftObj;
	public StageTreeNode midObj;
	public StageTreeNode rightObj;

	
	// 현재 노드 
	public string thisNodeName;
	[HideInInspector]
	public int thisNodeCount;

    public StageSceneTreeNodeManager.TreeNode bt;


	public int nodeLevelCount;

	StageSceneThisNodeTreeData stageSceneThisNodeTreeData;

	private void Start()
    {
		thisNodeCount = Int32.Parse(this.gameObject.name);
		bt  = new StageSceneTreeNodeManager.TreeNode(thisNodeName);

		putDataToNode();
	}


	public void putDataToNode()
    {
		bt.makeLeftSubTree(leftObj.bt);
		bt.makeRightSubTree(rightObj.bt);
		bt.makeMidSubTree(midObj.bt);

		stageSceneThisNodeTreeData = GameObject.FindWithTag("Map").transform.GetComponent<StageSceneThisNodeTreeData>();
		for (int i = 0; i < stageSceneThisNodeTreeData.tree.GetLength(0); i++)
		{
			for (int k = 0; k < stageSceneThisNodeTreeData.tree[i].thisLevelNode.GetLength(0); k++)
			{
				if (this.name == stageSceneThisNodeTreeData.tree[i].thisLevelNode[k].transform.gameObject.name)
				{
					nodeLevelCount = i;
				}
			}
		}
	}

}
