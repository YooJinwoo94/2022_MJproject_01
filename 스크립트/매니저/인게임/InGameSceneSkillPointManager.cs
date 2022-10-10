using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimalist.Bar.Quantity;


public class InGameSceneSkillPointManager : MonoBehaviour
{
    InGameSceneUiDataManager inGameSceneUiDataManager;
    [SerializeField]
    CharState charState;




    private void Start()
    {
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponentInChildren<InGameSceneUiDataManager>();
        StartCoroutine("ManaUpByTime");
    }


    public void skillPointUpDown(string upDown, int point)
    {
       switch(upDown)
        {
            case "up":
                if (charState.skillPointBar.Amount >= 100) return;

                charState.skillPointBar.Amount =Mathf.Clamp(charState.skillPointBar.Amount += point, 0, 100);
                charState.skillPoint = Mathf.Clamp(charState.skillPoint += point, 0, 100);
                break;

            case "down":
                if (charState.skillPointBar.Amount <= 0) return;

                charState.skillPointBar.Amount = Mathf.Clamp(charState.skillPointBar.Amount -= point, 0, 100);
                charState.skillPoint = Mathf.Clamp(charState.skillPoint -= point, 0, 100);
                break;
        }
    }

    //
    //전투시작에만 작동.
    //살아있는경우에만 작동
    IEnumerator ManaUpByTime ()
    {
        while(true)
        {
            if(inGameSceneUiDataManager.nowGameSceneState == InGameSceneUiDataManager.NowGameSceneState.battleStart)
            {
                yield return new WaitForSeconds(1);
                skillPointUpDown("up", 5);
            }
            yield return null;
        }
    }
}
