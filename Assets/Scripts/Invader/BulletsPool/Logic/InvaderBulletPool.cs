using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class InvaderBulletPool : IDisposable
{
    private readonly InvaderBulletMoverView[] _bulletPrefabs;
    private readonly InvaderShootingSettings _invaderShootingSettings;
    private readonly List<ObjectPool<InvaderBulletMoverView>> _pools = new();
    private readonly GameObject _parentObject = new("InvaderBullets");
    private readonly List<InvaderBulletMoverView> _activeBullets = new();
    private readonly Dictionary<InvaderBulletMoverView, IDisposable> _subscriptions = new();


    public InvaderBulletPool(InvaderBulletMoverView[] bulletPrefabs,
        InvaderShootingSettings invaderShootingSettings)
    {
        _bulletPrefabs = bulletPrefabs;
        _invaderShootingSettings = invaderShootingSettings;

        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            int index = i;
            InvaderBulletMoverView prefab = _bulletPrefabs[index];

            ObjectPool<InvaderBulletMoverView> pool = new(
            createFunc: () => Object.Instantiate(prefab, _parentObject.transform),
            actionOnGet: bullet => OnGetBullet(bullet, index),
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: _invaderShootingSettings.MaxActive,
            maxSize: _invaderShootingSettings.MaxActive);

            _pools.Add(pool);
        }
    }

    public void Dispose()
    {
        foreach (IDisposable subscription in _subscriptions.Values)
            subscription.Dispose();

        _subscriptions.Clear();
    }

    public bool TryGetRandomBullet(out InvaderBulletMoverView bullet)
    {
        bullet = null;

        if (_activeBullets.Count >= _invaderShootingSettings.MaxActive)
            return false;

        int randomIndex = Random.Range(0, _pools.Count);
        bullet = _pools[randomIndex].Get();
        return true;
    }

    public void ReleaseAllActive()
    {
        for (int i = _activeBullets.Count - 1; i >= 0; i--)
        {
            InvaderBulletMoverView bullet = _activeBullets[i];
            bullet.gameObject.SetActive(false);
        }
    }

    private void OnGetBullet(InvaderBulletMoverView bullet, int index)
    {
        bullet.gameObject.SetActive(true);
        _activeBullets.Add(bullet);

        IDisposable subscription = bullet.IsEnabled
            .Where(isEnabled => !isEnabled)
            .Subscribe(_ => _pools[index].Release(bullet));

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
