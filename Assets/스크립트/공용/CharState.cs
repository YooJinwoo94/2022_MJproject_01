using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharState : MonoBehaviour
{
    [HideInInspector]
    public enum NowState
    {
        isIdle,
        isDead,
        isWalkToEnemy,
        isWalkToOrginPos,
        isReadyForAttack,
        isFindingBush,
        isCantFindBush
    }
    public NowState nowState;

    public float findRange = 2f;
    public float attackRange = 1.5f;
    public float moveSpeed = 1f;

    public string charName;
    public int attackPower = 50;
    public int defensePower = 30;
    public int hpPoint = 200;
    public int skillPoint = 0;

    public int sponPos;


    private void Start()
    {
        nowState = NowState.isIdle;
    }
}
