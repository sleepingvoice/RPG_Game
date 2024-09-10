using BaseClass;
using System;
using UnityEngine;

public class PlayerBase : BCharBase
{
    private GameMgr gameMgr;

    [SerializeField] private PlayerInfo serverPlayerInfo;


    private PlayerMove playMove;
    private PlayerGorundCheck groundCheck;
    private PlayerAttack playerAttack;


    private void Start()
    {
        if (GameMgr.ins != null)
            gameMgr = GameMgr.ins;
        else
            Debug.LogError("게임매니저가 없습니다 정상적인 접근이 아닙니다.");

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
        Action<MonsterBase> AddAction = (MonsterBase target) =>
        {
            target.Dameged(baseCharInfo.atk);
        };
        playerAttack.SetWeaponDamageAct(AddAction);
    }
}
