using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerDeadState : State
{
    private float timer;

    public PLayerDeadState(Character character, Animator anim, string animString) : base(character, anim, animString)
    {
    }

    public override void Enter()
    {
        base.Enter();

        timer = 1f;
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
            character.OnDead();
        }
    }
}
