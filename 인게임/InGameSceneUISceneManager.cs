using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneUISceneManager : MonoBehaviour
{
    [SerializeField]
    PlayFabManager playFabManager;
    [SerializeField]
    InGameSceneUiDataManager inGameSceneUiDataManager;



    private void Start()
    {
        playFabManager.setPlayerData();

        setPlayerCharData();
    }








    //현재 구역의 몬스터의 수를 구하기
   public void checkMonsterCountInGrid()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                if (inGameSceneUiDataManager.enemyCharInGrid[inGameSceneUiDataManager.battleSceneCount, i, k] != null)
                {
                    inGameSceneUiDataManager.leftEnemyCount++;
                    continue;
                }
            }
        }
    }

    public void checkPlayerCountInGrid()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                if (PlayersCharGridDataManager.instance.nowGridData[i, k] != null)
                {
                    inGameSceneUiDataManager.leftPlayerCount++;
                    continue;
                }
            }
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
                if (inGameSceneUiDataManager.enemyCharInGrid[inGameSceneUiDataManager.battleSceneCount, i, k] == null)
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

        inGameSceneUiDataManager.playerObjList.Add(playerCharInGrid);
    }
    void turnOnEnemyChar(int num, string name)
    {
        GameObject enemyChatObj = Instantiate(inGameSceneUiDataManager.enemyObj, inGameSceneUiDataManager.enemyCharInGridPos[num].transform);
        enemyChatObj.name = name;

        inGameSceneUiDataManager.enemyObjList.Add(enemyChatObj);
    }
}
