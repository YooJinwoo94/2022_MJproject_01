using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerDataJsonManager : MonoBehaviour
{
    static int charCount = 10;
    [HideInInspector]
    public static PlayerDataJsonManager instance = null;
    [HideInInspector]
    public PlayerData playerData;

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

        for (int i = 0; i < playerData.charState.Length; i++)
        {
            instance.playerData.charState[i] = new PlayerData.CharState();
        }
    }


    [System.Serializable]
    public  class PlayerData
    {
        public int clearFloorCount = 0;

        public HaveWeaponName haveWeaponName = new HaveWeaponName();
        public HaveSocketName haveSocketName = new HaveSocketName();
        public CharGridData charGridData = new CharGridData();
        public CharState[] charState = new CharState[charCount];
        public HaveCharData haveCharData = new HaveCharData();

        public class HaveCharData
        {
           public List<string> charName = new List<string>();
        }

        public class CharState
        {
            public string name;

            public int hp;
            public int attackPower;
            public int skillAttackPower;
            public int defencePower;
            public int skillPoint;
            public float attackRange;
            public float attackSpeed;
            public float moveSpeed;
            public int criticalPercent;
            public string[] socketName = new string[2];
            public string[] weaponName = new string[2];
            public int love;
            public string atkPriority;

            public CharState(string name = "",
     int hp = 100, int skillPoint = 0 , int attack = 50, int skillAttackPower = 50 , int defence = 30,
     int criticalPercent= 5, float attackRange = 1.5f, float attackSpeed = 1f, float moveSpeed = 1f ,
     int love =1, string socket01 ="", string socket02 = "",
     string weaponName01 = "", string weaponName02 = "", string atkPriority ="")
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

                this.atkPriority = atkPriority;
            }
        }
        public class CharGridData
        {
            public string[,] nowGridData = new string[3, 3];
            public string[,,] allGridData = new string[4, 3, 3];

            public CharGridData(string[,] nowGridData = null , string [,,] allGridData = null)
            {
        
                this.nowGridData = nowGridData;
                this.allGridData = allGridData;
            }
        }


        //가지고 있는 소켓들의 이름
        public class HaveSocketName
        {
            public List<string> socketName = new List<string>();

           public  HaveSocketName(List<string> socketName = null)
            {
                this.socketName = socketName;
            }
        }
        //가지고 있는 무기들의 이름
        public class HaveWeaponName
        {
            public List<string> weaponName = new List<string>();

            public HaveWeaponName(List<string> haveWeaponName = null)
            {
                this.weaponName = haveWeaponName;
            }
        }
    }
}
