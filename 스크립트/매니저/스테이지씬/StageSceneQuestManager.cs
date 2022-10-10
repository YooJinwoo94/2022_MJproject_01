using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSceneQuestManager : MonoBehaviour
{
    [SerializeField]
    TypingTextCon typingTextCon;
    StageSceneTextManager stageSceneTextManager;
    [SerializeField]
    StageSceneUiDataManager stageSceneUiDataManager;

    [HideInInspector]
    public int randomCount;




    public void  getNodeNameAndSetRandomNum(string thisNodeName)
    {
        switch(thisNodeName)
        {
            case "엘리트 전투":
               // randomEventCount();
                break;

            case "보스":
                break;

            case "상점":
               // randomEventCount();
                break;

            case "이벤트":
                randomEventCount();

                if (stageSceneUiDataManager.eventObj.activeInHierarchy != true) stageSceneUiDataManager.eventObj.SetActive(true);
                //텍스트가 나오도록 구현해보자
                typingTextCon.typingTextStart();
                break;

            case "전투":
              //  randomEventCount();
                break;
        }
    }


    void randomEventCount()
    {
        randomCount = Random.Range(1, 10);

        if (stageSceneUiDataManager.clickedRandomEventCount == 10) return;

        while (stageSceneUiDataManager.clickedRandomEvent[randomCount] == "이미 눌린아이 입니다.")
        {
            randomCount = Random.Range(0, 10);
        }

        stageSceneUiDataManager.clickedRandomEvent[randomCount] = "이미 눌린아이 입니다.";
        stageSceneUiDataManager.clickedRandomEventCount++;
    }    
}
