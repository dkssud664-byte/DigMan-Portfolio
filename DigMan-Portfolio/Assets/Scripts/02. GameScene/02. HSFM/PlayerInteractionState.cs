using UnityEngine;

public class PlayerInteractionState : PlayerState
{
    protected override bool CanInteract => false;
    EquipType currentEquipType;
    public PlayerInteractionState(PlayerController player,
        PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        //currentEquipType = Facade.Instance.PlayerManager.PlayerInfo.CurrentEquipment;
    }

    public override void Update()
    {
        base.Update();

        switch (currentEquipType)
        {
            case EquipType.Shovel:
                break;
            case EquipType.Drill:
                break;
            case EquipType.Grenade:
                break;
            case EquipType.Gun:
                break;
            case EquipType.Launcher:
                break;
            case EquipType.Remote:
                break;
            default:
                break;
        }

    }

    public override void Exit()
    {

    }
}
