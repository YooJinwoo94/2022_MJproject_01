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



    // ������_�Ұ�����_����_��ġ�Դϴ�_ui ����
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
            case "����������":
                num = 0;
                break;

            case "����������":
                num = 1;
                break;

            case "�ٴٴٴٴ�":
                num = 2;
                break;

            case "������":
                num = 3;
                break;

            case "����������":
                num = 4;
                break;

            case "�ٹٹٹٹ�":
                num = 5;
                break;

            case "������":
                num = 6;
                break;

            case "�ƾƾƾƾ�":
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


    //���� ĳ������ ȣ������ ������
    //ȣ������ �翡 ���� ȣ����â�� ui�� �ٲ���ֵ��� ��
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
