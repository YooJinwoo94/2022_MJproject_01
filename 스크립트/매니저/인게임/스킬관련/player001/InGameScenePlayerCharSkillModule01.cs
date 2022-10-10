using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 기절 추가 
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











    // 스턴 시키기
    public void stunTarget(float stunTime)
    {
        getTargetImformation();

        if (targetCharState.checkCharCondition(CharState.CharCondition.isUnbeatable) == true) return;
        if (targetCharState.checkCharCondition(CharState.CharCondition.isStuned) == true ) return;

        //기절 시킴
        targetCharState.multiCondition.Add(CharState.CharCondition.isStuned);

        //이팩트 on 

        // 일정 시간후 스턴 풀림 
        targetCharState.turnOffCharCondition(stunTime, CharState.CharCondition.isStuned);
    }


    //범위 공격
    public void rangeAttack()
    {
        getTargetImformation();

        //범위 공격 시작 
        GameObject rangeAtk = Instantiate(rangeAttackObj);

        InGameSceneRangeAttack inGameSceneRangeAttack = rangeAtk.GetComponentInChildren<InGameSceneRangeAttack>();
        inGameSceneRangeAttack.whoUseRangeAtk = this.gameObject.transform.parent.parent.gameObject;
        rangeAtk.transform.position = inGameSceneCheckTargetAndGetDistance.target.transform.position;
    }

    // 막타로 죽이면 시간 증가
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
