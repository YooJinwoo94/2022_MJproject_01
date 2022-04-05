using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;


public class CheckEnemy : Conditional
{
     public InGameSceneUiDataManager inGameSceneUiDataManager;
     public SharedBool isEnemyOn;




    public override void OnStart()
    {
          if (inGameSceneUiDataManager == null)
           {
             inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
          }
    }


    public override TaskStatus OnUpdate()
	{

        switch (this.gameObject.transform.tag)
        {
            case "playerChar":
                // 현재 남아있는 아이가 없다! 
                if (inGameSceneUiDataManager.leftEnemyCount <= 0)
                {
                    isEnemyOn.Value = false;

                    return TaskStatus.Success;
                }
                else
                {
                    isEnemyOn.Value = true;

                    return TaskStatus.Success;
                }
                                                                                         
            case "enemyChar":
                // 현재 남아있는 아이가 없다! 
                if (inGameSceneUiDataManager.leftPlayerCount <= 0)
                {
                    isEnemyOn.Value = false;
                    return TaskStatus.Success;
                }              
                else 
                {
                    isEnemyOn.Value = true;
                    return TaskStatus.Success;
                }                                                     
        }
        return TaskStatus.Success;
    }
}