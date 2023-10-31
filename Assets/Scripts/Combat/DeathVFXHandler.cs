using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathVFXHandler : MonoBehaviour {
    [SerializeField] GamepadManager _gamepadManager;

    private void OnEnable() {
        Health.OnDeath += SpawnDeathVFX;
        Health.OnHit += SpawnHitVFX;
    }

    private void OnDisable() {
        Health.OnDeath -= SpawnDeathVFX;
        Health.OnHit -= SpawnHitVFX;
    }

    private void SpawnDeathVFX(Health sender, string tag) {
        GameObject vfxInstance = Instantiate(sender.DeathVFXPrefab, sender.transform.position, sender.transform.rotation);
        StartCoroutine(TagForDestruction(vfxInstance));
        CinemachineImpulseSource _shakeImpulse = sender.DeathVFXPrefab.GetComponent<CinemachineImpulseSource>();
        if (_shakeImpulse != null) {
            _shakeImpulse.GenerateImpulse();
            _gamepadManager.Vibrate();
        }
    }

    private void SpawnHitVFX(Health sender, string tag, GameObject hitSource) {
        if (sender.HitVFXPrefab != null) {
            
            Vector3 hitPosition;

            if (tag == "Enemy") {
                hitPosition = sender.transform.position;
            } else {
                hitPosition = hitSource.gameObject.GetComponent<Collider>().ClosestPointOnBounds(sender.transform.position);
            }
            
            GameObject vfxInstance = Instantiate(sender.HitVFXPrefab, hitPosition, hitSource.transform.rotation, tag == "Enemy" ? sender.transform : null);
            StartCoroutine(TagForDestruction(vfxInstance));
        }
    }

    private IEnumerator TagForDestruction(GameObject tag) {
        yield return new WaitForSeconds(1);
        Destroy(tag);
    }
}
