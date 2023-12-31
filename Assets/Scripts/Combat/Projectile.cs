using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IHitable
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _damageAmount = 1;

    private Rigidbody _rigidBody;
    private Shooting _shooting;
    private Health _health;
    private int _teamId;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
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
        iHitable?.TakeHit(_teamId, gameObject);

        _shooting.ReleaseProjectileFromPool(this);
    }

    public void TakeHit(int teamId, GameObject hitSource) {
        Health.OnHit?.Invoke(_health, gameObject.tag, hitSource);
    }
}
