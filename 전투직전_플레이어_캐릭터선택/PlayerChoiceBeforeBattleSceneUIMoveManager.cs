using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceBeforeBattleSceneUIMoveManager : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;




    private void Start()
    {
        if (obj.activeInHierarchy == true) obj.SetActive(false);
    }

    public void selectCharUiSetOn()
    {
        obj.SetActive(true);
    }
    public void selectCharUiSetOff()
    {
        obj.SetActive(false);
    }
}
