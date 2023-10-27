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
    public bool TriggersSpawn => _triggersSpawn;
    public int SpawnNumber => _spawnNumber;
    public void EnableInvulnerability() => _isInvulnerable = true;
    public void DisableInvulnerability() => _isInvulnerable = false;

    public static Action<Health, string> OnDeath;
    public static Action<Health, string> OnHit;

    [SerializeField] private GameObject _deathVFXPrefab;
    [SerializeField] private int _startingHealth = 4;
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isInvulnerable = false;
    [SerializeField] private float _invulnerabilityDuration;
    [SerializeField] private int _teamId;
    
    
    private bool _triggersSpawn = false;
    private int _spawnNumber;

    private void Start() {
        ResetHealth();
    }

    public void ResetHealth() {
        _currentHealth = _startingHealth;
    }

    public void SetSpawnOnDeath(int spawnNumber) {
        _triggersSpawn = true;
        _spawnNumber = spawnNumber;
    }

    public void ReduceHealth(int amount) {
        if (_isInvulnerable) return;

        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            OnDeath?.Invoke(this, gameObject.tag);
            if (_triggersSpawn) GameEvents.current.CheckPointEnter(_spawnNumber);
            Destroy(gameObject);
        }
    }
}
