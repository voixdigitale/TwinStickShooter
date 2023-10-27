public class FollowerEnemy : Enemy {

    protected override void OnStart() {
        base.OnStart();

        _targetLock.SetTarget(_player.transform);
    }

    public override void Movement() {
        if (_targetLock.CurrentTarget == null) return;

        float dirX = _targetLock.CurrentTarget.position.x - transform.position.x;
        float dirZ = _targetLock.CurrentTarget.position.z - transform.position.z;

        _movement.SetCurrentDirection(dirX, dirZ);
    }
}
