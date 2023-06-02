using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerRunState : State
{
    private Player player;

    public PLayerRunState(Character character, Animator anim, string animString, Player player) : base(character, anim, animString)
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

        if (player.MoveDirection.sqrMagnitude < 0.01f)
        {
            player.stateMachine.ChangeState(player.IdleState);
        }
    }
}
