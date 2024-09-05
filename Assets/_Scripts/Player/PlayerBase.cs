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
            Debug.LogError("게임매니저가 없습니다 정상적인 접근이 아닙니다.");
    }

}
