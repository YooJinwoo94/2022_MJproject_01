using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class InGameSceneCharSpineAniCon : MonoBehaviour
{
    [SerializeField]
    SkeletonAnimation skeletonAni;

    public void run()
    {
        skeletonAni.loop = true;
        skeletonAni.AnimationName = "run";
    }

    public void attack()
    {
        skeletonAni.loop = false;
        skeletonAni.AnimationName = "attack";
        skeletonAni.AnimationState.AddAnimation(0, "idle", true, 0f);
    }

    public void idle()
    {
        skeletonAni.loop = true;
        skeletonAni.AnimationName = "idle";
    }
}
