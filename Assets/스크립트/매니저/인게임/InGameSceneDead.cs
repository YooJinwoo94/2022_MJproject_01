using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneDead : MonoBehaviour
{
    [SerializeField]
    GameObject charObj;

    InGameSceneGameManager inGameSceneGameManager;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;

    private void Start()
    {
        inGameSceneGameManager = GameObject.Find("Manager").GetComponent<InGameSceneGameManager>();
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = gameObject.GetComponent<CharState>();

    }


    public void dead()
    {
        Debug.Log("Á×À½");
        charState.nowState = CharState.NowState.isDead;
       

        switch (this.gameObject.tag)
        {
            case "enemyChar":
                inGameSceneUiDataManager.enemyObjList.Remove(charObj);
                inGameSceneGameManager.ifDeadCheckLeftbattleSceneCount();
                break;

            case "playerChar":
                inGameSceneUiDataManager.playerObjList.Remove(charObj);
                break;
        }


        Destroy(charObj);
    }
}
