using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRunState : State
{
    private Bot bot;

    public BotRunState(Character character, Animator anim, string animString, Bot bot) : base(character, anim, animString)
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

        if (bot.MoveDirection.sqrMagnitude < 0.01f)
        {
            bot.stateMachine.ChangeState(bot.IdleState);
        }
    }
}
