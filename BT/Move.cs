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

        inGameSceneCharSpineAniCon.run();
        if (inGameSceneUiDataManager.enemyObjList.Count == 0)
        {
            inGameSceneCharMove.moveForward();
            Debug.Log("그냥 앞으로 걷기");

            return;
        }
        switch (this.gameObject.tag)
        {
            case "playerChar":
                inGameSceneCharMove.moveToEnemy(1);
                break;

            case "enemyChar":
                inGameSceneCharMove.moveToEnemy(-1);
                break;
        }
    }

    public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}