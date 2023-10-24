using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject DeathVFXPrefab => _deathVFXPrefab;

    public static Action<Health> OnDeath;

    [SerializeField] private GameObject _deathVFXPrefab;
    [SerializeField] private int _startingHealth = 4;
    [SerializeField] private int _teamId;

    private int _currentHealth;

    private void Start() {
        ResetHealth();
    }

    public void ResetHealth() {
        _currentHealth = _startingHealth;
    }

    public void ReduceHealth(int amount) {
        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
