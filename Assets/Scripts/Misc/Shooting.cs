using System;
using UnityEngine;
using UnityEngine.Pool;

public class Shooting : MonoBehaviour
{
    public static Action OnShoot;

    [SerializeField] private Transform _projectileNozzle;
    [SerializeField] private float _shootCoolDown = .5f;
    [SerializeField] private Projectile _projectilePrefab;

    private ObjectPool<Projectile> _projectilePool;
    private float _lastFireTime = 0f;
    private bool _isShooting;
    private bool _canShoot = true;

    private void OnEnable() {
        OnShoot += ShootProjectile;
        OnShoot += ResetLastFireTime;
    }

    private void OnDisable() {
        OnShoot -= ShootProjectile;
        OnShoot -= ResetLastFireTime;

    }

    private void Start() {
        CreateBulletPool();
    }

    private void Update() {
        Shoot();
    }

    private void AllowShoot() {
        _canShoot = true;
    }
    private void PreventShoot() {
        _canShoot = false;
    }

    public void SetTrigger(bool isShooting) {
        _isShooting = isShooting;
    }

    public void ReleaseProjectileFromPool(Projectile projectile) {
        _projectilePool.Release(projectile);
    }

    private void CreateBulletPool() {
        _projectilePool = new ObjectPool<Projectile>(() => {
            return Instantiate(_projectilePrefab);  // Fonction de création
        }, projectile => {
            projectile.gameObject.SetActive(true);  // OnGet
        }, projectile => {
            projectile.gameObject.SetActive(false); // OnRelease
        }, projectile => {
            Destroy(projectile);                    // OnDestroy
        }, false,                                   // Erreurs si on "Release" un objet dans la collection
        20,                                         // Taille du tableau pour éviter recréations
        40                                          // Taille maximale du tableau
        );
    }

    void Shoot() {
        if (!_canShoot) { return; }

        if (_isShooting && Time.time >= _lastFireTime) {
            OnShoot?.Invoke();
        }
    }

    private void ResetLastFireTime() {
        _lastFireTime = Time.time + _shootCoolDown;
    }

    private void ShootProjectile() {
        Projectile newProjectile = _projectilePool.Get();
        newProjectile.Init(this, _projectileNozzle.position, _projectileNozzle.rotation);
    }
}
