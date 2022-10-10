using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneRangeAttack : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D coll2D;
    CharState playerCharState;
    CharState enemyCharState;
    InGameSceneSkillPointManager inGameSceneSkillPointManager;
    [HideInInspector]
    public GameObject whoUseRangeAtk;


    private void Start()
    {
        Destroy(this.gameObject, 2f);        
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == whoUseRangeAtk.tag) return;
        
        if (collision.tag != "playerChar" &&
            collision.tag != "enemyChar") return;

         playerCharState = whoUseRangeAtk.transform.parent.GetComponentInChildren<CharState>();
         enemyCharState = collision.transform.parent.parent.GetComponentInChildren<CharState>();
        inGameSceneSkillPointManager = collision.GetComponentInChildren<InGameSceneSkillPointManager>();

        int damage = playerCharState.attackPower - enemyCharState.defencePower;

        if (enemyCharState.increasedDamageByMarking != 0)
        {
            float percentOfIncreasedDamageByMarking = ((enemyCharState.increasedDamageByMarking + 100) / (float)100);
            damage = Mathf.RoundToInt(damage * percentOfIncreasedDamageByMarking);
        }

        if (damage <= 0) damage = 1;

        enemyCharState.hpBar.Amount -= damage;
        enemyCharState.hp -= damage;
        coll2D.enabled = false;
    }
}
