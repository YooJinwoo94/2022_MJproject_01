using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneCharZPosManager : MonoBehaviour
{
    [SerializeField]
    GameObject charZposObj;



    Vector3 charVector;


    private void Update()
    {
        charVector = charZposObj.transform.position;
        charVector.z = this.transform.parent.position.y / 10;

        charZposObj.transform.position = charVector;
    }
}
