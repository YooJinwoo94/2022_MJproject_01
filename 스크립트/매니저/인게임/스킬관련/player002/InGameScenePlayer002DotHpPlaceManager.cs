using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScenePlayer002DotHpPlaceManager : MonoBehaviour
{
    public CharState ownerCharState;
    public BoxCollider2D box;

    //player02øÎ
    //========================================================================
    public InGameScenePlayerCharSkillModule02 inGameScenePlayerCharSkillModule02;
    public List<GameObject> dotDamageList = new List<GameObject>();
    public List<GameObject> hpUpList = new List<GameObject>();
    bool isPlaceEnd = false;
    [HideInInspector]
    public int placeTime = 10;
    //=========================================================================





    private void Start()
    {
        StartCoroutine(CheckTime());
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.transform.parent.parent.parent.parent.gameObject;

        switch (ownerCharState.charName)
        {
            case "player02":

                if (inGameScenePlayerCharSkillModule02.moduleNum == 1)
                {
                    if (col.tag != "enemyChar") return;

                    foreach (GameObject targetObj in dotDamageList)
                    {
                        if (targetObj == obj) return;
                    }

                    dotDamageList.Add(obj);
                    StartCoroutine(DotDamage(obj));
                }
                else if (inGameScenePlayerCharSkillModule02.moduleNum == 2)
                {
                    if (col.tag != "playerChar") return;

                    foreach (GameObject targetObj in hpUpList)
                    {
                        if (targetObj == obj) return;
                    }

                    hpUpList.Add(obj);
                    StartCoroutine(HpUp(obj));
                }
                break;


            case "player03":
                if (col.tag != "enemyChar") return;

                foreach (GameObject targetObj in dotDamageList)
                {
                    if (targetObj == obj) return;
                }


                break;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        GameObject obj = col.transform.parent.parent.parent.parent.gameObject;

        switch (ownerCharState.charName)
        {
            case "player02":
                if (inGameScenePlayerCharSkillModule02.moduleNum == 1)
                {
                    if (col.tag != "enemyChar") return;

                    foreach (GameObject targetObj in dotDamageList)
                    {
                        if (targetObj == obj)
                        {
                            dotDamageList.Remove(obj);
                            StopCoroutine(DotDamage(col.gameObject.transform.parent.parent.gameObject));
                            return;
                        }
                    }
                }
                else if (inGameScenePlayerCharSkillModule02.moduleNum == 2)
                {
                    if (col.tag != "playerChar") return;

                    foreach (GameObject targetObj in hpUpList)
                    {
                        if (targetObj == obj)
                        {
                            hpUpList.Remove(obj);
                            StopCoroutine(HpUp(col.gameObject.transform.parent.parent.gameObject));
                            return;
                        }
                    }
                }
                break;
        }
    }

    IEnumerator CheckTime()
    {
        Debug.Log("Ω√¿€");

        yield return new WaitForSeconds(placeTime);

        isPlaceEnd = true;
        Destroy(gameObject);

        yield return null;
    }

    IEnumerator DotDamage(GameObject obj)
    {
        CharState charState = obj.GetComponentInChildren<CharState>();

        if (charState.checkCharCondition(CharState.CharCondition.isUnbeatable) == true) yield break;

        charState.multiCondition.Add(CharState.CharCondition.isDotDamaged);
        charState.turnOffCharCondition(placeTime, CharState.CharCondition.isDotDamaged);

        while (isPlaceEnd == false )
        {
            if (obj.GetComponentInChildren<CharState>().hp <= 0) yield break;

            float percent = ((inGameScenePlayerCharSkillModule02.dotDamgePercent) / (float)100.0 );
            obj.GetComponentInChildren<CharState>().hp -= Mathf.RoundToInt(obj.GetComponentInChildren<CharState>().skillAttackPower * percent);

            yield return new WaitForSeconds(1f);    
        }
        yield return null;
    }

    IEnumerator HpUp(GameObject obj)
    {
        while (isPlaceEnd == false)
        {
            if (obj.GetComponentInChildren<CharState>().hp <= 0) yield break;

            float percentOfHpIncreased = ((inGameScenePlayerCharSkillModule02.hpUpPercent + 100) / (float)100.0 );

            obj.GetComponentInChildren<CharState>().hp = Mathf.RoundToInt(obj.GetComponentInChildren<CharState>().skillAttackPower * percentOfHpIncreased);

            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
