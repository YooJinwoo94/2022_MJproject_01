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

        // ���� �ν��ӿ� ���°��� �Ϸ� �ߴ�.
        if (inGameSceneCharMove.checkCharIsInBush(inGameSceneCheckBush.bush) == true)
        {
            charState.nowState = CharState.NowState.isReadyForAttack;
            Debug.Log("���ⳡ !");
        }
    }
}
