using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseClass;
using System;

public class PlayerBase : BCharBase
{
    private GameMgr gameMgr;

    [SerializeField] private PlayerInfo serverPlayerInfo;
    public PlayerState NowState { get; private set;}

    private PlayerMove playMove;
    private PlayerGorundCheck groundCheck;
    private PlayerAttack playerAttack;
    

    private void Start()
    {
        if (GameMgr.ins != null)
            gameMgr = GameMgr.ins;
        else
            Debug.LogError("���ӸŴ����� �����ϴ� �������� ������ �ƴմϴ�.");

        playMove = this.GetComponent<PlayerMove>();
        groundCheck = this.GetComponent<PlayerGorundCheck>();
        playerAttack = this.GetComponent<PlayerAttack>();
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
