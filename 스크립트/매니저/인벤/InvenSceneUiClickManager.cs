using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSceneUiClickManager : MonoBehaviour
{
    [SerializeField]
    InvenSceneUiDataManager invenSceneUiDataManager;



    public void scrollToggle(int num)
    {
        for (int i = 0; i < invenSceneUiDataManager.scrollObj.Length;i ++)
        {

            invenSceneUiDataManager.scrollObjScrollBar[i].value = 1;
            if (num == i)
            {
                invenSceneUiDataManager.scrollObj[num].SetActive(true);
                continue;
            }
            invenSceneUiDataManager.scrollObj[i].SetActive(false);
        }

    }
}
