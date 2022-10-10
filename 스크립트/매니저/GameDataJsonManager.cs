using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataJsonManager : MonoBehaviour
{
    [HideInInspector]
    public static GameDataJsonManager instance = null;
    [HideInInspector]
    public EnemyCharData enemyData;
    [HideInInspector]
    public PlayerCharData playerCharData;
    [HideInInspector]
    public DataOfSetting dataOfSetting;

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

        dataOfSetting = new DataOfSetting();
        for (int i = 0; i < enemyData.charState.Length; i++)
        {
            enemyData.charState[i] = new EnemyCharData.CharState();
        }
        for (int i = 0; i < playerCharData.charState.Length; i++)
        {
            playerCharData.charState[i] = new PlayerCharData.CharState();
        }
    }



    [System.Serializable]
    public class DataOfSetting
    {
        public bool isGameStartFirst = false;
        public int clearFloorCount = 0;
        public int playerCharCount = 10;
    }


    [System.Serializable]
    public class EnemyCharData
    {
        public CharState[] charState = new CharState[10];

        public class CharState
        {
            public string name;

            public int hp;
            public int attackPower;
            public int defencePower;
            public int skillPoint;
            public float attackRange;
            public float attackSpeed;
            public float moveSpeed;
            public int criticalPercent;
            public int love = 1;
            public string[] socketName = new string[2];
            public string[] weaponName = new string [2];

            public CharState(string name = "",
       int hp = 100, int skillPoint = 0, int attack = 50, int defence = 30,
       int criticalPercent = 5, float attackRange = 1.5f, float attackSpeed = 1f, float moveSpeed = 1f,
       int love = 1, string socket01 = "", string socket02 = "",
       string weaponName01 = "", string weaponName02 = "")
            {
                this.name = name;

                this.hp = hp;
                this.skillPoint = skillPoint;

                this.attackPower = attack;
                this.defencePower = defence;
                this.criticalPercent = criticalPercent;

                this.attackRange = attackRange;
                this.attackSpeed = attackSpeed;
                this.moveSpeed = moveSpeed;
                this.love = love;

                this.socketName[0] = socket01;
                this.socketName[1] = socket02;
                this.weaponName[0] = weaponName01;
                this.weaponName[1] = weaponName02;
            }
        }
    }


    [System.Serializable]
    public class PlayerCharData
    {
        public CharState [] charState = new CharState[10];
   
        public class CharState
        {
            public string name = "player01";

            public int hp = 100;
            public int attackPower = 50;
            public int skillAttackPower = 50;
            public int defencePower = 30;
            public int skillPoint = 0;
            public float attackRange = 1.5f;
            public float attackSpeed = 1;
            public float moveSpeed = 1;
            public int criticalPercent = 5;
            public string[] socketName = new string[2];
            public string[] weaponName = new string[2];
            public int love = 1;


            public CharState(string name = "",
       int hp = 100, int skillPoint = 0, int attack = 50, int skillAttackPower = 50 ,int defence = 30,
       int criticalPercent = 5, float attackRange = 1.5f, float attackSpeed = 1f, float moveSpeed = 1f,
       int love = 1, string socket01 = "", string socket02 = "",
       string weaponName01 = "", string weaponName02 = "")
            {
               this.name = name;

                this.hp = hp;
                this.skillPoint = skillPoint;
                this.attackPower = attack;
                this.skillAttackPower = skillAttackPower;
                this.defencePower = defence;
                this.criticalPercent = criticalPercent;
                this.attackRange = attackRange;
                this.attackSpeed = attackSpeed;
                this.moveSpeed = moveSpeed;
                this.love = love;

                this.socketName[0] = socket01;
                this.socketName[1] = socket02;

                this.weaponName[0] = weaponName01;
                this.weaponName[1] = weaponName02;
            }
        }
    }
}
