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
            case "����Ʈ ����":
               // randomEventCount();
                break;

            case "����":
                break;

            case "����":
               // randomEventCount();
                break;

            case "�̺�Ʈ":
                randomEventCount();

                if (stageSceneUiDataManager.eventObj.activeInHierarchy != true) stageSceneUiDataManager.eventObj.SetActive(true);
                //�ؽ�Ʈ�� �������� �����غ���
                typingTextCon.typingTextStart();
                break;

            case "����":
              //  randomEventCount();
                break;
        }
    }


    void randomEventCount()
    {
        randomCount = Random.Range(1, 10);

        if (stageSceneUiDataManager.clickedRandomEventCount == 10) return;

        while (stageSceneUiDataManager.clickedRandomEvent[randomCount] == "�̹� �������� �Դϴ�.")
        {
            randomCount = Random.Range(0, 10);
        }

        stageSceneUiDataManager.clickedRandomEvent[randomCount] = "�̹� �������� �Դϴ�.";
        stageSceneUiDataManager.clickedRandomEventCount++;
    }    
}
