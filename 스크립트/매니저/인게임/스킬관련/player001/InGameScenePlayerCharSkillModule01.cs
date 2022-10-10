using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� �߰� 
public class InGameScenePlayerCharSkillModule01 : MonoBehaviour
{
    GameObject target;
    CharState targetCharState;

    [SerializeField]
    GameObject rangeAttackObj;
    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    [SerializeField]
    InGameScenePlayerCharAttack01 inGameScenePlayerCharSkill01;

    int transformTimeUpInt = 5;



    void getTargetImformation()
    {
        target = inGameSceneCheckTargetAndGetDistance.target;
        targetCharState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();
    }











    // ���� ��Ű��
    public void stunTarget(float stunTime)
    {
        getTargetImformation();

        if (targetCharState.checkCharCondition(CharState.CharCondition.isUnbeatable) == true) return;
        if (targetCharState.checkCharCondition(CharState.CharCondition.isStuned) == true ) return;

        //���� ��Ŵ
        targetCharState.multiCondition.Add(CharState.CharCondition.isStuned);

        //����Ʈ on 

        // ���� �ð��� ���� Ǯ�� 
        targetCharState.turnOffCharCondition(stunTime, CharState.CharCondition.isStuned);
    }


    //���� ����
    public void rangeAttack()
    {
        getTargetImformation();

        //���� ���� ���� 
        GameObject rangeAtk = Instantiate(rangeAttackObj);

        InGameSceneRangeAttack inGameSceneRangeAttack = rangeAtk.GetComponentInChildren<InGameSceneRangeAttack>();
        inGameSceneRangeAttack.whoUseRangeAtk = this.gameObject.transform.parent.parent.gameObject;
        rangeAtk.transform.position = inGameSceneCheckTargetAndGetDistance.target.transform.position;
    }

    // ��Ÿ�� ���̸� �ð� ����
    public void transformTimeUp()
    {
        CharState targetCharState;
        targetCharState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();
        if (targetCharState.hp <= 0 )
        {
            inGameScenePlayerCharSkill01.transformTime += transformTimeUpInt;
        }
    }
}
