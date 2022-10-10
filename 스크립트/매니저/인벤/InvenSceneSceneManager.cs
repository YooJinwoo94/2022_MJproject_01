using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSceneSceneManager : MonoBehaviour
{
    [SerializeField]
    InvenSceneUiDataManager invenSceneUiDataManager;

    [SerializeField]
    InvenSceneCharState invenSceneCharState;

    [SerializeField]
    InvenSceneDataManager invenSceneDataManager;


    private void Start()
    {
        setSpine();
        setCharData();
        setLove();
    }



    // 착용이_불가능한_소켓_위치입니다_ui 조절
    public void noticeOn()
    {
        invenSceneUiDataManager.noticeAboutSocket[0].SetActive(true);
    }
    public void noticeOff()
    {
        Invoke("noticeOff2", 1f);
    }
    void noticeOff2()
    {
        invenSceneUiDataManager.noticeAboutSocket[0].SetActive(false);
    }



    void setSpine()
    {
        int num = 0;
        switch (invenSceneCharState.charName)
        {
            case "가가가가가":
                num = 0;
                break;

            case "나나나나나":
                num = 1;
                break;

            case "다다다다다":
                num = 2;
                break;

            case "라라라라라":
                num = 3;
                break;

            case "마마마마마":
                num = 4;
                break;

            case "바바바바바":
                num = 5;
                break;

            case "사사사사사":
                num = 6;
                break;

            case "아아아아아":
                num = 7;
                break;
        }

        invenSceneCharState.charSkeletonGraphic.skeletonDataAsset = invenSceneDataManager.charSkeletonGraphic[num].skeletonDataAsset;
        invenSceneCharState.charSkeletonGraphic.initialSkinName = invenSceneDataManager.charSkeletonGraphic[num].initialSkinName;
        invenSceneCharState.charSkeletonGraphic.startingAnimation = invenSceneDataManager.charSkeletonGraphic[num].startingAnimation;

        invenSceneUiDataManager.spineOfThisCharFace.skeletonDataAsset = invenSceneDataManager.charSkeletonGraphic[num].skeletonDataAsset;
        invenSceneUiDataManager.spineOfThisCharFace.initialSkinName = invenSceneDataManager.charSkeletonGraphic[num].initialSkinName;
        invenSceneUiDataManager.spineOfThisCharFace.startingAnimation = invenSceneDataManager.charSkeletonGraphic[num].startingAnimation;

        invenSceneCharState.charSkeletonGraphic.Initialize(true);
        invenSceneUiDataManager.spineOfThisCharFace.Initialize(true);
    }

    public void setCharData()
    {
        invenSceneUiDataManager.charDataString[0].text = invenSceneCharState.charName;
        invenSceneUiDataManager.charDataString[1].text = invenSceneCharState.charHp.ToString();
        invenSceneUiDataManager.charDataString[2].text = invenSceneCharState.charLove.ToString();

        invenSceneUiDataManager.charDataString[3].text =  invenSceneCharState.nameOfSocket[0];
        invenSceneUiDataManager.charDataString[4].text  = invenSceneCharState.nameOfSocket[1];
        invenSceneUiDataManager.charDataString[5].text  = invenSceneCharState.nameOfSocket[2];
    }


    //현재 캐릭터의 호감도를 조사함
    //호감도의 양에 따라 호감도창의 ui가 바뀔수있도록 함
    void setLove()
    {
        int num = 0;

        if (invenSceneCharState.charLove >= 80)
        {
            num = 0;
        }
        else if (invenSceneCharState.charLove >= 60)
        {
            num = 1;
        }
        else if (invenSceneCharState.charLove >= 40)
        {
            num = 2;
        }
        else if (invenSceneCharState.charLove >= 20)
        {
            num = 3;
        }
        else
        {
            num = 4;
        }

        for (int i = 0; i < num; i++)
        {
            invenSceneUiDataManager.loveUiObj[i].SetActive(true);
        }
        for (int i = num; i < invenSceneUiDataManager.loveUiObj.Length; i++)
        {
            invenSceneUiDataManager.loveUiObj[i].SetActive(false);
        }
    }
}
