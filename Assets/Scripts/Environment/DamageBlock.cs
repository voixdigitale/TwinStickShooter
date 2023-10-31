using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private int _teamId = 0;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerHitHandler _player = collision.gameObject.GetComponent<PlayerHitHandler>();
            _player.TakeHit(_teamId, gameObject);
        }
    }
}
