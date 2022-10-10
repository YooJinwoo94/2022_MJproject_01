using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Minimalist.Bar.Quantity;


public class CharState : MonoBehaviour
{
    public QuantityBhv hpBar;
    public QuantityBhv skillPointBar;

    [HideInInspector]
    public enum NowState
    {
        isDead,
        isWalkToEnemy,
        isWaitForCutScene,
        isReadyForAttack,
        isFindingBush,
        isWaitForBattleAndInAttackPos
    }

    // 무적
    // 스턴 
    /// <summary>
    ///////////////////////
    /// </summary>
    // 받는 뎀지 상태
    // 도트딜 받는상태
    public enum CharCondition
    {
        isStuned,     // 스턴 
        isUnbeatable, // 무적 
        isDotDamaged,  // 도트
        isDamagedUp
    }

    public NowState nowState;
    public List<CharCondition> multiCondition = new List<CharCondition>();


    public string charName;

    public int maxHp;
    public int hp ;
    public int attackPower ;
    public int skillAttackPower;
    public int defencePower;
    public int skillPoint ;
    public float attackRange ;
    public float attackSpeed ;
    public float moveSpeed ;
    public int criticalPercent;
    public string[] socketName = new string[2];
    public string[] weaponName = new string[2];

    public int love ;

    [HideInInspector]
    public int sponPos;
    [HideInInspector]
    public string charAniName;
    [HideInInspector]
    public string atkPriority;
    public float increasedDamageByMarking;


    private void Start()
    {
        maxHp = hp;
        nowState = NowState.isWaitForCutScene;
    }



   public bool checkCharCondition(CharCondition checkState)
    {
        foreach (CharCondition charCondition in this.multiCondition)
        {
            if (checkState == charCondition) return true;
        }

        return false;
    }


    public void turnOffCharCondition(float time, CharCondition checkState)
    {
        switch (checkState)
        {
            case CharCondition.isStuned:
                Invoke("returnStunToNormalState", time);
                break;

            case CharCondition.isDamagedUp:
                Invoke("returnDamagedUpToNormalState", time);
                break;

            case CharCondition.isDotDamaged:
                Invoke("returnDotDamagedToNormalState", time);
                break;

            case CharCondition.isUnbeatable:
                Invoke("returnUnbeatableToNormalState", time);
                break;
        }
    }


    void returnStunToNormalState()
    {
        multiCondition.Remove(CharCondition.isStuned);
    }
    void returnDamagedUpToNormalState()
    {
        increasedDamageByMarking = 0;
        multiCondition.Remove(CharCondition.isDamagedUp);
    }
    void returnDotDamagedToNormalState()
    {
        multiCondition.Remove(CharCondition.isDotDamaged);
    }
    void returnUnbeatableToNormalState()
    {
        multiCondition.Remove(CharCondition.isUnbeatable);
    }
}
