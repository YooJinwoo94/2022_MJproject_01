using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneDead : MonoBehaviour
{
    [SerializeField]
    GameObject charHpSkillPointBarObj;
    [SerializeField]
    GameObject charObj;

    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    InGameSceneGameManager inGameSceneGameManager;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;

    private void Start()
    {
        inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();
        inGameSceneGameManager = GameObject.Find("Manager").GetComponent<InGameSceneGameManager>();
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = gameObject.GetComponent<CharState>();
    }


    public void dead()
    {    
        if (charState.nowState == CharState.NowState.isDead) return;

        switch (this.gameObject.tag)
        {
            case "enemyChar":
                inGameSceneCharSpineAniCon.die();
                inGameSceneUiDataManager.enemyObjList.Remove(charObj);
                inGameSceneGameManager.ifEnemyCharDeadCheckLeftbattleSceneCount();
                break;

            case "playerChar":
                inGameSceneUiDataManager.playerObjList.Remove(charObj);
                inGameSceneCharSpineAniCon.die();
                inGameSceneGameManager.ifPlayerCharDeadCheckLeftPlayerCharCount();
                break;
        }
        charState.nowState = CharState.NowState.isDead;

        Destroy(charHpSkillPointBarObj);
        Invoke("deleteChar", 1.5f);
    }

    void deleteChar()
    {
        Destroy(charObj);
    }
}
