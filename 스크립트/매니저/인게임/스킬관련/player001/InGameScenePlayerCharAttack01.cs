using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class InGameScenePlayerCharAttack01 : MonoBehaviour
{
    InGameSceneUiDataManager inGameSceneUiDataManager;

    [SerializeField]
    SkeletonAnimation skeletonAni;
    [SerializeField]
    InGameScenePlayerCharSkillModule01 inGameScenePlayerCharSkillModule01;
    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    [SerializeField]
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    [SerializeField]
    CharState charState;

    [HideInInspector]
    CharState enemyCharState;
    [HideInInspector]
    InGameSceneSkillPointManager inGameSceneSkillPointManager;

    [HideInInspector]
    public bool timeEnd = false ;
    [HideInInspector]
    public int skillAttackCount;
    //���� �ð�
    [HideInInspector]
    public int transformTime = 7;

    Rigidbody2D rid2D;















    private void Start()
    {
        //inGameSceneUiDataManager = GameObject.Find("Manager").GetComponentInChildren<InGameSceneUiDataManager>();

        if (charState.charName != "player01") return;
        skeletonAni.AnimationState.Event += HandleEvent;
    }







    public void HandleEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (inGameSceneCheckTargetAndGetDistance.target == null) return;

        switch (e.Data.Name)
        {
            case "hit":

                if (skeletonAni.AnimationName == charState.charAniName + "/atk")
                {
                    normalAtkDamage();
                }
                if (skeletonAni.AnimationName == charState.charAniName + "/skill")
                {
                    skillAtkDamage();
                    skillAttack();
                }
                break;

            case "end_atk":
                inGameSceneCharSpineAniCon.idle();
                break;

            case "end_skill":
                inGameSceneCharSpineAniCon.idle();
                break;
        }
    }














    public void normalAttack()
    {
       
    }

    //���ʵ��� 3�� ��� 
    //�տ� 2���� �˹�����
    public void skillAttack()
    {
        if (skillAttackCount >= 2)
        {
            inGameSceneCheckTargetAndGetDistance.ifSkillUsedOnceChangeTarget = false;
            skillAttackCount = 0;
        }
        else
        {
            inGameSceneCheckTargetAndGetDistance.ifSkillUsedOnceChangeTarget = true;
            knockback();
            skillAttackCount++;
        }
    }

    public void normalAtkDamage()
    {
        enemyCharState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();
        inGameSceneSkillPointManager = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<InGameSceneSkillPointManager>();

        int damage = charState.attackPower - enemyCharState.defencePower;

        if (enemyCharState.increasedDamageByMarking != 0)
        {
            float percentOfIncreasedDamageByMarking = ((enemyCharState.increasedDamageByMarking + 100) / (float) 100);
            damage = Mathf.RoundToInt(damage * percentOfIncreasedDamageByMarking);
        }


        if (damage <= 0) damage = 1;
        enemyCharState.hpBar.Amount -= damage;
        inGameSceneSkillPointManager.skillPointUpDown("up", 10);
        enemyCharState.hp -= damage;
    }

    public void skillAtkDamage()
    {
        enemyCharState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();
        inGameSceneSkillPointManager = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<InGameSceneSkillPointManager>();

        int damage = charState.attackPower - enemyCharState.defencePower;

        if (enemyCharState.increasedDamageByMarking != 0)
        {
            float percentOfIncreasedDamageByMarking = ((enemyCharState.increasedDamageByMarking + 100) / (float) 100);

            damage = Mathf.RoundToInt(damage * percentOfIncreasedDamageByMarking);
        }

        if (damage <= 0) damage = 1;
        enemyCharState.hpBar.Amount -= damage;
        inGameSceneSkillPointManager.skillPointUpDown("up", 10);
        enemyCharState.hp -= damage;
    }








    // ����
    //inGameScenePlayerCharSkillModule01.stunTarget(2f);

    // ���� ����
    // inGameScenePlayerCharSkillModule01.rangeAttack();

    // 3Ÿ�� ������ ���� �ð� ����
    // inGameScenePlayerCharSkillModule01.transformTimeUp();



    public void knockback()
    {
        int xDirc = charState.transform.position.x - inGameSceneCheckTargetAndGetDistance.target.transform.position.x < 0 ? 1 : -1;
        int yDirc = charState.transform.position.y - inGameSceneCheckTargetAndGetDistance.target.transform.position.y < 0 ? 1 : -1;

        rid2D = inGameSceneCheckTargetAndGetDistance.target.GetComponent<Rigidbody2D>();
        
        rid2D.bodyType = RigidbodyType2D.Dynamic;
        rid2D.AddForce(new Vector2(xDirc, yDirc) * 3, ForceMode2D.Impulse);

        Invoke("zero", 0.2f);
    }
    void zero()
    {
        rid2D.velocity = Vector2.zero;
    }

    // �����ð����� ��� ������ ������ ����
    IEnumerator TimeCount()
    {
        charState.attackRange += 1;
        yield return new WaitForSeconds(transformTime);

        Debug.Log("das1");

        charState.attackRange -= 1;
        skillAttackCount = 0;
        charState.skillPoint = 0;
        charState.skillPointBar.Amount = 0;
        timeEnd = false;
        inGameSceneCheckTargetAndGetDistance.ifSkillUsedOnceChangeTarget = false;

        yield return null;
    }






    public void skillAniStart()
    {
        if (timeEnd != true)
        {
            timeEnd = true;
            StartCoroutine(TimeCount());
        }

        if (timeEnd == true &&
            inGameSceneCharSpineAniCon.skeletonAni.AnimationName != charState.charAniName + "/skill")
        {
            inGameSceneCharSpineAniCon.skillAttack();
        }
    }
}
