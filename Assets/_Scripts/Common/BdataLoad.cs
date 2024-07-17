using BaseClass;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;

namespace DataLoad
{
    public class MonsterClass
    {
        public string id;
        public MonsterInfo info;
    }

    public class PlayerClass
    {
        public string id;
        public MonsterInfo info;
    }


    public class BdataLoad : MonoBehaviour
    {
        BDataDic<MonsterInfo> Monsterdic;

        public void LoadData(GetTitleDataResult result, ref Dictionary<string, MonsterInfo> MonsterDic,ref Dictionary<string, PlayerInfo> PlayerDic)
        {
            MonsterDic = Monsterdic.JsonToDic(result.Data["Monster"]);
        }
    }
}
