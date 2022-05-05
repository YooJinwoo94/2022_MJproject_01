using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneDead : MonoBehaviour
{
    [SerializeField]
    GameObject charObj;

    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;

    private void Start()
    {
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        inGameSceneCheckTargetAndGetDistance = gameObject.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = gameObject.GetComponent<CharState>();

    }


    public void dead()
    {
        Debug.Log("Á×À½");

        charState.nowState = CharState.NowState.isDead;
        inGameSceneUiDataManager.enemyObjList.Remove(charObj);
        Destroy(charObj);
    }
}
