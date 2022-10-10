using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class InvenSceneDataManager : MonoBehaviour
{
    public SkeletonGraphic[] charSkeletonGraphic;

    public GameObject socketInvenObjSet;
    [HideInInspector]
    public GameObject[] socketInvenObj;

    public List<GameObject> countOfDetailUI = new List<GameObject>();


    private void Start()
    {
        socketInvenObj = new GameObject[socketInvenObjSet.transform.childCount];

        for (int i = 0; i< socketInvenObjSet.transform.childCount; i++)
        {
            socketInvenObj[i] = socketInvenObjSet.transform.GetChild(i).gameObject;
        }
    }
}
