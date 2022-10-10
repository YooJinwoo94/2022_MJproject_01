using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScenePlayer003PlaceManager : MonoBehaviour
{
    InGameSceneSkillPointManager inGameSceneSkillPointManager;

    [HideInInspector]
    public InGameScenePlayerCharSkillModule03 inGameScenePlayerCharSkillModule03;

    [HideInInspector]
    public string boomOwner;
    [HideInInspector]
    public CharState charState;


    List<GameObject> colList = new List<GameObject>();
    CharState enemyCharState;




    private void Start()
    {
        Destroy(this.gameObject, 0.1f);
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
