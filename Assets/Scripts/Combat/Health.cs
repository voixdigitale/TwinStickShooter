using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject DeathVFXPrefab => _deathVFXPrefab;
    public int GetTeamId => _teamId;
    public float InvulnerabilityDuration => _invulnerabilityDuration;
    public bool IsInvulnerable => _isInvulnerable;
    public int CurrentHealth => _currentHealth;
    public void EnableInvulnerability() => _isInvulnerable = true;
    public void DisableInvulnerability() => _isInvulnerable = false;

    public static Action<Health> OnDeath;

    [SerializeField] private GameObject _deathVFXPrefab;
    [SerializeField] private int _startingHealth = 4;
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isInvulnerable = false;
    [SerializeField] private float _invulnerabilityDuration;
    [SerializeField] private int _teamId;

    private void Start() {
        ResetHealth();
    }

    public void ResetHealth() {
        _currentHealth = _startingHealth;
    }

    public void ReduceHealth(int amount) {
        if (_isInvulnerable) return;

        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
