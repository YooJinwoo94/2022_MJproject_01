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
        isWalk
    }
    [HideInInspector]
    public NowState nowState;


    public float attackRange = 1.3f;
    public float moveSpeed = 10;

    public string charName;
    public int attackPower = 50;
    public int defensePower = 50;
    public int hpPoint = 200;
    public int skillPoint = 0;



    private void Start()
    {
        nowState = NowState.isIdle;
    }
}
