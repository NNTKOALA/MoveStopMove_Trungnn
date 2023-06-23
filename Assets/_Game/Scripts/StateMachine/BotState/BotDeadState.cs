using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDeadState : State
{
    private Bot bot;
    private float timer = 3f;

    public BotDeadState(Character character, Animator anim, string animString, Bot bot) : base(character, anim, animString)
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

        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            if (bot != null)
            {
                bot.OnDead();
            }
        }
    }
}
