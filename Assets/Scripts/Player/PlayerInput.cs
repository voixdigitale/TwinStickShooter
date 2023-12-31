using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public FrameInput FrameInput { get; private set; }

    private PlayerInputActions _playerInputActions;
    private InputAction _move;
    private InputAction _rotate;
    private InputAction _shoot;
    private InputAction _targetLock;
    private InputAction _pause;


    private void Awake() {
        _playerInputActions = new PlayerInputActions();

        _move = _playerInputActions.Player.Move;
        _rotate = _playerInputActions.Player.Rotate;
        _shoot = _playerInputActions.Player.Shoot;
        _targetLock = _playerInputActions.Player.TargetLock;
        _pause = _playerInputActions.Player.Pause;
    }

    private void OnEnable() {
        _playerInputActions.Enable();
    }

    private void OnDisable() {
        _playerInputActions.Disable();
    }

    private void Update() {
        FrameInput = GatherInput();
    }

    private FrameInput GatherInput() {
        return new FrameInput {
            Move = _move.ReadValue<Vector2>(),
            Rotate = _rotate.ReadValue<Vector2>(),
            Shoot = _shoot.ReadValue<float>() > 0,
            TargetLock = _targetLock.ReadValue<float>() > 0,
            Pause = _pause.ReadValue<float>() > 0,
        };
    }
}

public struct FrameInput {
    public Vector2 Move;
    public Vector2 Rotate;
    public bool Shoot;
    public bool TargetLock;
    public bool Pause;
}