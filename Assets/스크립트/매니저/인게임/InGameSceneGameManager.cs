using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InGameSceneGameManager : MonoBehaviour
{
    [SerializeField]
    PlayFabManager playFabManager;
    [SerializeField]
    InGameSceneUiDataManager inGameSceneUiDataManager;
    [SerializeField]
    InGameSceneIfRaidEnd inGameSceneIfRaidEnd;



    private void Start()
    {
        StartCoroutine("SettingStart");

        playFabManager.setPlayerAndEnemyData();
        setPlayerCharData();
    }

    IEnumerator SettingStart()
    {
        yield return new WaitForSeconds(1f);
        inGameSceneIfRaidEnd.isCamGetDistance(false);
        inGameSceneIfRaidEnd.bgMove(true);
    }



    public void ifDeadCheckLeftbattleSceneCount()
    {
         if (inGameSceneUiDataManager.battleSceneCount == 2) return;

         // 새로운 레이드가 시작될 예정입니다.
         if (inGameSceneUiDataManager.enemyObjList.Count == 0 && inGameSceneIfRaidEnd.waitForRaid == false)
          {
            inGameSceneIfRaidEnd.waitForRaid = true;
          }
    }









    //=========================================================================== string을 읽어서 몬스터 및 플레이어 캐릭터들을 활성화 시킨다.
    //playfab에서는 세로로 저장되기 때문에 반대로 읽어야 한다.
    //플레이어 캐릭터의 데이터를 가져온다.
    void setPlayerCharData()
    {
        for (int i = 0; i < PlayersCharGridDataManager.instance.nowGridData.GetLength(0); i++)
        {
            for (int k = 0; k < PlayersCharGridDataManager.instance.nowGridData.GetLength(1); k++)
            {
                if (PlayersCharGridDataManager.instance.nowGridData[i, k] == null)
                {
                    continue;
                }
                int num = 0;
                num = k * PlayersCharGridDataManager.instance.nowGridData.GetLength(0) + i;

                turnOnPlayerChar(num, PlayersCharGridDataManager.instance.nowGridData[i, k]);
            }
        }
    }
    //적 캐릭터의 데이터를 가져온다.
    //다음 inGameSceneUiDataManager.battleSceneCount로 되면 다시 호출해야 한다.
    public void setEnemyCharData()
    {
        for (int i = 0; i < inGameSceneUiDataManager.enemyCharInGrid.GetLength(1); i++)
        {
            for (int k = 0; k < inGameSceneUiDataManager.enemyCharInGrid.GetLength(2); k++)
            {
                if (inGameSceneUiDataManager.enemyCharInGrid[inGameSceneUiDataManager.battleSceneCount, i, k] == "null" ||
                    inGameSceneUiDataManager.enemyCharInGrid[inGameSceneUiDataManager.battleSceneCount, i, k] == null)
                {
                    continue;
                }
                int num = 0;
                num = k * inGameSceneUiDataManager.enemyCharInGrid.GetLength(1) + i;

                turnOnEnemyChar(num, inGameSceneUiDataManager.enemyCharInGrid[inGameSceneUiDataManager.battleSceneCount, i, k]);
            }
        }
    }

    void turnOnPlayerChar(int num, string name)
    {
        GameObject playerCharInGrid = Instantiate(inGameSceneUiDataManager.playerObj, inGameSceneUiDataManager.playerCharInGridPos[num].transform);
        playerCharInGrid.name = name;

        CharState charState = playerCharInGrid.transform.GetComponentInChildren<CharState>();
        charState.sponPos = num;

        inGameSceneUiDataManager.playerObjList.Add(playerCharInGrid);
    }
    void turnOnEnemyChar(int num, string name)
    {
        GameObject enemyChatObj = Instantiate(inGameSceneUiDataManager.enemyObj, inGameSceneUiDataManager.enemyCharInGridPos[num].transform);
        enemyChatObj.name = name;

        inGameSceneUiDataManager.enemyObjList.Add(enemyChatObj);
    }
}
