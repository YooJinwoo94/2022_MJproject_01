using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class InGameSceneCharSpineAniCon : MonoBehaviour
{
    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    [SerializeField]
    public SkeletonAnimation skeletonAni;
    [SerializeField]
    CharState charState;


    public void run()
    {
        switch (this.gameObject.tag)
        {
            case "enemyChar":
                if (skeletonAni.AnimationName == charState.charAniName + "/walk") return;

                skeletonAni.loop = true;
                skeletonAni.AnimationName = charState.charAniName + "/walk";
                break;

            case "playerChar":
                if (skeletonAni.AnimationName == charState.charAniName + "/walk") return;

                skeletonAni.loop = true;
                skeletonAni.AnimationName = charState.charAniName + "/walk";
                break;
        }
    }

    public void attack()
    {
        if (skeletonAni.AnimationName == charState.charAniName + "/atk") return;

        skeletonAni.state.ClearTrack(0);

        skeletonAni.loop = true;
        skeletonAni.AnimationName = charState.charAniName + "/atk";
    }

    public void skillAttack()
    {
        if (skeletonAni.AnimationName == charState.charAniName + "/skill") return;

        skeletonAni.state.ClearTrack(0);

        skeletonAni.loop = true;
        skeletonAni.AnimationName = charState.charAniName + "/skill";
    }


    public void idle()
    {
        if (skeletonAni.AnimationName == charState.charAniName + "/idle") return;

        skeletonAni.loop = true;
        skeletonAni.AnimationName = charState.charAniName + "/idle";
    }

    public void die()
    {
        skeletonAni.state.ClearTrack(0);

        skeletonAni.loop = false;
        skeletonAni.AnimationName = charState.charAniName + "/die";
    }

}
