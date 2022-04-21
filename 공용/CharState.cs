using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharState : MonoBehaviour
{
    // �̸�
    // ���ݷ� 
    // ����� 
    // ü�� 
    // ��������Ʈ�̸�? 

    [HideInInspector]
    public enum NowState
    {
        isAttack,
        isIdle,
        isDamaged,
        isWalk,
        isReadyForAttack,
        isFindingBush,
    }
    public NowState nowState;

    public float attackRange = 1.5f;
    public float moveSpeed = 1f;

    public string charName;
    public int attackPower = 50;
    public int defensePower = 30;
    public int hpPoint = 200;
    public int skillPoint = 0;



    private void Start()
    {
        nowState = NowState.isIdle;
    }
}
