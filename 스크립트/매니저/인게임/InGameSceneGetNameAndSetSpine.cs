using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using BehaviorDesigner.Runtime;
using UnityEngine.SceneManagement;
using Spine;

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
        if (SceneManager.GetActiveScene().name != "InGameScene") return;

        if (inGameSceneUiDataManager == null)
        {
            inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
        }

        checkName();
    }



    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "InGameScene") return;
        if (this.gameObject.activeInHierarchy != true) return;
        if (charState.charName != null) return;
        if (!this.gameObject.transform.parent) return;

       // checkName();
    }


    void checkName()
    {
        int num = 0;
       
        switch (this.gameObject.transform.parent.tag)
        {
            case "playerChar":
                switch (charState.charName)
                {
                    case "player01":
                        charState.charAniName = "001_anim";
                        num = 0;

                        setPlayerAni(num, "character_001_normal");
                        break;

                    case "player02":
                        charState.charAniName = "002_anim";
                        num = 1;

                        setPlayerAni(num, "character_002_normal");
                        break;

                    case "player03":
                        charState.charAniName = "003_anim";
                        num = 2;

                        setPlayerAni(num, "character_003_normal");                
                        break;

                    case "player04":
                        charState.charAniName = "004_anim";
                        num = 3;

                        setPlayerAni(num, "character_004_normal");
                        break;

                    case "player05":
                        charState.charAniName = "005_anim";   
                        num = 4;

                        setPlayerAni(num, "character_005_normal");
                        break;

                    case "player06":
                        charState.charAniName = "006_anim";
                        num = 5;

                        setPlayerAni(num, "character_006_normal");
                        break;

                    case "player07":
                        charState.charAniName = "007_anim";
                        num = 6;

                        setPlayerAni(num, "character_007_normal");
                        break;

                    case "player08":
                        charState.charAniName = "008_anim";
                        num = 7;

                        setPlayerAni(num, "character_008_normal");
                        break;

                    case "player09":
                        charState.charAniName = "009_anim";
                        num = 8;

                        setPlayerAni(num, "character_009_normal");
                        break;

                    case "player10":
                        charState.charAniName = "010_anim";
                        num = 9;

                        setPlayerAni(num, "character_010_normal");
                        break;
                }
                break;

            case "enemyChar":
                switch (charState.charName)
                {
                    case "monster01":
                        num = 0;
                        charState.charAniName = "001_anim";

                        setMonsterAni(num, "monster_001_normal");
                        break;

                    case "monster02":
                        num = 1;
                        charState.charAniName = "002_anim";

                        setMonsterAni(num, "monster_002_normal");
                        break;

                    case "monster03":
                        num = 2;
                        charState.charAniName = "003_anim";

                        setMonsterAni(num, "monster_003_normal");
                        break;

                    case "monster04":
                        num = 3;
                        charState.charAniName = "004_anim";

                        setMonsterAni(num, "monster_004_normal");
                        break;

                    case "monster05":
                        num = 4;
                        charState.charAniName = "005_anim";

                        setMonsterAni(num, "monster_005_normal");
                        break;

                    case "monster06":
                        num = 5;
                        charState.charAniName = "006_anim";

                        setMonsterAni(num, "monster_006_normal");
                        break;

                    case "monster07":
                        num = 6;
                        charState.charAniName = "007_anim";

                        setMonsterAni(num, "monster_007_normal");
                        break;

                    case "monster08":
                        num = 7;
                        charState.charAniName = "008_anim";

                        setMonsterAni(num, "monster_008_normal");
                        break;

                    case "monster09":
                        num = 8;
                        charState.charAniName = "009_anim";

                        setMonsterAni(num, "monster_009_normal");
                        break;

                    case "monster10":
                        num = 9;
                        charState.charAniName = "010_anim";

                        setMonsterAni(num, "monster_010_normal");
                        break;
                }
                break;
        }

        skeletonAnimation.Initialize(true);
    }





    void setPlayerAni(int num , string characterSkinName)
    {
        skeletonAnimation.skeletonDataAsset = inGameSceneUiDataManager.playerSkeletonAnimationSet[num].skeletonDataAsset;
        charBehaviorTree.ExternalBehavior = inGameSceneUiDataManager.playerBehaviorTreeDataSet[num];
        skeletonAnimation.AnimationName = inGameSceneUiDataManager.playerSkeletonAnimationSet[num].AnimationName;

        skeletonAnimation.initialSkinName = characterSkinName + "/1_normal";
        skeletonAnimation.Initialize(true);
    }


    void setMonsterAni(int num, string characterSkinName)
    {
        skeletonAnimation.skeletonDataAsset = inGameSceneUiDataManager.enemySkeletonAnimationSet[num].skeletonDataAsset;
        charBehaviorTree.ExternalBehavior = inGameSceneUiDataManager.enemyBehaviorTreeDataSet[num];
        skeletonAnimation.AnimationName = inGameSceneUiDataManager.enemySkeletonAnimationSet[num].AnimationName;

        skeletonAnimation.initialSkinName = characterSkinName + "/1_normal";
        skeletonAnimation.Initialize(true);
    }
}
