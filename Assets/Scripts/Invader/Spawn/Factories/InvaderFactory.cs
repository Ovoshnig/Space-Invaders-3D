using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class InvaderFactory : IDisposable
{
    private readonly IObjectResolver _container;
    private readonly InvaderRegistry _registry;
    private readonly InvaderEntityView[] _prefabs;
    private readonly Transform _invaderRoot;
    private readonly Dictionary<int, IObjectPool<InvaderEntityView>> _pools = new();

    public InvaderFactory(IObjectResolver resolver,
        InvaderRegistry registry,
        InvaderEntityView[] prefabs)
    {
        _container = resolver;
        _registry = registry;
        _prefabs = prefabs;
        _invaderRoot = new GameObject("Invaders").transform;

        InitializePools();
    }

    private void InitializePools()
    {
        for (int i = 0; i < _prefabs.Length; i++)
        {
            InvaderEntityView prefab = _prefabs[i];
            int prefabIndex = i;

            ObjectPool<InvaderEntityView> pool = new(
                createFunc: () => _container.Instantiate(prefab, _invaderRoot),
                actionOnGet: (instance) =>
                {
                    instance.gameObject.SetActive(true);
                    _registry.Add(instance);
                },
                actionOnRelease: (instance) =>
                {
                    instance.gameObject.SetActive(false);
                    _registry.Remove(instance);
                },
                actionOnDestroy: (instance) => { },
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: 100
            );
            _pools.Add(prefabIndex, pool);
        }
    }

    public void Dispose()
    {
        foreach (var pool in _pools.Values)
            pool.Clear();

        _pools.Clear();
    }

    public InvaderEntityView Create(int prefabIndex, Vector3 position)
    {
        if (!_pools.TryGetValue(prefabIndex, out var pool))
        {
            Debug.LogError($"No pool found for prefab index {prefabIndex}");
            return null;
        }

        InvaderEntityView instance = pool.Get();
        instance.transform.SetPositionAndRotation(position, Quaternion.identity);

        instance.DestroyerView.Destroyed
            .Take(1)
            .Subscribe(_ => pool.Release(instance));

        return instance;
    }
}
