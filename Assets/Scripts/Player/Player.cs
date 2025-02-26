using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("ANIMATOR")]
    [HideInInspector] public Animator anim { get; private set; }

    [Header("RIGIDBODY")]
    [HideInInspector] public Rigidbody2D rb { get; private set; }

    [Header("PLAYER STATE MACHINE")]
    public PlayerStateMachine stateMachine { get; private set; }

    [Header("PLAYER STATE")]
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    [Header("MOVEMENT SETTING")]
    public float moveSpeed = 6f;

    private void Awake()
    {
        stateMachine = new();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        idleState = new(this, stateMachine, "Idle");
        moveState = new(this, stateMachine, "Move");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float _x, float _y)
    {
        rb.linearVelocity = new(_x, _y);
    }
}
