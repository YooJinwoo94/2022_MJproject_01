using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StageSceneUiDataManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject mapObj;
    [HideInInspector]
    public StageSceneThisNodeTreeData stageSceneThisNodeTreeData;

    public string[] clickedRandomEvent;
    [HideInInspector]
    public int clickedRandomEventCount;

    public GameObject eventObj;

    public GameObject clickUiObj;
    [HideInInspector]
    public string url = "https://docs.google.com/spreadsheets/d/145Y78rWQdw9Aa3Eupo_UOA8KWd8UGBZltyV88NwG1U8/export?format=tsv&range=A1:F7";



    private void Start()
    {
        clickedRandomEvent = new string[10];
    }
}
