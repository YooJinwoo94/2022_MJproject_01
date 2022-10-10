using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using BehaviorDesigner.Runtime;
using UnityEngine.SceneManagement;



public class PlayerChoiceBeforeBattleSceneSceneGetCharAndSetSpine : MonoBehaviour
{
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;

    [SerializeField]
    SkeletonGraphic skeletonGraphic;
    [SerializeField]
    PlayerChoiceBeforeBattleSceneCharState charState;

   



    // Start is called before the first frame update
    void Start()
    {
        playerChoiceBeforeBattleSceneUiDataManager = GameObject.Find("ui_Data_Manager").GetComponent<PlayerChoiceBeforeBattleSceneUiDataManager>();
        checkName();
    }


    void checkName()
    {
        int num = 0;

        charState.charName = this.gameObject.transform.parent.name;

        switch (charState.charName)
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
                num = 8;
                break;
        }

        skeletonGraphic.skeletonDataAsset = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].skeletonDataAsset;
        skeletonGraphic.initialSkinName = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].initialSkinName;
        skeletonGraphic.startingAnimation = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].startingAnimation;

        skeletonGraphic.Clear();
        skeletonGraphic.Initialize(true);
    }
}
