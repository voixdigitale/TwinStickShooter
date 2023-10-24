using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathVFXHandler : MonoBehaviour
{
    [SerializeField] GamepadManager _gamepadManager;

     private void OnEnable() {
        Health.OnDeath += SpawnDeathVFX;
    }

    private void OnDisable() {
        Health.OnDeath -= SpawnDeathVFX;
    }

    private void SpawnDeathVFX(Health sender) {
        Instantiate(sender.DeathVFXPrefab, sender.transform.position, sender.transform.rotation);
        CinemachineImpulseSource _shakeImpulse = sender.DeathVFXPrefab.GetComponent<CinemachineImpulseSource>();
        if (_shakeImpulse != null ) {
            _shakeImpulse.GenerateImpulse();
            _gamepadManager.Vibrate();
        }

    }
}
