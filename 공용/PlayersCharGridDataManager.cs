using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCharGridDataManager : MonoBehaviour
{
    [HideInInspector]
    public string[,] nowGridData = new string[3, 3];
    [HideInInspector]
    public string[,,] allGridData = new string[4, 3, 3];
    [HideInInspector]
    public int playerCharChoiceCount = 0;





    public static PlayersCharGridDataManager instance = null;

    private void Start()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
}
