using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using BehaviorDesigner.Runtime;




public class InGameSceneCheckPosAndFlipChar : MonoBehaviour
{
    [SerializeField]
    GameObject charRotation;
    [SerializeField]
    BehaviorTree behaviorTree;
    SharedGameObject obj;
    GameObject target;



    private void FixedUpdate()
    {
        obj = (SharedGameObject)behaviorTree.GetVariable("target");
        target = obj.Value;

        if (target == null) return;

        if (target.transform.position.x <= this.gameObject.transform.position.x &&
            (charRotation.transform.localEulerAngles != new Vector3(0, -180, 0)) )
        {

            charRotation.transform.localEulerAngles = new Vector3 (0, -180,0);
        }
       else if (target.transform.position.x >= this.gameObject.transform.position.x &&
            (charRotation.transform.localEulerAngles != new Vector3(0, 0, 0)) )
        {
            charRotation.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
