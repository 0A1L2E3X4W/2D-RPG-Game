using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    [Header("ANIM BOOL")]
    private string animBoolName;

    [Header("MOVE INPUT")]
    public int xInput = 0;

    [Header("JUMP")]
    public bool isJumping = false;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        xInput = PlayerInputManager.Instance.xInputValue;
        isJumping = PlayerInputManager.Instance.jumpInput;

        player.anim.SetFloat("YVelocity", player.rb.linearVelocityY);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
}
