using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InGameSceneUiClickManager : MonoBehaviour
{
    [SerializeField]
    InGameSceneUiDataManager inGameSceneUiDataManager;
    [SerializeField]
    InGameSceneGameManager inGameSceneGameManager;

    public void pressBattleStart()
    {
        inGameSceneGameManager.battleStart();
    }

    public void pressIngameSpeedConBtn()
    {
        inGameSceneGameManager.changeIngameSpeed();
    }
}
