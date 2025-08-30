using R3;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class InvaderFactory : IInitializable, IDisposable
{
    private readonly IObjectResolver _container;
    private readonly InvaderRegistry _registry;
    private readonly InvaderEntityView[] _prefabs;
    private readonly CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> _destroyer;
    private readonly Transform _invaderRoot;
    private readonly CompositeDisposable _compositeDisposable = new();

    public InvaderFactory(IObjectResolver resolver,
        InvaderRegistry registry,
        InvaderEntityView[] prefabs,
        CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> destroyer)
    {
        _container = resolver;
        _registry = registry;
        _prefabs = prefabs;
        _destroyer = destroyer;
        _invaderRoot = new GameObject("Invaders").transform;
    }

    public void Initialize()
    {
        _destroyer.Destroyed
            .Subscribe(collidedDestructionEvent => _registry.Remove(collidedDestructionEvent.CollidedView))
            .AddTo(_compositeDisposable);
    }

    public InvaderEntityView Create(int prefabIndex, Vector3 position)
    {
        InvaderEntityView prefab = _prefabs[prefabIndex];
        InvaderEntityView instance = _container
            .Instantiate(prefab, position, Quaternion.identity, _invaderRoot);

        _registry.Add(instance);

        return instance;
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
