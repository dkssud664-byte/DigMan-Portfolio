using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputSystem Input { get; private set; }
    public PlayerInfo PlayerInfo { get; private set; }
    public PlayerStatsSystem PlayerStatsSystem { get; private set; }

    //이동
    public CharacterController CharacterController { get; private set; }
    Vector3 velocity;
    public float Speed { get; private set; }
    public float JumpPower { get; private set; }
    public float Gravity { get; private set; }

    //제트팩
    public JetpackAbility JetpackAbility { get; private set; }
    private bool isJetpacking;


    #region 상태
    private PlayerStateMachine playerStateMachine;
    #region Ground
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerWalkState PlayerWalkState { get; private set; }
    public PlayerRunState PlayerRunState { get; private set; }
    #endregion
    #region Air
    public PlayerJumpState PlayerJumpState { get; private set; }
    public PlayerJetpackState PlayerJetpackState { get; private set; }
    #endregion
    #region Interaction
    public PlayerInteractionState PlayerInteractionState { get; private set; }
    public PlayerShovelState PlayerShovelState { get; private set; }
    public PlayerDrillState PlayerDrillState { get; private set; }
    public PlayerGrenadeState PlayerGrenadeState { get; private set; }
    public PlayerGunState PlayerGunState { get; private set; }
    public PlayerLauncherState PlayerLauncherState { get; private set; }
    public PlayerRemoteState PlayerRemoteState { get; private set; }
    public PlayerEquipSwapState PlayerEquipSwapState { get; private set; }
    #endregion
    #region Action
    public PlayerTakeDamageState PlayerTakeDamageState { get; private set; }
    public PlayerDieState PlayerDieState { get; private set; }
    #endregion
    #endregion

    //UI
    private PlayerCanvas playerCanvas;

    //GameManager
    private GameManager gameManager;

    private void Awake()
    {
        //입력
        Input = Facade.Instance.InputSystem;

        //이동
        CharacterController = GetComponent<CharacterController>();

        //상태
        playerStateMachine = new PlayerStateMachine();

        //정보
        //PlayerInfo = GetComponent<PlayerInfo>();

        //플레이어 시스템
        PlayerStatsSystem = new PlayerStatsSystem();

        gameManager = Facade.Instance.GameManager;
    }

    void Start()
    {
        //초기화
        SetSpeed(PlayerInfo.Speed);
        SetJumpPower(PlayerInfo.JumpPower);
        SetGravity(PlayerInfo.Gravity);

        CreateStates(playerStateMachine);
        playerStateMachine.Initialize(PlayerIdleState);
        PlayerStatsSystem.Init(PlayerInfo);

        //제트팩
        JetpackAbility = new JetpackAbility(this, PlayerStatsSystem);
    }

    void Update()
    {
        if(!gameManager.CanPlay)
        {
            return;
        }

        playerStateMachine.CurrentState.Update();   //상태
        JetpackAbility.Update();                    //제트팩

        if(UnityEngine.Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log($"stamina {PlayerStatsSystem.CurrentStamina}");
        }

        PlayerStatsSystem.RecoverStamina(Time.deltaTime);
    }

    private void LateUpdate()
    {
        playerCanvas?.UpdateDirectionIcon(this.transform);
    }
    public void SetPlayerInfo(PlayerInfo info)
    {
        if(info == null)
        {
            return;
        }

        this.PlayerInfo = info;
    }

    #region 제트팩
    public void StartJetpack()
    {
        isJetpacking = true;
    }

    public void StopJetpack()
    {
        isJetpacking = false;
    }

    public void JetpackMove()
    {
        if (PlayerStatsSystem.CurrentStamina <= 0f)
            return;

        Vector3 velocity = Vector3.up * PlayerInfo.JetpackPower;
        Debug.Log($"velocity {velocity}");
        Debug.Log($"PlayerInfo.JetpackPower {PlayerInfo.JetpackPower}");
        CharacterController.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region 초기화
    public void SetSpeed(float speed)
    {
        this.Speed = speed;
    }

    public void SetJumpPower(float jumpPower)
    {
        this.JumpPower = jumpPower;
    }

    public void SetGravity(float gravity)
    {
        this.Gravity = gravity;
    }

    public void CreateStates(PlayerStateMachine playerStateMachine)
    {
        //Ground
        PlayerIdleState = new PlayerIdleState(this, playerStateMachine);
        PlayerWalkState = new PlayerWalkState(this, playerStateMachine);
        PlayerRunState = new PlayerRunState(this, playerStateMachine);

        //Air
        PlayerJumpState = new PlayerJumpState(this, playerStateMachine);
        PlayerJetpackState = new PlayerJetpackState(this, playerStateMachine);

        //Interaction
        PlayerInteractionState = new PlayerInteractionState(this, playerStateMachine);
        PlayerShovelState = new PlayerShovelState(this, playerStateMachine);
        PlayerDrillState = new PlayerDrillState(this, playerStateMachine);
        PlayerGrenadeState = new PlayerGrenadeState(this, playerStateMachine);
        PlayerGunState = new PlayerGunState(this, playerStateMachine);
        PlayerLauncherState = new PlayerLauncherState(this, playerStateMachine);
        PlayerRemoteState = new PlayerRemoteState(this, playerStateMachine);
        PlayerEquipSwapState = new PlayerEquipSwapState(this, playerStateMachine);

        //Action
        PlayerTakeDamageState = new PlayerTakeDamageState(this,playerStateMachine);
        PlayerDieState = new PlayerDieState(this, playerStateMachine);
    }

    #endregion

    public void Move(Vector2 input)
    {
        Vector3 horizontal =
        transform.forward * input.y +
        transform.right * input.x;

        Vector3 velocityY = Vector3.up * velocity.y;

        Vector3 finalMove = horizontal * Speed + velocityY;

        CharacterController.Move(finalMove * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        if (CharacterController.isGrounded && velocity.y <= 0f)
        {
            velocity.y = -2f;
        }

        velocity.y += Gravity * Time.deltaTime;
    }
    public void Jump()
    {
        //-2는 확실한 지면체크와 경사에서 튕김 방지하기 위해 사용한다.
        velocity.y = Mathf.Sqrt(JumpPower * -2f * Gravity);
    }

    public void SetPlayerCanvas(PlayerCanvas playerCanvas)
    {
        if(playerCanvas == null)
        {
            return;
        }

        this.playerCanvas = playerCanvas;
    }

    public void TrySelectEquipByIndex(int index)
    {
        var unlocked = PlayerInfo.PlayerUnlockEquipData.unlockedEquip;

        if (index < 0 || index >= unlocked.Count)
            return;

        EquipType selected = unlocked.ElementAt(index);
        PlayerInfo.SetCurrentEquip(selected);
    }

}
