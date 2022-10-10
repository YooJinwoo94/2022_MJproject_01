using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[HideInInspector]
public enum TextState
{
    textReady,
    textStart,
    textFin
}

public class TypingTextCon : MonoBehaviour
{
    [SerializeField]
    StageSceneQuestManager stageSceneQuestManager;
    [SerializeField]
    StageSceneTextManager stageSceneTextManager;
    [SerializeField]
    public Text text;
    [HideInInspector]
    public TextState textState = TextState.textReady;




    public void typingTextStart(int num = 0)
    {
         StartCoroutine(typingText(num));
    }

    IEnumerator typingText(int num = 0)
    {
        textState = TextState.textStart;

        for (int i = 0; i <= stageSceneTextManager.randomEvent[stageSceneQuestManager.randomCount, num].Length; i++)
        {
            text.text = null;
            text.text = stageSceneTextManager.randomEvent[stageSceneQuestManager.randomCount, num].Substring(0, i);

            yield return new WaitForSeconds(0.05f);
        }

        textState = TextState.textFin;
        StopCoroutine(typingText());
    }
}
