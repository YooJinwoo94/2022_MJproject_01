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
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
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
