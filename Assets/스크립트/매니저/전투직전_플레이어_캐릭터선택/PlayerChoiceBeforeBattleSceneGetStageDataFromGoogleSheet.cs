using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;
public class PlayerChoiceBeforeBattleSceneGetStageDataFromGoogleSheet : MonoBehaviour
{
    string url;
    string test;
    public static PlayerChoiceBeforeBattleSceneGetStageDataFromGoogleSheet instance = null;
    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void getStageInfo(string stageName)
    {
        switch (stageName)
        {
            case "Stage01":
                url = "https://sheets.googleapis.com/v4/spreadsheets/1KGntESjTsjSLA_uphJ1Dng_zRak2RvR1WvOFVbM7u6I/values/%EC%8B%9C%ED%8A%B81?key=AIzaSyAwp1t2Tderv67o7KDJuLS6CpObpqJYE1s&range=B2:W2";
                break;

            case "Stage02":
                break;

            case "Stage03":
                break;

            case "Stage04":
                break;
        }

        StartCoroutine("GetDataFromGoogleSheet");
    }
    IEnumerator GetDataFromGoogleSheet()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();   // 통신을 함

        string data = www.downloadHandler.text; // 데이터 받아옴
        Debug.Log("받기 끝");
     
        test = data.Substring(data.LastIndexOf(":"));
        test = test.Replace(":", "");
        test = test.Substring(9, test.Length - 20);

        SceneManager.LoadScene("InGameScene");
    }


    public void setDataToStage()
    {
           InGameSceneUiDataManager inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
           InGameSceneGameManager inGameSceneGameManager = GameObject.Find("Manager").GetComponent<InGameSceneGameManager>();
           inGameSceneUiDataManager.enemyCharInGrid = JsonConvert.DeserializeObject<string[,,]>(JsonConvert.DeserializeObject(test).ToString());
           //inGameSceneGameManager.setEnemyCharData(); 
    }

    //삭제
    /*
public void getStageName(string stageName)
{
  // getTitleData(stageName);
}


    void getTitleData(string stageName)
{
    PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
   result => {
       if (result.Data == null || !result.Data.ContainsKey(stageName)) Debug.Log("No " + " " + stageName);
       else
       {
           InGameSceneUiDataManager inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent<InGameSceneUiDataManager>();
           InGameSceneGameManager inGameSceneGameManager = GameObject.Find("Manager").GetComponent<InGameSceneGameManager>();

           inGameSceneUiDataManager.enemyCharInGrid = JsonConvert.DeserializeObject<string[,,]>(result.Data[stageName]);
           inGameSceneGameManager.setEnemyCharData();

           Debug.Log(result.Data[stageName]);
       }
   },
   error => {
       Debug.Log("Got error getting titleData:");
       Debug.Log(error.GenerateErrorReport());
   }
);
}
        */
}
