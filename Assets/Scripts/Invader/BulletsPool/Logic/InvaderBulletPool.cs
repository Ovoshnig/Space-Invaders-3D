using R3;
using System;
using UnityEngine.Pool;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = System.Random;

public class InvaderBulletPool : IInitializable, IDisposable
{
    private readonly InvaderBulletMoverView[] _bulletPrefabs;
    private readonly InvaderShootingSettings _invaderShootingSettings;
    private readonly BulletSettings _bulletSettings;
    private readonly FieldView _fieldView;
    private readonly Random _random = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    private ObjectPool<InvaderBulletMoverView> _pool;

    public InvaderBulletPool(InvaderBulletMoverView[] bulletPrefabs,
        InvaderShootingSettings invaderShootingSettings,
        BulletSettings bulletSettings,
        FieldView fieldView)
    {
        _bulletPrefabs = bulletPrefabs;
        _invaderShootingSettings = invaderShootingSettings;
        _bulletSettings = bulletSettings;
        _fieldView = fieldView;
    }

    public void Initialize()
    {
        _pool = new ObjectPool<InvaderBulletMoverView>(
            createFunc: CreateBullet,
            actionOnGet: bullet => bullet.gameObject.SetActive(true),
            actionOnRelease: bullet => bullet.gameObject.SetActive(false),
            actionOnDestroy: bullet => { },
            collectionCheck: true,
            defaultCapacity: _invaderShootingSettings.MaxActive,
            maxSize: _invaderShootingSettings.MaxActive);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    public bool TryGetBullet(out InvaderBulletMoverView bullet)
    {
        bullet = null;

        if (_pool.CountActive >= _invaderShootingSettings.MaxActive)
            return false;

        bullet = _pool.Get();
        return true;
    }

    private InvaderBulletMoverView CreateBullet()
    {
        int randomIndex = _random.Next(0, _bulletPrefabs.Length);
        InvaderBulletMoverView prefab = _bulletPrefabs[randomIndex];
        InvaderBulletMoverView bullet = Object.Instantiate(prefab);
        bullet.Construct(_bulletSettings, _fieldView);

        bullet.IsEnabled
            .Where(isEnabled => !isEnabled)
            .Subscribe(_ => _pool.Release(bullet))
            .AddTo(_compositeDisposable);

        return bullet;
    }
}
