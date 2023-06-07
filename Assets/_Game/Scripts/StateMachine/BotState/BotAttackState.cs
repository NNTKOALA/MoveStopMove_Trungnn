using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackState : State
{
    private Bot bot;
    private Character target;

    private float timer;

    public BotAttackState(Character character, Animator anim, string animString, Bot bot) : base(character, anim, animString)
    {
        this.bot = bot;
    }

    public override void Enter()
    {
        base.Enter();
        target = bot.FindClosetEnemy();
        if (target == null)
        {
            bot.stateMachine.ChangeState(bot.IdleState);
        }
        else
        {
            timer = 1.1f;
            bot.ThrowWeapon();
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
        if (timer < 0f)
        {
            bot.stateMachine.ChangeState(bot.IdleState);
        }

        if (bot.MoveDirection.sqrMagnitude > 0.01f)
        {
            bot.stateMachine.ChangeState(bot.RunState);
        }
    }
}
