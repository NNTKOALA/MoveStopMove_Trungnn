using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Character character;
    protected Animator anim;

    private string animString;

    public State(Character character, Animator anim, string animString)
    {
        this.character = character;
        this.anim = anim;
        this.animString = animString;
    }

    public virtual void Enter()
    {
        anim.SetBool(animString, true);
    }

    public virtual void Tick()
    {

    }

    public virtual void Exit()
    {
        anim.SetBool(animString, false);
    }
}
