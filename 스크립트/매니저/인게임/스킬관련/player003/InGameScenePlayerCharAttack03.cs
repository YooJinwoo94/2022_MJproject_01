using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;



public class InGameScenePlayerCharAttack03 : MonoBehaviour
{
    InGameSceneUiDataManager inGameSceneUiDataManager;

    [SerializeField]
    SkeletonAnimation skeletonAni;
    [SerializeField]
    InGameScenePlayerCharSkillModule03 inGameScenePlayerCharSkillModule03;
    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    [SerializeField]
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    [SerializeField]
    CharState charState;
    [HideInInspector]
    public bool timeEnd = false;
    //변신 시간
    bool isTransform = false;
    [HideInInspector]
    public int transformTime = 7;

    CharState enemyCharState;
    InGameSceneSkillPointManager inGameSceneSkillPointManager;

    GameObject target;
    float moveSpeedBackTime = 2f;
    float speedUp = 9f;
    
    [HideInInspector]
    public int hpUpPercent = 10;
    [HideInInspector]
    public int attackUpPercent = 10;
    [HideInInspector]
    public int attackSpeedUpPercent = 10;
    [HideInInspector]
    public int moveSpeedUpPercent = 10;

    List<float> orginState = new List<float>();






    private void Start()
    {
        if (charState.charName != "player03") return;

        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponentInChildren<InGameSceneUiDataManager>();
      
        skeletonAni.AnimationState.Event += HandleEvent;
    }

    private void FixedUpdate()
    {
        if (charState.charName != "player03") return;

        checkTargetIsDead();
    }

    public void HandleEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (inGameSceneCheckTargetAndGetDistance.target == null) return;

