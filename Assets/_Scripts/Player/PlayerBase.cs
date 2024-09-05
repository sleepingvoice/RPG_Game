using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseClass;

public class PlayerBase : MonoBehaviour
{
    private GameMgr gameMgr;
    private PlayerInfo infoPlayer;

    private void Start()
    {
        if (GameMgr.ins != null)
            gameMgr = GameMgr.ins;
        else
            Debug.LogError("���ӸŴ����� �����ϴ� �������� ������ �ƴմϴ�.");
    }

}
