using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _damageAmount = 1;

    private Rigidbody _rigidBody;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        _rigidBody.velocity = _rigidBody.transform.forward * _moveSpeed;
    }

    public void Init(Vector3 bulletSpawnPos, Quaternion bulletRotation) {
        transform.position = bulletSpawnPos;
        transform.rotation = bulletRotation;
    }
}
