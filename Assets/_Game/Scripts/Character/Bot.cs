using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    public Rigidbody rb;
    private float yPos;
    private Vector3 startPos;

    public Vector2 MoveDirection { get; private set; }

    //public StateMachine stateMachine { get; private set; }

    public BotIdleState IdleState { get; private set; }
    public BotRunState RunState { get; private set; }
    public BotAttackState AttackState { get; private set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
        stateMachine = new StateMachine();
        IdleState = new BotIdleState(this, CharacterAnimation, "idle", this);
        RunState = new BotRunState(this, CharacterAnimation, "run", this);
        AttackState = new BotAttackState(this, CharacterAnimation, "attack", this);
        stateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public void SetYPosition(float yPosition)
    {
        yPos = yPosition;
    }
}
