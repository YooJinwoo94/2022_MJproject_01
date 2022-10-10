using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneBulletDamageManager : MonoBehaviour
{
    InGameSceneSkillPointManager inGameSceneSkillPointManager;
    CharState enemyCharState;

    [HideInInspector]
    public InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    [HideInInspector]
    public CharState charState;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int attackPower;
    [HideInInspector]
    public bool go;
    [HideInInspector]
    public string bulletOwner;
    [HideInInspector]
    public bool isSkillAttack;
    [HideInInspector]
    public GameObject target;

    [SerializeField]
    public GameObject boomArea;
    [SerializeField]
    Renderer sprite;

    List<GameObject> colList = new List<GameObject>();




    private void Update()
    {
        if (go == false) return;

        transform.position = Vector2.MoveTowards(transform.position,
            target.transform.position, 0.05f);
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent.tag != "enemyChar") return;
        
        foreach(GameObject obj in colList)
        {
            if (col.transform.parent.parent.parent.parent.gameObject == obj) return;
        }

        colList.Add(col.transform.parent.parent.parent.parent.gameObject);

        if (inGameSceneCheckTargetAndGetDistance.target.transform.parent != col.transform.parent.parent.parent.parent)
        {
            damageCon(col.transform.parent.parent.gameObject);
        }
        else
        {
            damageCon(col.transform.parent.parent.gameObject);
            go = false;
            boomArea.SetActive(true);
            sprite.name = null;
            Destroy(this.gameObject, 2f);
        }
    }




    void damageCon(GameObject target)
    {
        enemyCharState = target.GetComponentInChildren<CharState>();
        inGameSceneSkillPointManager = target.GetComponentInChildren<InGameSceneSkillPointManager>();

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
}
