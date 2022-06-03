using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;




public class Move : Action
{
    InGameSceneCharMove inGameSceneCharMove;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;





    public override void OnStart()
    {
        inGameSceneCharMove = gameObject.GetComponent<InGameSceneCharMove>();
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
        inGameSceneCharSpineAniCon = gameObject.GetComponent<InGameSceneCharSpineAniCon>();
     
        switch (this.gameObject.tag)
        {
            case "playerChar":
                inGameSceneCharSpineAniCon.run();
                break;

            case "enemyChar":
                inGameSceneCharSpineAniCon.run();
                break;
        }
        inGameSceneCharMove.moveToEnemy();
    }

    public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}