using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerIdleState : State
{
    private Player player;

    public PLayerIdleState(Character character, Animator anim, string animString, Player player) : base(character, anim, animString)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();

        if (player.MoveDirection.sqrMagnitude > 0.01f)
        {
            player.stateMachine.ChangeState(player.RunState);
        }

        if (player.FindClosetEnemy() != null)
        {
            player.stateMachine.ChangeState(player.AttackState);
        }
    }
}
