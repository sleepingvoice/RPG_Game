using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseClass;
using System;

public class PlayerBase : BCharBase
{
    private GameMgr gameMgr;

    [SerializeField] private PlayerInfo serverPlayerInfo;

    [SerializeField] private PlayerMove playMove;
    [SerializeField] private PlayerGorundCheck grounCheck;
    [SerializeField] private PlayerAttack playerAttack;
    

    private void Start()
    {
        if (GameMgr.ins != null)
            gameMgr = GameMgr.ins;
        else
            Debug.LogError("���ӸŴ����� �����ϴ� �������� ������ �ƴմϴ�.");
    }

    private void InitPlayer(PlayerInfo Info)
    {
        serverPlayerInfo = Info;
        baseCharInfo = serverPlayerInfo.playerChar;
    }

    private void AttackSet()
    {
        Action<MonsterBase> AddAction = (MonsterBase target) => {
            target.Dameged(baseCharInfo.atk);
        };
        playerAttack.SetWeaponDamageAct(AddAction);
    }
}
