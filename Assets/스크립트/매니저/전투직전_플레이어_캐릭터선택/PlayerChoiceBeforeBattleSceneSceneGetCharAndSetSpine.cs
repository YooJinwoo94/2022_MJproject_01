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
            case "��Ʋ����_ĳ����":
                num = 0;
                break;

            case "����������":
                num = 0;
                break;

            case "����������":
                num = 1;
                break;

            case "�ٴٴٴٴ�":
                num = 2;
                break;

            case "������":
                num = 3;
                break;

            case "����������":
                num = 4;
                break;

            case "�ٹٹٹٹ�":
                num = 5;
                break;
        }

        SkeletonGraphic.initialSkinName = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].initialSkinName;
        SkeletonGraphic.startingAnimation = playerChoiceBeforeBattleSceneUiDataManager.playerSkeletonGraphicSet[num].startingAnimation;

        SkeletonGraphic.Clear();
        SkeletonGraphic.Initialize(true);
    }
}
