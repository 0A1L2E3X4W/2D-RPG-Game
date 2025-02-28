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
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }

    [Header("MOVEMENT SETTING")]
    public float moveSpeed = 6f;

    [Header("JUMP SETTING")]
    public float jumpForce = 12;

    [Header("COLLISION SETTING")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("FACING")]
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    private void Awake()
    {
        stateMachine = new();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        idleState = new(this, stateMachine, "Idle");
        moveState = new(this, stateMachine, "Move");
        jumpState = new(this, stateMachine, "Jump");
        airState = new(this, stateMachine, "Jump");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

    // GIZMOS COLLISION CHECK
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    // FLIP
    private void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _xVelocity)
    {
        if (_xVelocity > 0 && !facingRight)
            Flip();
        else if (_xVelocity < 0 && facingRight)
            Flip();
    }
}
