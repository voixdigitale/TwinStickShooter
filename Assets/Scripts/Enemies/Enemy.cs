using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] protected float _startDelay = .5f;

    protected Flash _flash;
    protected Health _health;
    protected Shooting[] _shooting;
    protected Movement _movement;
    protected TargetLock _targetLock;

    protected Transform _player;
    protected Rigidbody _rigidBody;

    private void Awake() {
        _flash = GetComponent<Flash>();
        _health = GetComponent<Health>();
        _shooting = GetComponents<Shooting>();
        _movement = GetComponent<Movement>();
        _targetLock = GetComponent<TargetLock>();
        _rigidBody = GetComponent<Rigidbody>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable() {
        Health.OnDeath += Health_OnDeath;
    }

    private void OnDisable() {
        Health.OnDeath -= Health_OnDeath;
    }

    private void Start() {
        OnStart();
    }

    protected virtual void OnStart() {
        foreach(Shooting shooting in _shooting) {
            shooting.PreventShoot();
        }
        
        StartCoroutine(StartDelay(_startDelay));
    }

    private void Update() {
        Movement();
        Shooting();
    }

    public void TakeHit(int teamId, GameObject hitSource) {
        if (_health.IsInvulnerable) return;

        if (teamId != _health.GetTeamId && _flash != null) {
            Health.OnHit?.Invoke(_health, gameObject.tag, hitSource);
            _flash.StartFlash();
            _health.EnableInvulnerability();
            StartCoroutine(HitCoroutine(_health.InvulnerabilityDuration));
        }
    }

    private IEnumerator HitCoroutine(float delay) {
        yield return new WaitForSeconds(delay);
        _health.DisableInvulnerability();
    }

    public void TakeDamage(int teamId, int damageAmount) {
        if (teamId != _health.GetTeamId) _health.ReduceHealth(damageAmount);
    }

    // Start is called before the first frame update
    public abstract void Movement();

    public virtual void Shooting() {
        foreach (Shooting shooting in _shooting)
            shooting.SetTrigger(true);
    }

    private IEnumerator StartDelay(float startDelay) {
        yield return new WaitForSeconds(startDelay);
        foreach (Shooting shooting in _shooting)
            shooting.AllowShoot();
    }

    private void Health_OnDeath(Health sender, string tag) {
        if (tag == "Player") {
            if (_targetLock != null) _targetLock.ClearTarget();
        }
    }
}
