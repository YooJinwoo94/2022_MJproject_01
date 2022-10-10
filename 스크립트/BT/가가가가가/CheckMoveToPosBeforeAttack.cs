using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CheckMoveToPosBeforeAttack : Conditional
{
    CharState charState;
    InGameSceneUiDataManager inGameSceneUiDataManager;
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    InGameSceneCharMove inGameSceneCharMove;
    InGameSceneGameManager inGameSceneGameManager;

    public override void OnStart()
    {
        inGameSceneGameManager = GameObject.Find("Manager").gameObject.GetComponentInChildren<InGameSceneGameManager>();
        charState = this.gameObject.transform.GetComponentInChildren<CharState>();
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponentInChildren<InGameSceneUiDataManager>();
        inGameSceneCharMove = gameObject.GetComponentInChildren<InGameSceneCharMove>();
        inGameSceneCharSpineAniCon = gameObject.GetComponentInChildren<InGameSceneCharSpineAniCon>();
    }



    public override TaskStatus OnUpdate()
    {
        if (charState.nowState == CharState.NowState.isWaitForBattleAndInAttackPos) return TaskStatus.Failure;

        switch(this.gameObject.tag)
        {
            case "playerChar":
                if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.cutScene_playerCharWalkIn) return TaskStatus.Failure;

                switch(inGameSceneCharMove.IsArrivedInAttackPos(this.gameObject.tag))
                {
                    case true:
                        //움직이는 배경을 꺼준다.
                        inGameSceneGameManager.bgMove(false);

                        inGameSceneCharSpineAniCon.idle();
                        inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos += 1;
                        charState.nowState = CharState.NowState.isWaitForBattleAndInAttackPos;

                        //만약 다들 도착한 상태라면
                        if (inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos == inGameSceneUiDataManager.playerObjList.Count)
                        {
                            //적 캐릭터 활성화
                            inGameSceneGameManager.setEnemyCharData();
                            //이제 적이 들어올 시간입니다.
                            inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.cutScene_enemyCharWalkIn;
                        }
                        return TaskStatus.Failure;
                }
                break;

            case "enemyChar":
                if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.cutScene_enemyCharWalkIn) return TaskStatus.Failure;

                switch (inGameSceneCharMove.IsArrivedInAttackPos(this.gameObject.tag))
                {
                    case true:
                        inGameSceneCharSpineAniCon.idle();
                        inGameSceneUiDataManager.countEnemyCharArrivedToAttackPos += 1;
                        charState.nowState = CharState.NowState.isWaitForBattleAndInAttackPos;

                        //만약 다들 도착한 상태라면
                        if (inGameSceneUiDataManager.countEnemyCharArrivedToAttackPos == inGameSceneUiDataManager.enemyObjList.Count)
                        {
                            //전투 버튼 활성화 시키기
                            inGameSceneGameManager.battleStartBtn.SetActive(true);

                            //전투 직전으로 enum을 변경해준다.
                            inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.canMoveCharBeforeBattle;
                        }
                        return TaskStatus.Failure;
                }
                break;
        }

        return TaskStatus.Success;
    }
}
