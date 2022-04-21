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
        skeletonAni.AnimationName = "run";
    }

    public void attack()
    {
        skeletonAni.AnimationName = "attack";
    }

    public void idle()
    {
        skeletonAni.AnimationName = "idle";
    }
}
