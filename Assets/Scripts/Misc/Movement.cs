using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _particleTriggerSpeed = 3f;
    [SerializeField] private Transform _movementParticles;


    private float _moveX;
    private float _moveZ;
    private Vector3 _rotation;
    private bool _canMove = true;

    private Rigidbody _rigidBody;
    private TargetLock _targetLock;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
        _targetLock = GetComponent<TargetLock>();
    }
    private void AllowMove() {
        _canMove = true;
    }
    private void PreventMove() {
        _canMove = false;
    }

    private void FixedUpdate() {
        Move();
        Rotate();
    }

    public void SetCurrentDirection(float currentXDirection, float currentZDirection) {
        _moveX = currentXDirection;
        _moveZ = currentZDirection;
    }

    public void SetCurrentRotation(float currentXRotation, float currentZRotation) {
        if (currentXRotation == 0f && currentZRotation == 0f) return;
        _rotation = new Vector3(currentXRotation, 0f, currentZRotation);
    }

    private void Move() {
        if (!_canMove) { return; }

        Vector3 movement = new Vector3(_moveX * _moveSpeed, 0f, _moveZ * _moveSpeed);
        _rigidBody.velocity = movement;
        CheckMoveParticles(movement);
    }

    private void Rotate() {
        if (_targetLock != null && !_targetLock.IsEnabled()) {
            if (_rotation != Vector3.zero)
                _rigidBody.rotation = Quaternion.LookRotation(_rotation);
        } else {
            RotateWithTargetLock();
        }
    }

    private void RotateWithTargetLock() {
        Transform currentTarget = _targetLock.CurrentTarget;
        Vector3 newDirection = currentTarget.position - _rigidBody.position;

        _rigidBody.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
    }

    private void CheckMoveParticles(Vector3 movement) {
        ParticleSystem particleSystem = _movementParticles.GetComponent<ParticleSystem>();
        if (movement.magnitude > _particleTriggerSpeed) {
            if (!particleSystem.isPlaying) particleSystem.Play();
            _movementParticles.eulerAngles = new Vector3(0f, Vector3.Angle(Vector3.forward, movement.normalized), 0f);
        } else {
            particleSystem.Stop();
        }
        
    }
}
