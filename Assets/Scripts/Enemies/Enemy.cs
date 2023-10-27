using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] protected float _startDelay = .5f;

    protected Flash _flash;
    protected Health _health;
    protected Shooting _shooting;
    protected Movement _movement;
    protected TargetLock _targetLock;

    protected Transform _player;

    private void Awake() {
        _flash = GetComponent<Flash>();
        _health = GetComponent<Health>();
        _shooting = GetComponent<Shooting>();
        _movement = GetComponent<Movement>();
        _targetLock = GetComponent<TargetLock>();

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
        _shooting.PreventShoot();
        StartCoroutine(StartDelay(_startDelay));
    }

    private void Update() {
        Movement();
        Shooting();
    }

    public void TakeHit(int teamId) {
        if (teamId != _health.GetTeamId) {
            Health.OnHit?.Invoke(_health, gameObject.tag);
            _flash.StartFlash();
        }
    }

    public void TakeDamage(int teamId, int damageAmount) {
        if (teamId != _health.GetTeamId) _health.ReduceHealth(damageAmount);
    }

    // Start is called before the first frame update
    public abstract void Movement();

    public virtual void Shooting() {
        _shooting.SetTrigger(true);
    }

    private IEnumerator StartDelay(float startDelay) {
        yield return new WaitForSeconds(startDelay);
        _shooting.AllowShoot();
    }

    private void Health_OnDeath(Health sender, string tag) {
        if (tag == "Player") {
            _targetLock.ClearTarget();
        }
    }
}
