using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerDeadState : State
{
    public PLayerDeadState(Character character, Animator anim, string animString) : base(character, anim, animString)
    {
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
    }
}
