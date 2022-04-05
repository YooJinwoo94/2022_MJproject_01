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

                charBehaviorTree.ExternalBehavior = inGameSceneUiDataManager.playerBehaviorTreeDataSet[num];
                skeletonAnimation.initialSkinName = inGameSceneUiDataManager.playerSkeletonAnimationSet[num].initialSkinName;
                skeletonAnimation.AnimationName = inGameSceneUiDataManager.playerSkeletonAnimationSet[num].AnimationName;
                break;

            case "enemyChar":
                switch (charState.charName)
                {
                    case "몬스터01":
                        num = 0;
                        break;

                    case "몬스터02":
                        num = 1;
                        break;

                    case "몬스터03":
                        num = 2;
                        break;

                    case "몬스터04":
                        num = 3;
                        break;

                    case "몬스터05":
                        num = 4;
                        break;

                    case "몬스터06":
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
