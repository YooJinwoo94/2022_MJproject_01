using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using BehaviorDesigner.Runtime;
using UnityEngine.SceneManagement;


public class InGameSceneGetNameAndSetSpine : MonoBehaviour
{
    [SerializeField]
    BehaviorTree charBehaviorTree;
    InGameSceneUiDataManager inGameSceneUiDataManager;

    [SerializeField]
    SkeletonAnimation skeletonAnimation;
    [SerializeField]
    CharState charState;


    private void Start()
    {
        if (inGameSceneUiDataManager == null)
        {
            inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        }


        checkName();
    }



    private void FixedUpdate()
    {
        if (this.gameObject.activeInHierarchy != true) return;
        if (charState.charName != null) return;
        if (!this.gameObject.transform.parent) return;

        checkName();
    }


    void checkName()
    {
        int num = 0;
        charState.charName = this.gameObject.transform.parent.name;
             
        switch (this.gameObject.transform.parent.tag)
        {
            case "playerChar":
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

                charBehaviorTree.ExternalBehavior = inGameSceneUiDataManager.playerBehaviorTreeDataSet[num];
                skeletonAnimation.initialSkinName = inGameSceneUiDataManager.playerSkeletonAnimationSet[num].initialSkinName;
                skeletonAnimation.AnimationName = inGameSceneUiDataManager.playerSkeletonAnimationSet[num].AnimationName;
                break;

            case "enemyChar":
                switch (charState.charName)
                {
                    case "����01":
                        num = 0;
                        break;

                    case "����02":
                        num = 1;
                        break;

                    case "����03":
                        num = 2;
                        break;

                    case "����04":
                        num = 3;
                        break;

                    case "����05":
                        num = 4;
                        break;

                    case "����06":
                        num = 5;
                        break;
                }

                charBehaviorTree.ExternalBehavior = inGameSceneUiDataManager.enemyBehaviorTreeDataSet[num];
                skeletonAnimation.initialSkinName = inGameSceneUiDataManager.enemySkeletonAnimationSet[num].initialSkinName;
                skeletonAnimation.AnimationName = inGameSceneUiDataManager.enemySkeletonAnimationSet[num].AnimationName;
                break;
        }

        //skeletonAnimation.Clear();
        skeletonAnimation.Initialize(true);
    }
}
