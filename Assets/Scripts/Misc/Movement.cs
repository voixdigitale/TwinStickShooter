using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;


    private float _moveX;
    private float _moveZ;
    private Vector3 _rotation;
    private bool _canMove = true;

    private Rigidbody _rigidBody;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void AllowMove() {
        _canMove = true;
    }
    private void PreventMove() {
        _canMove = false;
    }

    private void FixedUpdate() {
        Move();
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
        if (_rotation != Vector3.zero) _rigidBody.rotation = Quaternion.LookRotation(_rotation);
    }
}
