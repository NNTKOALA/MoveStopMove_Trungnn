using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackState : State
{
    private Player player;
    private Character target;

    private float timer;

    public PlayerAttackState(Character character, Animator anim, string animString, Player player) : base(character, anim, animString)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        target = player.FindClosetEnemy();
        if (target == null)
        {
            player.stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            timer = 1.1f;
            player.ThrowWeapon();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();

        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            player.stateMachine.ChangeState(player.IdleState);
        }

        if (player.MoveDirection.sqrMagnitude > 0.01f)
        {
            player.stateMachine.ChangeState(player.RunState);
        }
    }
}
