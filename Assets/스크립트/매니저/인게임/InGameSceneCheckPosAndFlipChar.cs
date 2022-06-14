using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using BehaviorDesigner.Runtime;




public class InGameSceneCheckPosAndFlipChar : MonoBehaviour
{
    InGameSceneUiDataManager inGameSceneUiDataManager;
    [SerializeField]
    GameObject charRotation;
    [SerializeField]
    InGameSceneCheckTargetAndGetDistance inGameSceneCheckTargetAndGetDistance;
    SharedGameObject obj;
    GameObject target;


    private void Start()
    {
        inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
    }

    private void FixedUpdate()
    {
        if (inGameSceneUiDataManager.nowGameSceneState != InGameSceneUiDataManager.NowGameSceneState.battleStart) return;


        target = inGameSceneCheckTargetAndGetDistance.target;

        if (target == null)
        {
            charRotation.transform.localEulerAngles = new Vector3(0, 0, 0);
            return;
        }

        if (target.transform.position.x <= this.gameObject.transform.position.x &&
            (charRotation.transform.localEulerAngles != new Vector3(0, -180, 0)) )
        {
            charRotation.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
       else if (target.transform.position.x >= this.gameObject.transform.position.x &&
            (charRotation.transform.localEulerAngles != new Vector3(0, 0, 0)) )
        {
            charRotation.transform.localEulerAngles = new Vector3(0, -180, 0);
           
        }
    }
}
