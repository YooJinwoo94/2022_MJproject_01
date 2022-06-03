using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerChoiceBeforeBattleSceneUIMoveManager : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;
    [SerializeField]
    PlayerChoiceBeforeBattleSceneSceneManager playerChoiceBeforeBattleSceneSceneManager;

    [SerializeField]
    Image[] gridToggleImage;
    [SerializeField]
    Button[] gridToggleBtn;

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



    public void clickToggleBtnOfGrid(int num)
    {
        for (int i = 0; i < 4; i++)
        {
            if ( i == num)
            {
                gridToggleBtn[i].interactable = false;
                gridToggleImage[i].color = new Color(255, 0, 0, 255);
            }
           else
            {
                gridToggleBtn[i].interactable = true;
                gridToggleImage[i].color = new Color(0, 255, 255, 255);
            }
        }
    }
}
