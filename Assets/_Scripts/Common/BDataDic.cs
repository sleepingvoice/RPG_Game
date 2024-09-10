using System;
using System.Collections.Generic;
using UnityEngine;

public class BdataDic<T>
{
    [Serializable]
    public class LoadDataClass
    {
        public string id;
        public T info;
    }

    [Serializable]
    public class LoadDataList
    {
        public List<LoadDataClass> List;
    }

    public Dictionary<string, T> JsonToDic(string Json)
    {
        var DataList = JsonUtility.FromJson<LoadDataList>(Json);

        if (DataList == null) // ���� ���ٸ� ��ͱ״�� ������
            return new Dictionary<string, T>();

        Dictionary<string, T> TempDic = new Dictionary<string, T>();

        foreach (var data in DataList.List)
        {
            TempDic.Add(data.id, data.info);
        }
        return TempDic;
    }
}
