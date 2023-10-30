using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour, IDamageable
{
    private Flash _flash;
    private Health _health;

    private void Awake() {
        _flash = GetComponent<Flash>();
        _health = GetComponent<Health>();
    }

    public void TakeHit(int teamId, GameObject hitSource) {
        if (teamId != _health.GetTeamId) {
            _flash.StartFlash();
        }
    }

    public void TakeDamage(int teamId, int damageAmount) {
        if (teamId != _health.GetTeamId) {
            _health.ReduceHealth(damageAmount);
        }
    }
}
