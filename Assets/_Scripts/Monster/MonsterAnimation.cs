using BaseClass;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    [SerializeField] private Animator monsterAnimator;

    public MonsterAniState NowState
    {
        get; private set;
    }
    public AnimationHandler aniHandler;

    private void Start()
    {
        NowState = new MonsterAniState();
    }

    public void Noraml()
    {
        NowState.moveState = MS_Move.normal;
        NowState.attackState = MS_Attak.normal;

        monsterAnimator.SetBool("walkState", false);
        monsterAnimator.SetBool("attackState", false);
    }

    public void PlayerWalkState(MS_Move MoveState)
    {
        NowState.moveState = MoveState;
        monsterAnimator.SetBool("walkState", MoveState == MS_Move.walk? true : false);
    }

    public void PlayerAttack(MS_Attak AttackState)
    {
        NowState.attackState = AttackState;
        monsterAnimator.SetBool("attackState", AttackState == MS_Attak.Attak ? true : false);
    }
}
