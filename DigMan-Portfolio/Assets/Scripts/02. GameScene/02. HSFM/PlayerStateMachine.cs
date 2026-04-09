public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }
    private bool transitionedThisFrame;

    public void Initialize(PlayerState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        //if (transitionedThisFrame)
        //    return;

        if (CurrentState == newState)
            return;

        transitionedThisFrame = true;

        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void LateUpdate()
    {
        transitionedThisFrame = false;
    }
}
