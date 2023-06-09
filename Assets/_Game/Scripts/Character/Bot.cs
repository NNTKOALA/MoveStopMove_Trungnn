using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public static event EventHandler<OnAnyEnemyDeathArgs> onAnyEnemyDeath;
    public class OnAnyEnemyDeathArgs : EventArgs
    {
        public Bot bot;
        public Character damageDealer;
    }

    [SerializeField] float patrolRadius = 7f;
    public float PatrolRadius => patrolRadius;
    public NavMeshAgent agent;
    public Rigidbody rb;
    private float yPos;
    private Vector3 startPos;


    public Vector2 MoveDirection { get; private set; }
    public bool IsPause { get; set; }

    //public StateMachine stateMachine { get; private set; }

    public BotIdleState IdleState { get; private set; }
    public BotRunState RunState { get; private set; }
    public BotAttackState AttackState { get; private set; }

    public BotDeadState DeadState { get; private set; }
    public BotWinState WinState { get; private set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        int weaponRd = UnityEngine.Random.Range(0, 11);
        SetWeapon((EWeaponType)weaponRd);
        startPos = transform.position;
        stateMachine = new StateMachine();
        IdleState = new BotIdleState(this, CharacterAnimation, "idle", this);
        RunState = new BotRunState(this, CharacterAnimation, "run", this);
        AttackState = new BotAttackState(this, CharacterAnimation, "attack", this);
        DeadState = new BotDeadState(this, CharacterAnimation, "dead", this);
        WinState = new BotWinState(this, CharacterAnimation, "win");
        stateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (IsPause)
        {
            agent.isStopped = true;
            return;
        }
        else
        {
            agent.isStopped = false;
        }
        base.Update();
    }

    public void SetYPosition(float yPosition)
    {
        yPos = yPosition;
    }


    public void SetDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
    }

    public override void ReleaseSelf()
    {
        stateMachine.ChangeState(IdleState);
        base.ReleaseSelf();
        WaypointManager.Instance.RemoveIndicator(this);
    }

    public override void TakeDamage(Character damageDealer)
    {
        base.TakeDamage(damageDealer);
        stateMachine.ChangeState(DeadState);
        base.ChangeScale(damageDealer);
        onAnyEnemyDeath?.Invoke(this, new OnAnyEnemyDeathArgs { bot=this, damageDealer = damageDealer });
    }

}
