using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 MoveInput => _frameInput.Move;

    public static PlayerController Instance;

    private PlayerInput _playerInput;
    private FrameInput _frameInput;
    private Rigidbody _rigidBody;
    private Movement _movement;
    private Shooting _shooting;
    private TargetLock _targetLock;

    public void Awake() {
        if (Instance == null) { Instance = this; }

        _rigidBody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _movement = GetComponent<Movement>();
        _shooting = GetComponent<Shooting>();
        _targetLock = GetComponent<TargetLock>();
    }

    private void Update() {
        GatherInput();
        Movement();
        Shooting();
        TargetLock();
    }

    private void GatherInput() {
        _frameInput = _playerInput.FrameInput;
    }

    private void Movement() {
        _movement.SetCurrentDirection(_frameInput.Move.x, _frameInput.Move.y);
        _movement.SetCurrentRotation(_frameInput.Rotate.x, _frameInput.Rotate.y);
    }

    private void Shooting() {
        _shooting.SetTrigger(_frameInput.Shoot);
    }

    private void TargetLock() {
        if (_frameInput.TargetLock)
            _targetLock.Trigger();
    }
}
