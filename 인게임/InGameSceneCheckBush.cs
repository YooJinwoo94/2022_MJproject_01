using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneCheckBush : MonoBehaviour
{
	public GameObject bush;

	[SerializeField]
	GameObject checkPos;

	public Vector2 boxSize;

	CharState charState;

    private void Start()
    {
		charState = gameObject.GetComponent<CharState>();
	}


    public bool isBushNear()
	{
		if (charState.nowState == CharState.NowState.isFindingBush) return true;
		
		Collider2D[] col = Physics2D.OverlapBoxAll(checkPos.transform.position, boxSize, 0.4f);
	
		foreach (Collider2D item in col)
		{
			switch(item.tag)
            {
				case "usingBush":
					//Debug.Log("부쉬에 동료가 있다! 여긴 못사용해!");
					return false;

				case "bush":
					Debug.Log("부쉬에 들어간다");

					item.tag = "usingBush";
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
