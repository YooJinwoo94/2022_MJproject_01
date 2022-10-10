using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;


public class SaveLoadDataManager : MonoBehaviour
{
    public void savePlayerData()
    {
        string jsonData = JsonConvert.SerializeObject(PlayerDataJsonManager.instance.playerData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        File.WriteAllBytes(Application.dataPath + "/PlayerDataFile.json", data);
    }
    public void saveSettingData()
    {
        string jsonData = JsonConvert.SerializeObject(GameDataJsonManager.instance.dataOfSetting);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        File.WriteAllBytes(Application.dataPath + "/GameDataJsonManager.dataOfSetting.json", data);
    }

    public void loadPlayerData()
    {
        // 개인 캐릭터 데이터 가져오기.
        loadPlayersOwnCharData();

        // 캐릭터 데이터 DB 가져오기.
        loadPlayersCharDB();

        loadSetting();
        //비교후 가져오기.
         loadCharDataIfNull();
    }
    
    void loadPlayersOwnCharData()
    {
        FileStream stream = new FileStream(Application.dataPath + "/PlayerDataFile.json", FileMode.Open);
        byte[] data = new byte[stream.Length];
        stream.Read(data, 0, data.Length);
        stream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        PlayerDataJsonManager.instance.playerData = JsonConvert.DeserializeObject<PlayerDataJsonManager.PlayerData>(jsonData);
    }
    void loadPlayersCharDB()
    {
        FileStream stream = new FileStream(Application.dataPath + "/GameDataJsonManager.playerCharData.json", FileMode.Open);
        byte[] data = new byte[stream.Length];
        stream.Read(data, 0, data.Length);
        stream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        GameDataJsonManager.instance.playerCharData = JsonConvert.DeserializeObject<GameDataJsonManager.PlayerCharData>(jsonData);
    }
    void loadSetting()
    {
        FileStream stream02 = new FileStream(Application.dataPath + "/GameDataJsonManager.dataOfSetting.json", FileMode.Open);
        byte[] data02 = new byte[stream02.Length];
        stream02.Read(data02, 0, data02.Length);
        stream02.Close();
        string jsonData02 = Encoding.UTF8.GetString(data02);
        GameDataJsonManager.instance.dataOfSetting = JsonConvert.DeserializeObject<GameDataJsonManager.DataOfSetting>(jsonData02);
    }


    void loadCharDataIfNull()
    {
        for (int a = 0; a < PlayerDataJsonManager.instance.playerData.charState.Length; a++)
        {
            // 처음 시작하는것 같아.
            if (GameDataJsonManager.instance.dataOfSetting.isGameStartFirst == false)
            {
                Debug.Log("처음 시작이지?");

                PlayerDataJsonManager.instance.playerData.charState[a].name = GameDataJsonManager.instance.playerCharData.charState[a].name;
                PlayerDataJsonManager.instance.playerData.charState[a].hp = GameDataJsonManager.instance.playerCharData.charState[a].hp;
                PlayerDataJsonManager.instance.playerData.charState[a].attackPower = GameDataJsonManager.instance.playerCharData.charState[a].attackPower;
                PlayerDataJsonManager.instance.playerData.charState[a].skillAttackPower = GameDataJsonManager.instance.playerCharData.charState[a].skillAttackPower;
                PlayerDataJsonManager.instance.playerData.charState[a].defencePower = GameDataJsonManager.instance.playerCharData.charState[a].defencePower;
                PlayerDataJsonManager.instance.playerData.charState[a].skillPoint = GameDataJsonManager.instance.playerCharData.charState[a].skillPoint;
                PlayerDataJsonManager.instance.playerData.charState[a].attackRange = GameDataJsonManager.instance.playerCharData.charState[a].attackRange;
                PlayerDataJsonManager.instance.playerData.charState[a].attackSpeed = GameDataJsonManager.instance.playerCharData.charState[a].attackSpeed;
                PlayerDataJsonManager.instance.playerData.charState[a].moveSpeed = GameDataJsonManager.instance.playerCharData.charState[a].moveSpeed;
                PlayerDataJsonManager.instance.playerData.charState[a].criticalPercent = GameDataJsonManager.instance.playerCharData.charState[a].criticalPercent;
                PlayerDataJsonManager.instance.playerData.charState[a].socketName[0] = GameDataJsonManager.instance.playerCharData.charState[a].socketName[0];
                PlayerDataJsonManager.instance.playerData.charState[a].socketName[1] = GameDataJsonManager.instance.playerCharData.charState[a].socketName[1];
                PlayerDataJsonManager.instance.playerData.charState[a].weaponName[0] = GameDataJsonManager.instance.playerCharData.charState[a].weaponName[0];
                PlayerDataJsonManager.instance.playerData.charState[a].weaponName[1] = GameDataJsonManager.instance.playerCharData.charState[a].weaponName[1];

                Debug.Log(a);

                continue;
            }

            //처음 스타트는 아닌데 가지고 있는 케릭이 전무하네?
            if (PlayerDataJsonManager.instance.playerData.haveCharData.charName.Count == 0)
            {
                Debug.Log("처음 스타트는 아닌데 가지고 있는 케릭이 전무하네?");

                PlayerDataJsonManager.instance.playerData.charState[a].name = GameDataJsonManager.instance.playerCharData.charState[a].name;
                PlayerDataJsonManager.instance.playerData.charState[a].hp = GameDataJsonManager.instance.playerCharData.charState[a].hp;
                PlayerDataJsonManager.instance.playerData.charState[a].attackPower = GameDataJsonManager.instance.playerCharData.charState[a].attackPower;
                PlayerDataJsonManager.instance.playerData.charState[a].skillAttackPower = GameDataJsonManager.instance.playerCharData.charState[a].skillAttackPower;
                PlayerDataJsonManager.instance.playerData.charState[a].defencePower = GameDataJsonManager.instance.playerCharData.charState[a].defencePower;
                PlayerDataJsonManager.instance.playerData.charState[a].skillPoint = GameDataJsonManager.instance.playerCharData.charState[a].skillPoint;
                PlayerDataJsonManager.instance.playerData.charState[a].attackRange = GameDataJsonManager.instance.playerCharData.charState[a].attackRange;
                PlayerDataJsonManager.instance.playerData.charState[a].attackSpeed = GameDataJsonManager.instance.playerCharData.charState[a].attackSpeed;
                PlayerDataJsonManager.instance.playerData.charState[a].moveSpeed = GameDataJsonManager.instance.playerCharData.charState[a].moveSpeed;
                PlayerDataJsonManager.instance.playerData.charState[a].criticalPercent = GameDataJsonManager.instance.playerCharData.charState[a].criticalPercent;
                PlayerDataJsonManager.instance.playerData.charState[a].socketName[0] = GameDataJsonManager.instance.playerCharData.charState[a].socketName[0];
                PlayerDataJsonManager.instance.playerData.charState[a].socketName[1] = GameDataJsonManager.instance.playerCharData.charState[a].socketName[1];
                PlayerDataJsonManager.instance.playerData.charState[a].weaponName[0] = GameDataJsonManager.instance.playerCharData.charState[a].weaponName[0];
                PlayerDataJsonManager.instance.playerData.charState[a].weaponName[1] = GameDataJsonManager.instance.playerCharData.charState[a].weaponName[1];

                continue;
            }
        }

 
        //만약 내가 player01, player02, player03을 가지고 있는 상태면
        // 해당 애들만 뺴고 데이터를 복사해 오기.
        for (int i =0; i < PlayerDataJsonManager.instance.playerData.charState.Length; i++)
        {
            bool pass = false;
            for (int k = 0; k < PlayerDataJsonManager.instance.playerData.haveCharData.charName.Count; k++)
            {
                if (PlayerDataJsonManager.instance.playerData.haveCharData.charName[k] == GameDataJsonManager.instance.playerCharData.charState[i].name)
                {
                    pass = true;
                    break;
                }
            }

            if (pass == true) continue;

            PlayerDataJsonManager.instance.playerData.charState[i].name = GameDataJsonManager.instance.playerCharData.charState[i].name;
            PlayerDataJsonManager.instance.playerData.charState[i].hp = GameDataJsonManager.instance.playerCharData.charState[i].hp;
            PlayerDataJsonManager.instance.playerData.charState[i].skillAttackPower = GameDataJsonManager.instance.playerCharData.charState[i].skillAttackPower;
            PlayerDataJsonManager.instance.playerData.charState[i].attackPower = GameDataJsonManager.instance.playerCharData.charState[i].attackPower;
            PlayerDataJsonManager.instance.playerData.charState[i].defencePower = GameDataJsonManager.instance.playerCharData.charState[i].defencePower;
            PlayerDataJsonManager.instance.playerData.charState[i].skillPoint = GameDataJsonManager.instance.playerCharData.charState[i].skillPoint;
            PlayerDataJsonManager.instance.playerData.charState[i].attackRange = GameDataJsonManager.instance.playerCharData.charState[i].attackRange;
            PlayerDataJsonManager.instance.playerData.charState[i].attackSpeed = GameDataJsonManager.instance.playerCharData.charState[i].attackSpeed;
            PlayerDataJsonManager.instance.playerData.charState[i].moveSpeed = GameDataJsonManager.instance.playerCharData.charState[i].moveSpeed;
            PlayerDataJsonManager.instance.playerData.charState[i].criticalPercent = GameDataJsonManager.instance.playerCharData.charState[i].criticalPercent;
            PlayerDataJsonManager.instance.playerData.charState[i].socketName[0] = GameDataJsonManager.instance.playerCharData.charState[i].socketName[0];
            PlayerDataJsonManager.instance.playerData.charState[i].socketName[1] = GameDataJsonManager.instance.playerCharData.charState[i].socketName[1];
            PlayerDataJsonManager.instance.playerData.charState[i].weaponName[0] = GameDataJsonManager.instance.playerCharData.charState[i].weaponName[0];
            PlayerDataJsonManager.instance.playerData.charState[i].weaponName[1] = GameDataJsonManager.instance.playerCharData.charState[i].weaponName[1];
        }

        if (GameDataJsonManager.instance.dataOfSetting.isGameStartFirst == false)
        {
            GameDataJsonManager.instance.dataOfSetting.isGameStartFirst = true;
            saveSettingData();
        }

        savePlayerData();
     }



    public void loadEnemyData()
    {
      
    }
    public void saveData()
    {
           string jsonData = JsonConvert.SerializeObject(GameDataJsonManager.instance.dataOfSetting);
           byte[] data = Encoding.UTF8.GetBytes(jsonData);
           File.WriteAllBytes(Application.dataPath + "/GameDataJsonManager.dataOfSetting.json", data);

        //   string jsonData = JsonConvert.SerializeObject(GameDataJsonManager.instance.dataOfSetting);
        //   byte[] data = Encoding.UTF8.GetBytes(jsonData);
        //   File.WriteAllBytes(Application.dataPath + "/GameDataJsonManager.dataOfSetting.json", data);
    }
}
