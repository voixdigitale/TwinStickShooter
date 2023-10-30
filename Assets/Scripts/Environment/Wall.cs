using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IHitable {
    private Health _health;

    private void Awake() {
        _health = GetComponent<Health>();
    }
    public void TakeHit(int teamId, GameObject hitSource) {
        Health.OnHit?.Invoke(_health, gameObject.tag, hitSource);
    }
}
