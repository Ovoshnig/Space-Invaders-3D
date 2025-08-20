using R3;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class InvaderFactory : IDisposable
{
    private readonly IObjectResolver _container;
    private readonly InvaderRegistry _registry;
    private readonly GameObject[] _prefabs;
    private readonly Transform _invaderRoot;
    private readonly CompositeDisposable _compositeDisposable = new();

    public InvaderFactory(IObjectResolver resolver,
        InvaderRegistry registry,
        GameObject[] prefabs)
    {
        _container = resolver;
        _registry = registry;
        _prefabs = prefabs;
        _invaderRoot = new GameObject("Invaders").transform;
    }

    public InvaderMoverView Create<T>(int prefabIndex, Vector3 position)
    {
        GameObject prefab = _prefabs[prefabIndex];
        GameObject instance = _container.Instantiate(prefab, position, Quaternion.identity, _invaderRoot);
        InvaderMoverView moverView = instance.GetComponent<InvaderMoverView>();

        _registry.Add(moverView);

        moverView.Destroyed
            .Subscribe(_ => _registry.Remove(moverView))
            .AddTo(_compositeDisposable);

        return moverView;
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
