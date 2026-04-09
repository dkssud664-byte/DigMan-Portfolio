using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected Vector2 move;
    protected virtual bool CanInteract => true;

    protected PlayerState(PlayerController player,
        PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() {
    }
    public virtual void Update()
    {
        move = player.Input.Move;
        player.ApplyGravity();
        player.Move(move);

        int index = player.Input.GetNumberKeyDown();
        if (index != -1)
        {
            player.TrySelectEquipByIndex(index);
        }
    }
    public virtual void Exit() { }
}