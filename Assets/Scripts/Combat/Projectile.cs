using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _damageAmount = 1;

    private Rigidbody _rigidBody;
    private Shooting _shooting;
    private int _teamId;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        _rigidBody.velocity = _rigidBody.transform.forward * _moveSpeed;
    }

    public void Init(Shooting shooting, int teamId, Vector3 projectileSpawnPos, Quaternion projectileRotation) {
        transform.position = projectileSpawnPos;
        transform.rotation = projectileRotation;
        _shooting = shooting;
        _teamId = teamId;
    }

    private void OnTriggerEnter(Collider other) {
        IDamageable iDamageable = other.gameObject.GetComponent<IDamageable>();
        iDamageable?.TakeDamage(_teamId, _damageAmount);

        IHitable iHitable = other.gameObject.GetComponent<IHitable>();
        iHitable?.TakeHit(_teamId);

        _shooting.ReleaseProjectileFromPool(this);
    }
}
