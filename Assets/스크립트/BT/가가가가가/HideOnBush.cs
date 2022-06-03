using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HideOnBush : Action
{
    CharState charState;
    InGameSceneCharMove inGameSceneCharMove;
    InGameSceneCheckBush inGameSceneCheckBush;


    public override void OnStart()
    {
        charState = this.gameObject.transform.GetComponent<CharState>();
        inGameSceneCharMove = gameObject.transform.GetComponent<InGameSceneCharMove>();
        inGameSceneCheckBush = gameObject.transform.GetComponent<InGameSceneCheckBush>();


        inGameSceneCharMove.moveToBush(inGameSceneCheckBush.bush);

        // 현재 부쉬속에 숨는것을 완료 했다.
        if (inGameSceneCharMove.checkCharIsInBush(inGameSceneCheckBush.bush) == true)
        {
            charState.nowState = CharState.NowState.isReadyForAttack;
            Debug.Log("숨기끝 !");
        }
    }
}
