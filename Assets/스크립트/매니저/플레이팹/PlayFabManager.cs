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




    // 로그인 관련
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { print("로그인 성공"); myID = result.PlayFabId; setPlayerAndEnemyData();}, (error) => print("로그인 실패"));
    }
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => print("회원가입 성공"), (error) => print("회원가입 실패"));
    }






    // 데이터 올리기 관련
    public void putNowGridData()
    {    
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "현재 전투에 사용할 그리드", JsonConvert.SerializeObject(PlayersCharGridDataManager.instance.nowGridData) } } 
        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }
    public void putAllGridData()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() { { "배치한 모든 그리드의 정보값", JsonConvert.SerializeObject(PlayersCharGridDataManager.instance.allGridData) } }
        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }












    // 데이터 받아오기 관련
    public void setPlayerAndEnemyData()
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };
        switch (SceneManager.GetActiveScene().name)
        {
            case "logInScene":
                PlayFabClientAPI.GetUserData(request, playerLoginData, (error) => print("데이터 불러오기 실패"));
                break;

            case "PlayerChoiceBeforeBattleScene":
                PlayFabClientAPI.GetUserData(request, playerCharDataInBeforeBattleScene, (error) => print("데이터 불러오기 실패"));
                break;

                
            case "InGameScene":
                // PlayFabClientAPI.GetUserReadOnlyData(request, playerInGameData, (error) => print("데이터 불러오기 실패"));
                PlayerChoiceBeforeBattleSceneGetStageDataFromGoogleSheet.instance.setDataToStage();
                break;
        }
    }


    void playerLoginData(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("배치한 모든 그리드의 정보값"))
        {
            PlayersCharGridDataManager.instance.allGridData = JsonConvert.DeserializeObject<string[,,]>(result.Data["배치한 모든 그리드의 정보값"].Value);
            SceneManager.LoadScene("PlayerChoiceBeforeBattleScene");
        }
    }
    void playerCharDataInBeforeBattleScene(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("배치한 모든 그리드의 정보값"))
        {
            PlayerChoiceBeforeBattleSceneSceneManager playerChoiceBeforeBattleSceneSceneManager;
            playerChoiceBeforeBattleSceneSceneManager = GameObject.Find("SceneManager").GetComponent<PlayerChoiceBeforeBattleSceneSceneManager>();
            playerChoiceBeforeBattleSceneSceneManager.setGridDataFromServer();
        }
    }

    /*
    void playerInGameData(GetUserDataResult result)
    {
        GetStageDataFromPlayFab getMonsterSheetGoogleSheet = GameObject.Find("Manager").GetComponent<GetStageDataFromPlayFab>();
        getMonsterSheetGoogleSheet.getStageName("Stage01");
    }
    */
}