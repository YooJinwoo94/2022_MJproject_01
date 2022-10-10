using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;



public class InvenSceneUiDataManager : MonoBehaviour
{
    //스크롤하는 obj
   public GameObject[] scrollObj;
    // 스크롤하는 obj의 스크롤바
    public Scrollbar[] scrollObjScrollBar;

    public GameObject[] noticeAboutSocket;

    public SkeletonGraphic spineOfThisCharFace;

    public Text[] charDataString;

    public GameObject[] loveUiObj;
}