        switch (e.Data.Name)
        {
            case "hit":

                if ( charState.skillPoint < 100 )
                {
                    normalAttack();

                    normalAtkDamage();
                }
                if (charState.skillPoint >= 100)
                {
                    skillAttack();
     
                    skillAtkDamage();
                }
                break;

            case "end_atk":
                inGameSceneCharSpineAniCon.idle();
                break;

            case "end_skill":
                Debug.Log("aadsadsdasdasdsad");
                isTransform = true;

                inGameSceneCharSpineAniCon.skeletonAni.initialSkinName = "character_003_normal" + "/3_skilled";
                inGameSceneCharSpineAniCon.skeletonAni.Initialize(true);
                skeletonAni.AnimationState.Event += HandleEvent;
                inGameSceneCharSpineAniCon.attack();

                upCharState();
                break;
        }
    }


    public void normalAttack()
    {
        target = inGameSceneCheckTargetAndGetDistance.target;
    }

    public void skillAttack()
    {
        target = inGameSceneCheckTargetAndGetDistance.target;
    }

    public void normalAtkDamage()
    {
        enemyCharState = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<CharState>();
        inGameSceneSkillPointManager = inGameSceneCheckTargetAndGetDistance.target.GetComponentInChildren<InGameSceneSkillPointManager>();

        int damage = charState.attackPower - enemyCharState.defencePower;

        if (enemyCharState.increasedDamageByMarking != 0)
        {
            float percentOfIncreasedDamageByMarking = ((enemyCharState.increasedDamageByMarking + 100) / (float)100);

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
            float percentOfIncreasedDamageByMarking = ((enemyCharState.increasedDamageByMarking + 100) / (float)100);

            damage = Mathf.RoundToInt(damage * percentOfIncreasedDamageByMarking);
        }

        if (damage <= 0) damage = 1;
        enemyCharState.hpBar.Amount -= damage;
        inGameSceneSkillPointManager.skillPointUpDown("up", 10);
        enemyCharState.hp -= damage;
    }






    // 일정시간동안 계속 떄리는 일종의 변신
    IEnumerator TimeCount()
    {
        inGameSceneCheckTargetAndGetDistance.ifSkillUsedOnceChangeTarget = true;
        inGameSceneCharSpineAniCon.skillAttack();

        yield return new WaitForSeconds(transformTime);

        module();
        resetSkillPoint();
        resetCharState();

        isTransform = false;
        timeEnd = false;
        inGameSceneCharSpineAniCon.skeletonAni.initialSkinName = "character_003_normal" + "/1_normal";
        inGameSceneCharSpineAniCon.skeletonAni.Initialize(true);
        skeletonAni.AnimationState.Event += HandleEvent;

        yield return null;
    }

    // 스킬 애니메이션 스타트
    public void skillAniStart()
    {
        if (timeEnd != true)
        {
            timeEnd = true;
            StartCoroutine(TimeCount());
        }

        if (timeEnd == true &&
            isTransform == true &&
            inGameSceneCharSpineAniCon.skeletonAni.AnimationName != charState.charAniName + "/atk")
        {
            inGameSceneCharSpineAniCon.attack();
        }
    }

    public void resetSkillPoint()
    {
        charState.skillPoint = 0;
        charState.skillPointBar.Amount = 0;
        inGameSceneCheckTargetAndGetDistance.ifSkillUsedOnceChangeTarget = false;
    }

    void upCharState()
    {
        orginState[0] = charState.hp;
        orginState[1] = charState.maxHp;
        orginState[2] = charState.attackPower;
        orginState[3] = charState.skillAttackPower;
        orginState[4] = charState.moveSpeed;
        orginState[5] = charState.attackSpeed;

        //============================================================================

        int hp = charState.hp;
        int maxHp = charState.maxHp;
        float hpPercent = ((hpUpPercent + 100) / (float)100);
        hp = Mathf.RoundToInt(hp * hpPercent);
        charState.hpBar.MaximumAmount += maxHp;
        charState.hpBar.Amount += hp;
        charState.hp += hp;

        //============================================================================

        int attackPower = charState.attackPower;
        int skillAttackPower = charState.skillAttackPower;
        float attackPercent = ((attackUpPercent + 100) / (float)100);
        attackPower = Mathf.RoundToInt(attackPower * attackPercent);
        skillAttackPower = Mathf.RoundToInt(skillAttackPower * attackPercent);
        charState.attackPower = attackPower;
        charState.skillAttackPower = skillAttackPower;

        //============================================================================

        float moveSpeed = charState.moveSpeed;     
        float moveSpeedPercent = ((moveSpeedUpPercent + 100) / (float)100);
        moveSpeed = Mathf.RoundToInt(moveSpeed * moveSpeedPercent);
        charState.moveSpeed = moveSpeed;

        //============================================================================

        float attackSpeed = charState.attackSpeed;
        float attackSpeedPercent = ((attackSpeedUpPercent + 100) / (float)100);
        attackSpeed = Mathf.RoundToInt(attackSpeed * attackSpeedPercent);
        charState.attackSpeed = attackSpeed;
    }
    void resetCharState()
    {
        charState.hp = (int)orginState[0];
        charState.maxHp = (int)orginState[1];
        charState.attackPower = (int)orginState[2];
        charState.skillAttackPower = (int)orginState[3];
        charState.moveSpeed = orginState[4];
        charState.attackSpeed = orginState[5];
    }




    public void  module()
    {
        switch (inGameScenePlayerCharSkillModule03.moduleNum)
        {
            case 0:
                inGameScenePlayerCharSkillModule03.damageUp(inGameScenePlayerCharSkillModule03.attackDamageUpPercent,
                inGameSceneCheckTargetAndGetDistance.target);
                break;
            case 1:
                inGameScenePlayerCharSkillModule03.playerUnbeatable();
                break;
            case 2:
                inGameScenePlayerCharSkillModule03.inEndSkillAttackPlaceInstacne();
                break;
        }
    }



    void moveToTarget()
    {
        charState.moveSpeed += speedUp;

        Invoke("moveSpeedBack", moveSpeedBackTime);
    }
    void moveSpeedBack()
    {
        charState.moveSpeed -= speedUp;
    }

    public void checkTargetIsDead()
    {
        if (target == null
            || inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.battleStart) return;

        
        if (inGameSceneCheckTargetAndGetDistance.target.transform.parent.gameObject != target.transform.parent.gameObject)
        {
            target = inGameSceneCheckTargetAndGetDistance.target;
            moveToTarget();
            module();
        }
    }
}
