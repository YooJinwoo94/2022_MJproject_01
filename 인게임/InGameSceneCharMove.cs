using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneCharMove : MonoBehaviour
{
    [SerializeField]
    PathManager pathManager;
    [SerializeField]
    Transform moveForwardPos;
    [SerializeField]
    Transform charTransform;


    CharState charState;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    private void Start()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = gameObject.transform.GetComponent<CharState>();
      
    }

    public void moveToEnemy()
    {
        pathManager.pathFinding();
        if (pathManager.FinalNodeList.Count == 0) return;

        if (pathManager.FinalNodeList.Count == 1) return;
        Vector2 movePos = new Vector2Int(pathManager.FinalNodeList[1].x, pathManager.FinalNodeList[1].y);
        Vector2 currentPos = new Vector2Int(Mathf.RoundToInt(this.gameObject.transform.position.x), Mathf.RoundToInt(this.gameObject.transform.position.y));
       
       // if (currentPos == movePos) movePos = new Vector2(pathManager.FinalNodeList[1].x, pathManager.FinalNodeList[1].y);

        charTransform.position =    
            Vector2.MoveTowards(charTransform.position, movePos, 1f * Time.deltaTime);
    }

    public void moveForward()
    {
        charTransform.position =
          Vector2.MoveTowards(charTransform.position, moveForwardPos.position, 1f * Time.deltaTime);
    }


    public void moveToBush(GameObject bush)
    {
        charTransform.position =
            Vector2.MoveTowards(charTransform.position, bush.transform.position, 1f * Time.deltaTime);
    }

    public bool checkCharIsInBush(GameObject bush)
    {
        if (charTransform.position== bush.transform.position)
        {
            return true;
        }
        return false;
    }

}
