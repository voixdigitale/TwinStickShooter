using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVFXHandler : MonoBehaviour
{
    private void OnEnable() {
        Health.OnDeath += SpawnDeathVFX;
    }

    private void OnDisable() {
        Health.OnDeath -= SpawnDeathVFX;
    }

    private void SpawnDeathVFX(Health sender) {
        Instantiate(sender.DeathVFXPrefab, sender.transform.position, sender.transform.rotation);
    }
}
