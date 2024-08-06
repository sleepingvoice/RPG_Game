using BaseClass;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;

namespace DataLoad
{

    public class BdataLoad : MonoBehaviour
    {
        BdataDic<MonsterInfo> Monsterdic;

        public void LoadData(GetTitleDataResult result, ref Dictionary<string, MonsterInfo> MonsterDic,ref Dictionary<string, PlayerInfo> PlayerDic)
        {
            MonsterDic = Monsterdic.JsonToDic(result.Data["Monster"]);
        }
    }
}
