using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScenePlayerCharSkillModule02 : MonoBehaviour
{
    [SerializeField]
    CharState playerCharState;

    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    [SerializeField]
    InGameScenePlayerCharAttack02 inGameScenePlayerCharSkill02;

    [HideInInspector]
    public int attackDamageUpTime = 5;
    [HideInInspector]
    public int attackDamageUpPercent = 10;
    [HideInInspector]
    public int placeTime = 10;
    [HideInInspector]
    public int dotDamgePercent = 5;
    [HideInInspector]
    public int hpUpPercent = 5;
    public int moduleNum = -1;

    public GameObject place;











    public void bulletDamageUp(float howManyDamageUp,float time,GameObject target)
    {
        CharState targetCharState = target.GetComponentInChildren<CharState>();

        if (targetCharState.checkCharCondition(CharState.CharCondition.isUnbeatable) == true) return;
        if (targetCharState.checkCharCondition(CharState.CharCondition.isDamagedUp) == true) return;

        // ¿Ã∆—∆Æ on 

        targetCharState.multiCondition.Add(CharState.CharCondition.isDamagedUp);
        targetCharState.increasedDamageByMarking = howManyDamageUp;

        targetCharState.turnOffCharCondition(time, CharState.CharCondition.isDamagedUp);
    }


    public void placeInstacne(Vector2 pos)
    {
         GameObject placeObj = Instantiate(place, GameObject.Find("bulletPos").transform);

        InGameScenePlayer002DotHpPlaceManager inGameScenePlayer002DotHpPlaceManager = placeObj.GetComponent<InGameScenePlayer002DotHpPlaceManager>();
        inGameScenePlayer002DotHpPlaceManager.placeTime = placeTime;
        inGameScenePlayer002DotHpPlaceManager.transform.position = pos;
        inGameScenePlayer002DotHpPlaceManager.ownerCharState = playerCharState;
        inGameScenePlayer002DotHpPlaceManager.inGameScenePlayerCharSkillModule02 = GetComponentInChildren<InGameScenePlayerCharSkillModule02>();
        inGameScenePlayer002DotHpPlaceManager.box.enabled = true;
    }
}
