using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSceneDialogueManager : MonoBehaviour
{
    [SerializeField]
    StageSceneQuestManager stageSceneQuestManager;
    [SerializeField]
    TypingTextCon typingTextCon;
    [SerializeField]
    StageSceneTextManager stageSceneTextManager;
    [SerializeField]
    StageSceneUiDataManager stageSceneUiDataManager;


    public int count = 1;

    // Update is called once per frame
    void Update()
    {
        if (typingTextCon.textState == TextState.textStart || typingTextCon.textState == TextState.textReady) return;
        
        if (count == 
            stageSceneTextManager.randomEventText[stageSceneQuestManager.randomCount].ToString().Split('\n').GetLength(0))
        {

            stageSceneUiDataManager.clickUiObj.SetActive(true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || 
            Input.GetMouseButtonDown(0))
        {
            typingTextCon.typingTextStart(count);
            count++;
        }
    }
}
