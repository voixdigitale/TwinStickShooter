using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _hitVFX;
    [SerializeField] private GamepadManager _gamepadManager;
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private GameObject _playerRightSide;
    [SerializeField] private GameObject _playerLeftSide;

    private Health _health;
    private float _colorChangeDelay = .5f;

    private void Awake() {
        _health = GetComponent<Health>();
    }

    public void TakeDamage(int teamId, int damageAmount) {
        if (_health.IsInvulnerable) return;

        if (teamId != _health.GetTeamId) {
            _health.ReduceHealth(damageAmount);
            LoseParts();
        }
    }

    public void TakeHit(int teamId) {
        if (_health.IsInvulnerable) return;

        if (teamId != _health.GetTeamId) {
            TurnBlack();
            _hitVFX.SetActive(true);
            _health.EnableInvulnerability();
            StartCoroutine(HitCoroutine(_health.InvulnerabilityDuration));
            CinemachineImpulseSource _shakeImpulse = _hitVFX.GetComponent<CinemachineImpulseSource>();
            if (_shakeImpulse != null) {
                _shakeImpulse.GenerateImpulse();
                _gamepadManager.Vibrate(.5f);
            }
        }
    }

    private IEnumerator HitCoroutine(float delay) {
        yield return new WaitForSeconds(delay);
        _hitVFX.SetActive(false);
        _health.DisableInvulnerability();
        StartCoroutine(GraduallyReturnToWhite());
    }

    private void TurnBlack() {
        Renderer[] renderers = _playerModel.GetComponentsInChildren<Renderer>();
        
        foreach(Renderer r in renderers) {
            r.material.color = Color.black;
        }
    }

    private IEnumerator GraduallyReturnToWhite() {
        float t = 0;

        while (t < _colorChangeDelay) {
            t += Time.deltaTime;
            Renderer[] renderers = _playerModel.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers) {
                if(r.gameObject.name == "Center Cylinder") continue;
                r.material.color = Color.Lerp(Color.black, Color.white, t / _colorChangeDelay);
            }

            yield return null;
        }
    }

    private void LoseParts() {
        if (_health.CurrentHealth == 2) {
            _playerRightSide.SetActive(false);
        } else if (_health.CurrentHealth == 1) {
            _playerLeftSide.SetActive(false);
        }
    }
}
