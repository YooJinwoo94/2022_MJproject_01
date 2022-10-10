using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InvenSceneSocketState : MonoBehaviour
{
    [SerializeField]
    Text socketName;
    [SerializeField]
    Text socketDetail;

    public string thisSocketName;
    public string thisSocketDetail;


    private void Start()
    {
        socketName.text = thisSocketName;
        socketDetail.text = thisSocketDetail;
    }
}
