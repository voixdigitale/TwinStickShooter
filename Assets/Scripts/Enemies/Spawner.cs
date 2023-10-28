using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _checkPointChannel = 0;
    [SerializeField] private int _checkPointsNeeded = 1;
    [SerializeField] private float _spawnDelay = 0f;
    [SerializeField] private GameObject _spawnVFX;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private bool _triggersSpawnOnDeath;
    [SerializeField] private int _spawnChannel;

    private int _checkPointsTriggered = 0;

    private void OnEnable() {
        GameEvents.OnCheckPointEnter += CheckSpawning;
    }

    private void OnDisable() {
        GameEvents.OnCheckPointEnter -= CheckSpawning;
    }

    private void CheckSpawning(int checkPointNum) {
        if (checkPointNum == _checkPointChannel) {
            _checkPointsTriggered++;

            if (_checkPointsTriggered < _checkPointsNeeded) return;
            
            StartCoroutine(PrepareSpawn(_spawnDelay));
        }
    }

    private IEnumerator PrepareSpawn(float spawnDelay) {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(_spawnVFX, transform.position, transform.rotation, transform);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(.4f);
        GameObject spawnedEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, _enemyContainer);
        if (_triggersSpawnOnDeath) {
            spawnedEnemy.GetComponent<Health>().SetSpawnOnDeath(_spawnChannel);
        }

        StartCoroutine(TagForDestruction());
    }

    private IEnumerator TagForDestruction() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
