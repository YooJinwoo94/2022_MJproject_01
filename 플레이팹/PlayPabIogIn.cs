using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;



public class PlayPabIogIn : MonoBehaviour
{
    [SerializeField]
    PlayerChoiceBeforeBattleSceneUiDataManager playerChoiceBeforeBattleSceneUiDataManager;

    public InputField EmailInput, PasswordInput, UsernameInput;
    public Text LogText;
    public string myID;


    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { print("�α��� ����"); myID = result.PlayFabId; }, (error) => print("�α��� ����"));
    }

    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => print("ȸ������ ����"), (error) => print("ȸ������ ����"));
    }

    public void SetData()
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "0", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[0,0] },
        { "1", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[0,1] },{ "2", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[0,2] },
            { "3", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[1,0] } ,{ "4", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[1,1] }
        ,{ "5", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[1,2] },{ "6", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[2,0] },
            { "7", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[2,1] },{ "8", playerChoiceBeforeBattleSceneUiDataManager.playerCharChoiceData[2,2] } }
        };

        



        PlayFabClientAPI.UpdateUserData(request, (result) => print("������ ���� ����"), (error) => print("������ ���� ����"));
    }

    public void GetData()
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };
        PlayFabClientAPI.GetUserData(request, (result) => { foreach (var eachData in result.Data) LogText.text += eachData.Key + " : " + eachData.Value.Value + "\n"; }, (error) => print("������ �ҷ����� ����"));
    }
}