using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] FloatingJoystick variableJoystick;

    public Rigidbody rb;

    private float yPos;
    private Vector3 startPos;
    public Vector2 MoveDirection {  get; private set; }

    
    public PLayerIdleState IdleState { get; private set; }
    public PLayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PLayerDeadState DeadState { get; private set; }
    public PlayerWinState WinState { get; private set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        startPos = transform.position;
        stateMachine = new StateMachine();
        IdleState = new PLayerIdleState(this, CharacterAnimation, "idle", this);
        RunState = new PLayerRunState(this, CharacterAnimation, "run", this);
        AttackState = new PlayerAttackState(this, CharacterAnimation, "attack", this);
        DeadState = new PLayerDeadState(this, CharacterAnimation, "dead");
        WinState = new PlayerWinState(this, CharacterAnimation, "win"); 

        stateMachine.Initialize(IdleState);


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(variableJoystick.Horizontal) > 0.1f || Mathf.Abs(variableJoystick.Vertical) > 0.1f)
            {
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(variableJoystick.Horizontal, 0f, variableJoystick.Vertical));
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.fixedDeltaTime * 30f);
            }

            MoveDirection = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical);
            
            this.transform.position = new Vector3(this.transform.position.x + variableJoystick.Horizontal * moveSpeed * Time.deltaTime,
                yPos, this.transform.position.z + variableJoystick.Vertical * moveSpeed * Time.fixedDeltaTime);
        }
        else  
        {
            MoveDirection = Vector2.zero;
        }
    }

    public void SetYPosition(float yPosition)
    {
        yPos = yPosition;
    }

    public override void TakeDamage(Character damageDealer)
    {
        base.TakeDamage(damageDealer);
        stateMachine.ChangeState(DeadState);
        base.ChangeScale(damageDealer);        
    }

    public override void OnNewGame()
    {
        base.OnNewGame();
        transform.position = startPos;
        stateMachine.ChangeState(IdleState);
    }
}
