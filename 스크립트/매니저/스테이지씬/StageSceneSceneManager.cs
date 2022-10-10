using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSceneSceneManager : MonoBehaviour
{
    [SerializeField]
    StageSceneUiDataManager stageSceneUiDataManager;



    public void settingDataAboutMapTreeNode ()
    {
        //  stageSceneUiDataManager.mapObj
        stageSceneUiDataManager.stageSceneThisNodeTreeData = GameObject.FindWithTag("Map").transform.GetComponent<StageSceneThisNodeTreeData>();
    }
}


