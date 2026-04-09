using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GamePlayState State { get; private set; } = GamePlayState.Playing;

    public bool CanPlay => State == GamePlayState.Playing;


    private void Awake()
    {
        Facade.Instance.SetGameManager(this);
    }

    public void SetState(GamePlayState state)
    {
        State = state;
    }
}
