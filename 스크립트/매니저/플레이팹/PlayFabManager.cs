using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;


public class PlayFabManager : MonoBehaviour
{/*
    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;

    public InputField  EmailInput, PasswordInput;

    public string myID;




    // �α��� ����
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { print("�α��� ����"); myID = result.PlayFabId; setPlayerAndEnemyData();}, (error) => print("�α��� ����"));
    }
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => print("ȸ������ ����"), (error) => print("ȸ������ ����"));
    }





    /*
    // ������ �ø��� ����
    public void putNowGridData()
    {    
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "���� ������ ����� �׸���", JsonConvert.SerializeObject(PlayerDataJsonManager.instance.playerData.charGridData.nowGridData) } } 
        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("������ ���� ����"), (error) => print("������ ���� ����"));
    }
    public void putAllGridData()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() { { "��ġ�� ��� �׸����� ������", JsonConvert.SerializeObject(PlayerDataJsonManager.instance.playerData.charGridData.allGridData) } }
        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("������ ���� ����"), (error) => print("������ ���� ����"));
    }







    /*
    // ������ �޾ƿ��� ����
    public void setPlayerAndEnemyData()
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };
        switch (SceneManager.GetActiveScene().name)
        {
            case "logInScene":
                PlayFabClientAPI.GetUserData(request, playerLoginData, (error) => print("������ �ҷ����� ����"));
                break;

            case "PlayerChoiceBeforeBattleScene":
                PlayFabClientAPI.GetUserData(request, playerCharDataInBeforeBattleScene, (error) => print("������ �ҷ����� ����"));
                break;

                
             //case "InGameScene":
                // PlayFabClientAPI.GetUserReadOnlyData(request, playerInGameData, (error) => print("������ �ҷ����� ����"));
                // PlayerChoiceBeforeBattleSceneGetStageDataFromGoogleSheet.instance.setDataToStage();
              //  break;
        }
    }

    */

    /*
    void playerLoginData(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("��ġ�� ��� �׸����� ������"))
        {
            PlayerDataJsonManager.instance.playerData.charGridData.allGridData = JsonConvert.DeserializeObject<string[,,]>(result.Data["��ġ�� ��� �׸����� ������"].Value);
            PlayerDataJsonManager.instance.playerData.charGridData.nowGridData = JsonConvert.DeserializeObject<string[,]>(result.Data["���� ������ ����� �׸���"].Value);
          
            Debug.Log(PlayerDataJsonManager.instance.playerData.charGridData.allGridData[0, 1, 1]);
            Debug.Log(PlayerDataJsonManager.instance.playerData.charGridData.nowGridData[1, 1]);
           
            SceneManager.LoadScene("PlayerChoiceBeforeBattleScene");
        }
    }
    void playerCharDataInBeforeBattleScene(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("��ġ�� ��� �׸����� ������"))
        {
            PlayerChoiceBeforeBattleSceneSceneManager playerChoiceBeforeBattleSceneSceneManager;
            playerChoiceBeforeBattleSceneSceneManager = GameObject.Find("SceneManager").GetComponent<PlayerChoiceBeforeBattleSceneSceneManager>();
            playerChoiceBeforeBattleSceneSceneManager.setGridDataFromServer();
        }
    }
    */
    
   // void playerInGameData(GetUserDataResult result)
    //{
   //     GetStageDataFromPlayFab getMonsterSheetGoogleSheet = GameObject.Find("Manager").GetComponent<GetStageDataFromPlayFab>();
    //    getMonsterSheetGoogleSheet.getStageName("Stage01");
   // }
}