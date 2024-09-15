using BaseClass;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

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

        playerAnimator.SetInteger("walkState", 0);
        playerAnimator.SetInteger("attackState", 0);
    }

    public void PlayerWalkState(PS_Move MoveState)
    {
        NowState.moveState = MoveState;
        playerAnimator.SetInteger("walkState", (int)MoveState);
    }

    public void PlayerAttack(PS_Attack AttackState)
    {
        NowState.attackState = AttackState;
        playerAnimator.SetInteger("attackState", (int)AttackState);
    }

}
