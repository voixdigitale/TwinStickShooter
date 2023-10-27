using Unity.VisualScripting;
using UnityEngine;

public class GraySphere : Enemy, IEnemy {

    [SerializeField] private bool _invertX;
    [SerializeField] private bool _invertY;
    [SerializeField] private Vector2 _distance;
    [SerializeField] private Vector2 _movementFrequency;
    [SerializeField] private bool _lookAtPlayer;

    private Vector3 _movePosition;

    protected override void OnStart() {
        base.OnStart();

        if (_lookAtPlayer) _targetLock.SetTarget(_player.transform);
    }

    public override void Movement() {
        if (_targetLock.CurrentTarget == null) return;

        float dirX = _invertX ? Mathf.Cos(Time.time * _movementFrequency.x) * _distance.x : Mathf.Sin(Time.time * _movementFrequency.x) * _distance.x;
        float dirZ = _invertY ? Mathf.Cos(Time.time * _movementFrequency.y) * _distance.y : Mathf.Sin(Time.time * _movementFrequency.y) * _distance.y;

        _movement.SetCurrentDirection(dirX, dirZ);
    }
}
