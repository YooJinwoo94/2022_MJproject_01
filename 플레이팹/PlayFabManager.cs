using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;


public class PlayFabManager : MonoBehaviour
{
    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;

    public InputField  EmailInput, PasswordInput;

    public string myID;




    // �α��� ����
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { print("�α��� ����"); myID = result.PlayFabId; setPlayerData();}, (error) => print("�α��� ����"));
    }
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => print("ȸ������ ����"), (error) => print("ȸ������ ����"));
    }






    // ������ �ø��� ����
    public void putNowGridData()
    {    
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "���� ������ ����� �׸���", JsonConvert.SerializeObject(PlayersCharGridDataManager.instance.nowGridData) } } 
        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("������ ���� ����"), (error) => print("������ ���� ����"));
    }
    public void putAllGridData()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() { { "��ġ�� ��� �׸����� ������", JsonConvert.SerializeObject(PlayersCharGridDataManager.instance.allGridData) } }
        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("������ ���� ����"), (error) => print("������ ���� ����"));
    }




    // ������ �޾ƿ��� ����
    public void setPlayerData()
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

            case "InGameScene":
                PlayFabClientAPI.GetUserReadOnlyData(request, playerInGameData, (error) => print("������ �ҷ����� ����"));
                break;
        }
    }


    void playerLoginData(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("��ġ�� ��� �׸����� ������"))
        {
            PlayersCharGridDataManager.instance.allGridData = JsonConvert.DeserializeObject<string[,,]>(result.Data["��ġ�� ��� �׸����� ������"].Value);
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
    void playerInGameData(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("��������01 ���� ��ġ"))
        {
            InGameSceneUiDataManager inGameSceneUiDataManager = GameObject.Find("Manager").GetComponent< InGameSceneUiDataManager>();
            inGameSceneUiDataManager.enemyCharInGrid = JsonConvert.DeserializeObject<string[,,]>(result.Data["��������01 ���� ��ġ"].Value);

            InGameSceneUISceneManager inGameSceneUISceneManager = GameObject.Find("Manager").GetComponent<InGameSceneUISceneManager>();

            //inGameSceneUISceneManager.setPlayerCharData();
            inGameSceneUISceneManager.checkPlayerCountInGrid();
            inGameSceneUISceneManager.checkMonsterCountInGrid();
            inGameSceneUISceneManager.setEnemyCharData();
        }
    }
}