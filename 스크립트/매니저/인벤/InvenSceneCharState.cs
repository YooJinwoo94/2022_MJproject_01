using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class InvenSceneCharState : MonoBehaviour
{
    //현재 이 아이의 이름
    public string charName;
    
    //현재 이 아이의 체력 
    public int charHp;

    //현재 이 아이의 호감도 
    public int charLove;

    public SkeletonGraphic charSkeletonGraphic;

    public string[] nameOfSocket;
}
