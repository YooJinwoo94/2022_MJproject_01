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
                        //�����̴� ����� ���ش�.
                        inGameSceneGameManager.bgMove(false);

                        inGameSceneCharSpineAniCon.idle();
                        inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos += 1;
                        charState.nowState = CharState.NowState.isWaitForBattleAndInAttackPos;

                        //���� �ٵ� ������ ���¶��
                        if (inGameSceneUiDataManager.countPlayerCharArrivedToAttackPos == inGameSceneUiDataManager.playerObjList.Count)
                        {
                            //�� ĳ���� Ȱ��ȭ
                            inGameSceneGameManager.setEnemyCharData();
                            //���� ���� ���� �ð��Դϴ�.
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

                        //���� �ٵ� ������ ���¶��
                        if (inGameSceneUiDataManager.countEnemyCharArrivedToAttackPos == inGameSceneUiDataManager.enemyObjList.Count)
                        {
                            //���� ��ư Ȱ��ȭ ��Ű��
                            inGameSceneGameManager.battleStartBtn.SetActive(true);

                            //���� �������� enum�� �������ش�.
                            inGameSceneUiDataManager.nowGameSceneState = InGameSceneUiDataManager.NowGameSceneState.canMoveCharBeforeBattle;
                        }
                        return TaskStatus.Failure;
                }
                break;
        }

        return TaskStatus.Success;
    }
}
