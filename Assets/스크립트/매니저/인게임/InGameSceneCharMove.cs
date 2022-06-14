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
    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;

    InGameSceneUiDataManager inGameSceneUiDataManager;
    CharState charState;


    private void Start()
    {
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = gameObject.transform.GetComponent<CharState>();
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
    }

    public void moveToEnemy()
    {
        pathManager.pathFinding();
        if (pathManager.FinalNodeList.Count == 0) return;

         if (pathManager.FinalNodeList.Count == 1) return;
        Vector2 movePos = new Vector2Int(pathManager.FinalNodeList[1].x, pathManager.FinalNodeList[1].y);
       
        charTransform.position = Vector2.MoveTowards(charTransform.position, movePos, charState.moveSpeed * Time.deltaTime);
    }

    public void moveToAttackPos(string thisCharName)
    {
        switch (thisCharName)
        {
            case "playerChar":
                charTransform.position =
         Vector2.MoveTowards(charTransform.position,
         inGameSceneUiDataManager.playerCharMovePosBeforeBattle[charState.sponPos].transform.position, charState.moveSpeed * Time.deltaTime);
                break;

            case "enemyChar":
                charTransform.position =
          Vector2.MoveTowards(charTransform.position,
          inGameSceneUiDataManager.enemyCharMovePosBeforeBattle[charState.sponPos].transform.position, charState.moveSpeed * Time.deltaTime);
                break;
        }     
    }

    //애들이 스폰하고 위치로 도착했는지 체크하기
    public bool IsArrivedInAttackPos(string thisCharName)
    {
        switch (thisCharName)
        {
            case "playerChar":
                if (charTransform.position == 
                    inGameSceneUiDataManager.playerCharMovePosBeforeBattle[charState.sponPos].transform.position) return true;
                break;

            case "enemyChar":
                if (charTransform.position ==
                   inGameSceneUiDataManager.enemyCharMovePosBeforeBattle[charState.sponPos].transform.position) return true;
                break;
        }
      
        return false;
    }


    public void moveToBush(GameObject bush)
    {
        charTransform.position =
            Vector2.MoveTowards(charTransform.position, bush.transform.position, charState.moveSpeed * Time.deltaTime);
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
