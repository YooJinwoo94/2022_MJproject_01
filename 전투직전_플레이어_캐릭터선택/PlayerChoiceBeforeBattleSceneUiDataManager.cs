using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceBeforeBattleSceneUiDataManager : MonoBehaviour
{
    public GameObject[] playerCharInGridPosNum;
   public GameObject playerCharInGrid;
    [HideInInspector]
    public string[,] playerCharChoiceData = new string[3, 3];
   [HideInInspector]
   public int playerCharChoiceCount = 0;
}
