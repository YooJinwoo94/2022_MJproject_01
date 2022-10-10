using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class StageSceneTextManager : MonoBehaviour
{
    public TextAsset []randomEventText;
    [HideInInspector]
    public string[,] randomEvent;



    private void Start()
    {
        settingAboutRandomEvent();
    }

    // 랜덤 이벤트관련
    //====================================================================
    void settingAboutRandomEvent()
    {
        randomEvent = new string[10, 20];

        for (int i = 0; i< randomEventText.GetLength(0); i++)
        {
            randomEventTextOn(i);
        }
    }
    void randomEventTextOn(int randomEventCount)
    {
        for (int i = 0; i < randomEventText[randomEventCount].ToString().Split('\n').GetLength(0); i++)
        {
            randomEvent[randomEventCount, i] = randomEventText[randomEventCount].ToString().Split('\n')[i];
        }
    }
}
