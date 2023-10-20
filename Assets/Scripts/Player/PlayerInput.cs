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


    private void Awake() {
        _playerInputActions = new PlayerInputActions();

        _move = _playerInputActions.Player.Move;
        _rotate = _playerInputActions.Player.Rotate;
        _shoot = _playerInputActions.Player.Shoot;
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
        };
    }
}

public struct FrameInput {
    public Vector2 Move;
    public Vector2 Rotate;
    public bool Shoot;
}