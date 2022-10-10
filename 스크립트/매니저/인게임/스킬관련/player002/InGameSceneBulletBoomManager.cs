using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneBulletBoomManager : MonoBehaviour
{
    InGameSceneSkillPointManager inGameSceneSkillPointManager;

    [HideInInspector]
    public InGameScenePlayerCharSkillModule02 inGameScenePlayerCharSkillModule02;

    [HideInInspector]
    public string boomOwner;
    [HideInInspector]
    public CharState charState;
    [HideInInspector]
    public GameObject target;

    List<GameObject> colList = new List<GameObject>();
    CharState enemyCharState;



    private void Start()
    {
       switch (inGameScenePlayerCharSkillModule02.moduleNum)
        {
            case 0:
                    inGameScenePlayerCharSkillModule02.bulletDamageUp(inGameScenePlayerCharSkillModule02.attackDamageUpPercent,
                    inGameScenePlayerCharSkillModule02.attackDamageUpTime,
                    target);
                break;
            case 1:
                inGameScenePlayerCharSkillModule02.placeInstacne(this.transform.position);
                break;
            case 2:
                inGameScenePlayerCharSkillModule02.placeInstacne(this.transform.position);
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent.tag != "enemyChar") return;

        foreach (GameObject obj in colList)
        {
            if (col.transform.parent.parent.parent.parent.gameObject == obj) return;
        }
        //중복 체크 방지용
        colList.Add(col.transform.parent.parent.parent.parent.gameObject);

        damageCon(col.transform.parent.parent.gameObject);
    }



    void damageCon(GameObject target)
    {
        enemyCharState = target.GetComponentInChildren<CharState>();
        inGameSceneSkillPointManager = target.GetComponentInChildren<InGameSceneSkillPointManager>();


        int damage = charState.skillAttackPower - enemyCharState.defencePower;

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
}
