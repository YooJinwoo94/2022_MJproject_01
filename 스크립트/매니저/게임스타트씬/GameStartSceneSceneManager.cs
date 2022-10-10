using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameStartSceneSceneManager : MonoBehaviour
{
    public void moveToPlayerChoiceBeforeBattleScene()
    {
        SceneManager.LoadScene("PlayerChoiceBeforeBattleScene");
    }
}
