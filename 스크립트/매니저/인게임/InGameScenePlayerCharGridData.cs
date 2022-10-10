using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScenePlayerCharGridData : MonoBehaviour
{
    [HideInInspector]
    public string[,] nowGridData = new string[3, 3];


    private void Start()
    {
        nowGridData = PlayerDataJsonManager.instance.playerData.charGridData.nowGridData;
    }
}
