using BaseClass;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator PlayerAnimator;

    public PlayerAniState NowState
    {
        get; private set;
    }
    public AnimationHandler aniHandler;

    private void Start()
    {
        NowState = new PlayerAniState();
    }

    public void Noraml()
    {
        NowState.moveState = PS_Move.normal;
        NowState.attackState = PS_Attack.normal;

        PlayerAnimator.SetInteger("walkState", 0);
        PlayerAnimator.SetInteger("attackState", 0);
    }

    public void PlayerWalkState(PS_Move MoveState)
    {
        NowState.moveState = MoveState;
        PlayerAnimator.SetInteger("walkState", (int)MoveState);
    }

    public void PlayerAttack(PS_Attack AttackState)
    {
        NowState.attackState = AttackState;
        PlayerAnimator.SetInteger("attackState", (int)AttackState);
    }

}
