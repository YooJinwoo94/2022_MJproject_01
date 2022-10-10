using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartSceneUIClickManager : MonoBehaviour
{
    [SerializeField]
    GameStartSceneSceneManager gameStartSceneSceneManager;
    [SerializeField]
    SaveLoadDataManager saveLoadDataManager;


    delegate void MyDelegate();
    MyDelegate myDelegate;


    public void gameStart()
    {
        myDelegate = delegate () { saveLoadDataManager.loadPlayerData();  };
        myDelegate += delegate () { gameStartSceneSceneManager.moveToPlayerChoiceBeforeBattleScene(); };


        myDelegate();
    }



    public void cascas1t()
    {
       // saveLoadDataManager.saveData();
    }
}
