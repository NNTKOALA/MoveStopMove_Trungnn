using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotIdleState : State
{
    private Bot bot;

    public BotIdleState(Character character, Animator anim, string animString, Bot bot) : base(character, anim, animString)
    {
        this.bot = bot;
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

        if (bot.FindClosetEnemy() != null)
        {
            bot.stateMachine.ChangeState(bot.AttackState);
        }
        else
        {
            bot.stateMachine.ChangeState(bot.RunState);
        }
    }
}
