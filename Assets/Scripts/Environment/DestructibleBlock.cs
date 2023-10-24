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

    public void TakeHit() {
        _flash.StartFlash();
    }

    public void TakeDamage(int damageAmount) {
        _health.ReduceHealth(damageAmount);
    }

    public int GetTeamId() {
        return -1;
    }
}
