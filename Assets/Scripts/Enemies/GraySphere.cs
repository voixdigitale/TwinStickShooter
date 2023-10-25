using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraySphere : MonoBehaviour, IEnemy
{
    [SerializeField] private int _speed;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _startDelay = .5f;

    private Flash _flash;
    private Health _health;
    private Shooting _shooting;
    private Movement _movement;
    private int _currentWaypoint;

    private Transform _player;

    private void Awake() {
        _flash = GetComponent<Flash>();
        _health = GetComponent<Health>();
        _shooting = GetComponent<Shooting>();
        _movement = GetComponent<Movement>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start() {
        _currentWaypoint = 0;
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
    public void Movement() {
        float dirX = _player.position.x - transform.position.x;
        float dirZ = _player.position.z - transform.position.z;

        _movement.SetCurrentDirection(dirX, dirZ);
        transform.LookAt(_player.position);
    }

    public void Shooting() {
        _shooting.SetTrigger(true);
    }

    private IEnumerator StartDelay(float startDelay) {
        yield return new WaitForSeconds(startDelay);
        _shooting.AllowShoot();
    }
}
