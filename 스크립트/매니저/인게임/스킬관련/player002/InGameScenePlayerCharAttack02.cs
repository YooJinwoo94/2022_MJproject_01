using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class InGameScenePlayerCharAttack02 : MonoBehaviour
{
    InGameSceneUiDataManager inGameSceneUiDataManager;

    [SerializeField]
    SkeletonAnimation skeletonAni;
    [SerializeField]
    GameObject bulletObj;
    [SerializeField]
    InGameScenePlayerCharSkillModule02 inGameScenePlayerCharSkillModule02;
    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    [SerializeField]
    InGameSceneCharSpineAniCon inGameSceneCharSpineAniCon;
    [SerializeField]
    CharState charState;

  
    CharState enemyCharState;
    InGameSceneSkillPointManager inGameSceneSkillPointManager;




    private void Start()
    {
        // inGameSceneUiDataManager = GameObject.Find("Manager").GetComponentInChildren<InGameSceneUiDataManager>();

        if (charState.charName != "player02") return;
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
                    skillAttack();
                }
                break;

            case "end_atk":
                inGameSceneCharSpineAniCon.idle();
                break;

            case "end_skill":
                resetSkillPoint();
                inGameSceneCharSpineAniCon.idle();
                break;
        }
    }



    public void normalAttack()
    {

    }

    //저 - 격
    public void skillAttack()
    {
        shootBullet(true);
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







    // 총알 쏘기
    void shootBullet(bool isSkill)
    {
        GameObject bullet = Instantiate(bulletObj, GameObject.Find("bulletPos").transform);
        InGameSceneBulletDamageManager inGameSceneBulletDamageManager = bullet.GetComponentInChildren<InGameSceneBulletDamageManager>();

    
        inGameSceneBulletDamageManager.bulletOwner = tag;
        inGameSceneBulletDamageManager.inGameSceneCheckTargetAndGetDistance = inGameSceneCheckTargetAndGetDistance;
        inGameSceneBulletDamageManager.attackPower = 20;
        inGameSceneBulletDamageManager.charState = charState;
        inGameSceneBulletDamageManager.transform.position = new Vector2(transform.position.x, transform.position.y);
        inGameSceneBulletDamageManager.isSkillAttack = isSkill;
        inGameSceneBulletDamageManager.target = inGameSceneCheckTargetAndGetDistance.target;

        InGameSceneBulletBoomManager inGameSceneBulletBoomManager = bullet.GetComponentInChildren<InGameSceneBulletDamageManager>().boomArea.GetComponentInChildren<InGameSceneBulletBoomManager>();
        inGameSceneBulletBoomManager.boomOwner = tag;
        inGameSceneBulletBoomManager.charState = charState;
        inGameSceneBulletBoomManager.inGameScenePlayerCharSkillModule02 = inGameScenePlayerCharSkillModule02;
        inGameSceneBulletBoomManager.target = inGameSceneCheckTargetAndGetDistance.target;
        inGameSceneBulletDamageManager.go = true; 
    }

















    // 스킬 애니메이션 스타트
    public void skillAniStart()
    {
        inGameSceneCharSpineAniCon.skillAttack();
    }

    public void resetSkillPoint()
    {
        Debug.Log("das2");

        charState.skillPoint = 0;
        charState.skillPointBar.Amount = 0;
        inGameSceneCheckTargetAndGetDistance.ifSkillUsedOnceChangeTarget = false;
    }
}
