using R3;
using System;
using System.Collections.Generic;
using UnityEngine.Pool;
using Object = UnityEngine.Object;
using Random = System.Random;

public class InvaderBulletPool : IDisposable
{
    private readonly InvaderBulletMoverView[] _bulletPrefabs;
    private readonly InvaderShootingSettings _invaderShootingSettings;
    private readonly FieldView _fieldView;
    private readonly ObjectPool<InvaderBulletMoverView> _pool;
    private readonly List<InvaderBulletMoverView> _activeBullets = new();
    private readonly Random _random = new();
    private readonly Dictionary<InvaderBulletMoverView, IDisposable> _subscriptions = new();

    public InvaderBulletPool(
        InvaderBulletMoverView[] bulletPrefabs,
        InvaderShootingSettings invaderShootingSettings,
        FieldView fieldView)
    {
        _bulletPrefabs = bulletPrefabs;
        _invaderShootingSettings = invaderShootingSettings;
        _fieldView = fieldView;

        _pool = new ObjectPool<InvaderBulletMoverView>(
            createFunc: CreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: _invaderShootingSettings.MaxActive,
            maxSize: _invaderShootingSettings.MaxActive);
    }

    public void Dispose()
    {
        foreach (IDisposable subscription in _subscriptions.Values)
            subscription.Dispose();

        _subscriptions.Clear();
    }

    public bool TryGetBullet(out InvaderBulletMoverView bullet)
    {
        bullet = null;

        if (_pool.CountActive >= _invaderShootingSettings.MaxActive)
            return false;

        bullet = _pool.Get();
        return true;
    }

    public void ReleaseAllActive()
    {
        for (int i = _activeBullets.Count - 1; i >= 0; i--)
        {
            InvaderBulletMoverView bullet = _activeBullets[i];
            _pool.Release(bullet);
        }
    }

    private InvaderBulletMoverView CreateBullet()
    {
        int randomIndex = _random.Next(0, _bulletPrefabs.Length);
        InvaderBulletMoverView prefab = _bulletPrefabs[randomIndex];
        InvaderBulletMoverView bullet = Object.Instantiate(prefab);
        bullet.Construct(_fieldView, _invaderShootingSettings);

        return bullet;
    }

    private void OnGetBullet(InvaderBulletMoverView bullet)
    {
        bullet.gameObject.SetActive(true);
        _activeBullets.Add(bullet);

        IDisposable subscription = bullet.IsEnabled
            .Where(isEnabled => !isEnabled)
            .Subscribe(_ => _pool.Release(bullet));

        _subscriptions[bullet] = subscription;
    }

    private void OnReleaseBullet(InvaderBulletMoverView bullet)
    {
        CleanupBullet(bullet);
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(InvaderBulletMoverView bullet) => CleanupBullet(bullet);

    private void CleanupBullet(InvaderBulletMoverView bullet)
    {
        if (_subscriptions.TryGetValue(bullet, out IDisposable subscription))
        {
            subscription.Dispose();
            _subscriptions.Remove(bullet);
        }

        _activeBullets.Remove(bullet);
    }
}
