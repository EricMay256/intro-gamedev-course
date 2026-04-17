using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
  public static PlayerInputManager Instance;
  public Vector2 Movement;
  public Vector2 LookDelta;
  public bool AttackPressed { get; private set; }
  public bool AttackHeld { get; private set; }
  public bool AttackReleased { get; private set; }
  public bool JumpPressed { get; private set; }
  public bool JumpHeld { get; private set; }
  public bool JumpReleased { get; private set; }
  public bool InteractPressed { get; private set; }
  public bool InteractHeld { get; private set; }
  public bool InteractReleased { get; private set; }
  public bool CrouchPressed { get; private set; }
  public bool CrouchHeld { get; private set; }
  public bool CrouchReleased { get; private set; }
  public bool SprintPressed { get; private set; }
  public bool SprintHeld { get; private set; }
  public bool SprintReleased { get; private set; }
  public bool PausePressed { get; private set; }
  public bool PauseHeld { get; private set; }
  public bool PauseReleased { get; private set; }
  public bool GroundPressed { get; private set; }
  public bool GroundHeld { get; private set; }
  public bool GroundReleased { get; private set; }
  public bool DamagePressed { get; private set; }
  public bool DamageHeld { get; private set; }
  public bool DamageReleased { get; private set; }
  public bool WallslidePressed { get; private set; }
  public bool WallslideHeld { get; private set; }
  public bool WallslideReleased { get; private set; }
  private InputAction _attackAction, _jumpAction, _interactAction, _crouchAction, _sprintAction;
  private InputAction _pauseActionPlayer, _pauseActionUI;
  private InputAction _movementAction, _lookAction;
  private InputAction _groundAction, _damageAction, _wallslideAction;
  public PlayerInput PlayerInput { get; private set; }
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
    PlayerInput = GetComponent<PlayerInput>();
  }

  void Start()
  {
    _movementAction = PlayerInput.actions["Move"];
    _lookAction = PlayerInput.actions["Look"];
    _attackAction = PlayerInput.actions["Attack"];
    _jumpAction = PlayerInput.actions["Jump"];
    _interactAction = PlayerInput.actions["Interact"];
    _crouchAction = PlayerInput.actions["Crouch"];
    _sprintAction = PlayerInput.actions["Sprint"];
    _pauseActionPlayer = PlayerInput.actions["Player/Pause"];
    _pauseActionUI = PlayerInput.actions["UI/Pause"];
    _groundAction = PlayerInput.actions["Ground"];
    _damageAction = PlayerInput.actions["Damage"];
    _wallslideAction = PlayerInput.actions["Wallslide"];
  }

  // Update is called once per frame
  void Update()
  {
    AttackPressed = _attackAction.WasPressedThisFrame();
    AttackHeld = _attackAction.IsPressed();
    AttackReleased = _attackAction.WasReleasedThisFrame();

    JumpPressed = _jumpAction.WasPressedThisFrame();
    JumpHeld = _jumpAction.IsPressed();
    JumpReleased = _jumpAction.WasReleasedThisFrame();

    InteractPressed = _interactAction.WasPressedThisFrame();
    InteractHeld = _interactAction.IsPressed();
    InteractReleased = _interactAction.WasReleasedThisFrame();

    CrouchPressed = _crouchAction.WasPressedThisFrame();
    CrouchHeld = _crouchAction.IsPressed();
    CrouchReleased = _crouchAction.WasReleasedThisFrame();

    SprintPressed = _sprintAction.WasPressedThisFrame();
    SprintHeld = _sprintAction.IsPressed();
    SprintReleased = _sprintAction.WasReleasedThisFrame();

    GroundPressed = _groundAction.WasPressedThisFrame();
    GroundHeld = _groundAction.IsPressed();
    GroundReleased = _groundAction.WasReleasedThisFrame();

    DamagePressed = _damageAction.WasPressedThisFrame();
    DamageHeld = _damageAction.IsPressed();
    DamageReleased = _damageAction.WasReleasedThisFrame();

    WallslidePressed = _wallslideAction.WasPressedThisFrame();
    WallslideHeld = _wallslideAction.IsPressed();
    WallslideReleased = _wallslideAction.WasReleasedThisFrame();

    PausePressed = _pauseActionPlayer.WasPressedThisFrame() || _pauseActionUI.WasPressedThisFrame();
    PauseHeld = _pauseActionPlayer.IsPressed() || _pauseActionUI.IsPressed();
    PauseReleased = _pauseActionPlayer.WasReleasedThisFrame() || _pauseActionUI.WasReleasedThisFrame();

    Movement = _movementAction.ReadValue<Vector2>();
    LookDelta = _lookAction.ReadValue<Vector2>();
  }
}
