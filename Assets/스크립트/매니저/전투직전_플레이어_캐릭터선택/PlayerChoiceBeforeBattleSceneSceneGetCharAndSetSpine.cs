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
    SkeletonGraphic SkeletonGraphic;
    [SerializeField]
    CharState charState;




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
            case "배틀씬용_캐릭터":
                num = 0;
                break;

            case "가가가가가":
                num = 0;
                break;

            case "나나나나나":
                num = 1;
                break;

            case "다다다다다":
                num = 2;
                break;

            case "라라라라라":
                num = 3;
                break;

            case "마마마마마":
                num = 4;
                break;

            case "바바바바바":
                num = 5;
                break;
        }

        SkeletonGraphic.initialSkinName = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].initialSkinName;
        SkeletonGraphic.startingAnimation = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].startingAnimation;

        SkeletonGraphic.Clear();
        SkeletonGraphic.Initialize(true);
    }
}
