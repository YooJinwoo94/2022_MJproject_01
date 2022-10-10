using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScenePlayerCharSkillModule03 : MonoBehaviour
{
    [SerializeField]
    CharState playerCharState;

    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    [SerializeField]
    InGameScenePlayerCharAttack03 inGameScenePlayerCharSkill03;

    [HideInInspector]
    public int attackDamageUpTime = 5;
    [HideInInspector]
    public int attackDamageUpPercent = 10;

    [HideInInspector]
    public int UnbeatableTime = 7;

    [SerializeField]
    GameObject inEndSkillAttackPlaceObj;

    public int moduleNum = -1;












    public void damageUp(float howManyDamageUp, GameObject target)
    {
        CharState targetCharState = target.GetComponentInChildren<CharState>();

        if (targetCharState.checkCharCondition(CharState.CharCondition.isUnbeatable) == true) return;
        if (targetCharState.checkCharCondition(CharState.CharCondition.isDamagedUp) == true) return;

        // ¿Ã∆—∆Æ on 

        targetCharState.multiCondition.Add(CharState.CharCondition.isDamagedUp);
        targetCharState.increasedDamageByMarking = howManyDamageUp;

        targetCharState.turnOffCharCondition(attackDamageUpTime, CharState.CharCondition.isDamagedUp);
    }

    public void playerUnbeatable()
    {
        playerCharState.multiCondition.Add(CharState.CharCondition.isUnbeatable);

        playerCharState.turnOffCharCondition(UnbeatableTime, CharState.CharCondition.isUnbeatable);
    }
   
    public void inEndSkillAttackPlaceInstacne()
    {
        GameObject place =  Instantiate(inEndSkillAttackPlaceObj, this.gameObject.transform);

        InGameScenePlayer003PlaceManager inGameScenePlayer003PlaceManager = place.GetComponent<InGameScenePlayer003PlaceManager>();
        inGameScenePlayer003PlaceManager.charState = playerCharState;
        inGameScenePlayer003PlaceManager.boomOwner = playerCharState.charName;
    }    
}
