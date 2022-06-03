using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;



//�Լ��� �Ϲ������δ� ������ �ִ� ���� Ž�� 
// colliderbox�� ��ġ�Ͽ� ã�� 
// �ش� �Ÿ� ����ϱ� 
// �ش� �Ÿ����� ���� �̳��̸� ���� �ൿ
// �ƴϸ� �ش� ��ұ��� �̵� �ൿ
// ���� ���� ��ã���� idle �ൿ


// ���Ŀ� �� �� ����
// ���ݷ� ���� ���� 
// ���� ���� ���� 
// ������ �� ���� 
// 


public class CanNormalAttack : Conditional
{
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    CharState charState;
    InGameSceneUiDataManager inGameSceneUiDataManager;




    public override void OnStart()
    {
        inGameSceneUiDataManager = GameObject.Find("Manager").gameObject.GetComponent<InGameSceneUiDataManager>();
        inGameSceneCheckTargetAndGetDistance = gameObject.transform.GetComponent<InGameSceneCheckTargetAndGetDistance>();
        charState = this.gameObject.transform.GetComponent<CharState>();
    }


    public override TaskStatus OnUpdate()
    {
        if (inGameSceneUiDataManager.isBattleStart == false) return TaskStatus.Failure;
        if (charState.nowState != CharState.NowState.isReadyForAttack ) return TaskStatus.Failure;
        if (inGameSceneUiDataManager.waitForRaid == true) return TaskStatus.Failure;

        if (inGameSceneCheckTargetAndGetDistance.target == null) return TaskStatus.Failure;


        // ���� ���� �̳��� ���
        if ((inGameSceneCheckTargetAndGetDistance.isEnemyOutOfAttackRange() == false)   
            && (charState.skillPoint <= 100)) return TaskStatus.Success;

        return TaskStatus.Failure;
	}
}