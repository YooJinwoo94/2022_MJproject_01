using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Minimalist.Bar.Quantity;


public class InGameSceneGetSetDataOfChar : MonoBehaviour
{
    [SerializeField]
    CharState charState;







    private void Start()
    {
        charState = gameObject.GetComponentInChildren<CharState>();
        getSetDataOfChar();
    }



    void getSetDataOfChar()
    {
        int num = 0;

        switch(charState.charName)
        {
            case "player01":
                num = 0;
                break;
            case "player02":
                num = 1;
                break;
            case "player03":
                num = 2;
                break;
            case "player04":
                num = 3;
                break;
            case "player05":
                num = 4;
                break;
            case "player06":
                num = 5;
                break;
            case "player07":
                num = 6;
                break;
            case "player08":
                num = 7;
                break;
            case "player09":
                num = 8;
                break;
            case "player10":
                num = 9;
                break;
        }

        charState.hp = PlayerDataJsonManager.instance.playerData.charState[num].hp;
        charState.skillPoint = PlayerDataJsonManager.instance.playerData.charState[num].skillPoint;

        charState.attackPower = PlayerDataJsonManager.instance.playerData.charState[num].attackPower;
        charState.skillAttackPower = PlayerDataJsonManager.instance.playerData.charState[num].skillAttackPower;
        charState.defencePower = PlayerDataJsonManager.instance.playerData.charState[num].defencePower;
        charState.criticalPercent = PlayerDataJsonManager.instance.playerData.charState[num].criticalPercent;

        charState.attackRange = PlayerDataJsonManager.instance.playerData.charState[num].attackRange;
        charState.attackSpeed = PlayerDataJsonManager.instance.playerData.charState[num].attackSpeed;
        charState.moveSpeed = PlayerDataJsonManager.instance.playerData.charState[num].moveSpeed;

        charState.love = PlayerDataJsonManager.instance.playerData.charState[num].love;
        charState.socketName[0] = PlayerDataJsonManager.instance.playerData.charState[num].socketName[0];
        charState.socketName[1] = PlayerDataJsonManager.instance.playerData.charState[num].socketName[1];

        charState.weaponName[0] = PlayerDataJsonManager.instance.playerData.charState[num].weaponName[0];
        charState.weaponName[1] = PlayerDataJsonManager.instance.playerData.charState[num].weaponName[1];

        charState.atkPriority = PlayerDataJsonManager.instance.playerData.charState[num].atkPriority;

        charState.hpBar.MaximumAmount = PlayerDataJsonManager.instance.playerData.charState[num].hp;
        charState.hpBar.FillAmount = 1;
    }
}
