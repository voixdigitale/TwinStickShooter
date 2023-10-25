using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    public Transform CurrentTarget => _currentTarget;
    public bool IsEnabled() => _enabled;

    [SerializeField] private bool _enabled;
    [SerializeField] private float _lockCooldown = .5f;
    [SerializeField] private Transform _currentTarget;

    private Collider[] _closeEnemies;
    
    private bool _isCoolingDown = false;
    private int _radarRadius = 3;
    private int _enemyLayerMask = 6;

    private void OnEnable() {
        Health.OnDeath += CheckTargetDeath;
    }

    private void OnDisable() {
        Health.OnDeath -= CheckTargetDeath;
    }

    public bool NextTarget() {
        CheckForEnemies();
        if (_closeEnemies.Length < 1 ) return false;

        _currentTarget = _closeEnemies[0].transform;
        return true;
    }

    public void CheckForEnemies() {
        _closeEnemies = Physics.OverlapSphere(transform.position, _radarRadius, 1 << _enemyLayerMask);
    }

    public void Enable() {
        if (!_isCoolingDown) {
            NextTarget();
            _enabled = true;
            StartCooldown();
        }
    }
    public void Disable() {
        if (!_isCoolingDown) {
            _enabled = false;
            StartCooldown();
        }
    }
    public void Trigger() {
        if (!_isCoolingDown) {
            Transform prevTarget = _currentTarget;
            NextTarget();
            if (!_enabled && _currentTarget != null || _enabled && prevTarget != _currentTarget) {
                _enabled = true;
            } else {
                _enabled = false;
            }
            StartCooldown();
        }
    }

    private void StartCooldown() {
        _isCoolingDown = true;
        StartCoroutine(UseCoolDown(_lockCooldown));
    }

    private IEnumerator UseCoolDown(float lockCooldown) {
        yield return new WaitForSeconds(lockCooldown);
        _isCoolingDown = false;
    }

    private void CheckTargetDeath(Health sender, string tag) {
        if (sender.transform == _currentTarget) {
            _currentTarget = null;
            if (!NextTarget()) {
                Disable();
            }
        }
    }
}
