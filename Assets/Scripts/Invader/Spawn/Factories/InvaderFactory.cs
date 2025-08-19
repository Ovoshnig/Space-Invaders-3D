using UnityEngine;
using VContainer;
using VContainer.Unity;

public class InvaderFactory
{
    private readonly IObjectResolver _container;
    private readonly GameObject[] _prefabs;
    private readonly Transform _invaderRoot;

    public InvaderFactory(IObjectResolver resolver, GameObject[] prefabs)
    {
        _container = resolver;
        _prefabs = prefabs;
        _invaderRoot = new GameObject("Invaders").transform;
    }

    public T Create<T>(int prefabIndex) where T : Component
    {
        GameObject prefab = _prefabs[prefabIndex];
        return _container.Instantiate(prefab, _invaderRoot).GetComponent<T>();
    }
}
