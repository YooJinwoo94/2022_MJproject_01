using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSceneTreeNodeManager : MonoBehaviour
{
	[System.Serializable]
	public class TreeNode
	{
		private TreeNode mid;
		private TreeNode left;
		private TreeNode right;
		public string thisNodeName;

		public TreeNode(string name)
		{
			mid = null;
			left = null;
			right = null;
			thisNodeName = name;
		}



		
		public void makeMidSubNull()
		{
			this.thisNodeName = null;
		}
		public void makeLeftSubNull()
		{
			this.thisNodeName = null;
		}
		public void makeRightSubNull()
		{
			this.thisNodeName = null;
		}
		

		public void makeLeftSubTree(TreeNode sub)
		{
			this.left = sub;
		}
		public void makeRightSubTree(TreeNode sub)
		{
			this.right = sub;
		}
		public void makeMidSubTree(TreeNode sub)
		{
			this.mid = sub;
		}




		public string getData()
		{
			return this.thisNodeName;					
		}
		public TreeNode getLeftSubTree()
		{
			return this.left;
		}
		public TreeNode getRightSubTree()
		{
			return this.right;
		}
	}
}
