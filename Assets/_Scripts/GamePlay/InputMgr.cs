using UnityEngine;
using UnityEngine.InputSystem;

public class InputMgr : MonoBehaviour
{
    public Vector2 MoveInput
    {
        get; private set;
    }
    public float RotateInput
    {
        get; private set;
    }
    public bool JumpInput
    {
        get; private set;
    }
    public bool AttackInput
    {
        get; private set;
    }
    public bool RunInput
    {
        get; private set;
    }

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _rotateAction;
    private InputAction _attackAction;
    private InputAction _jumpAction;
    private InputAction _runAction;

    private void Awake()
    {
        _playerInput = this.GetComponent<PlayerInput>();

        SetupInputActions();
    }

    private void Update()
    {
        UpdataInputs();
    }

    private void SetupInputActions()
    {

        if (_playerInput == null)
            return;

        _moveAction = _playerInput.actions["Move"];
        _rotateAction = _playerInput.actions["Rotate"];
        _attackAction = _playerInput.actions["Attack"];
        _jumpAction = _playerInput.actions["Jump"];
        _runAction = _playerInput.actions["Run"];
    }

    private void UpdataInputs()
    {
        if (_playerInput == null)
            return;

        MoveInput = _moveAction.ReadValue<Vector2>();
        RotateInput = _rotateAction.ReadValue<float>();
        AttackInput = _attackAction.ReadValue<float>() != 0 ? true : false;
        JumpInput = _jumpAction.ReadValue<float>() != 0 ? true : false;
        RunInput = _runAction.ReadValue<float>() != 0 ? true : false;
    }
}
