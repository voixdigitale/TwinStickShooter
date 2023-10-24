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
    private int _currentWaypoint;

    private void Awake() {
        _flash = GetComponent<Flash>();
        _health = GetComponent<Health>();
        _shooting = GetComponent<Shooting>();
    }

    private void Start() {
        _currentWaypoint = 0;
        _shooting.PreventShoot();
        StartCoroutine(StartDelay(_startDelay));
    }

    private void Update() {
        Moving();
        Shooting();
    }

    public void TakeHit(int teamId) {
        if (teamId != _health.GetTeamId) _flash.StartFlash();
    }

    public void TakeDamage(int teamId, int damageAmount) {
        if (teamId != _health.GetTeamId) _health.ReduceHealth(damageAmount);
    }

    // Start is called before the first frame update
    public void Moving() {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    public void Shooting() {
        _shooting.SetTrigger(true);
    }

    private IEnumerator StartDelay(float startDelay) {
        yield return new WaitForSeconds(startDelay);
        _shooting.AllowShoot();
    }
}
