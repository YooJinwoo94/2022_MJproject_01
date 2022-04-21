using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneCheckBush : MonoBehaviour
{
	public GameObject bush;
	[SerializeField]
	GameObject checkPos;

	public Vector2 boxSize;

	public bool isBushNear()
	{
		Debug.Log("부쉬 확인하기");

		Collider2D[] col = Physics2D.OverlapBoxAll(checkPos.transform.position, boxSize, 0);

		foreach (Collider2D item in col)
		{
			if (item.tag == "usingBush")
            {
				Debug.Log("부쉬에 동료가 있다! 여긴 못사용해!");
				return false;
			}
			if (item.tag == "bush")
            {
				item.tag = "usingBush";
				Debug.Log("부쉬가 근처에 있다");
				bush = item.gameObject;
				return true;
			}
		}
		return false;
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(checkPos.transform.position, boxSize);
	}
}
