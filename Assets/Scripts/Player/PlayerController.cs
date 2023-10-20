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

    public void Awake() {
        if (Instance == null) { Instance = this; }

        _rigidBody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _movement = GetComponent<Movement>();
    }

    private void Update() {
        GatherInput();
        Movement();
    }

    private void GatherInput() {
        _frameInput = _playerInput.FrameInput;
    }

    private void Movement() {
        _movement.SetCurrentDirection(_frameInput.Move.x, _frameInput.Move.y);
        _movement.SetCurrentRotation(_frameInput.Rotate.x, _frameInput.Rotate.y);
    }
}
