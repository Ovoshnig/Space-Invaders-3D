using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvaderRegistry
{
    private readonly List<InvaderEntityView> _invaderEntityViews = new();
    private readonly Subject<InvaderEntityView> _added = new();
    private readonly Subject<InvaderEntityView> _removed = new();

    public InvaderRegistry()
    {
        Changed = Observable.Merge(Added, Removed);
        Observable<bool> updates = Changed.Select(_ => _invaderEntityViews.Any());

        Any = Observable.Return(_invaderEntityViews.Any())
            .Concat(updates)
            .DistinctUntilChanged()
            .ToReadOnlyReactiveProperty();
    }

    public IReadOnlyList<InvaderEntityView> InvaderEntityViews => _invaderEntityViews;
    public Observable<InvaderEntityView> Added => _added;
    public Observable<InvaderEntityView> Removed => _removed;
    public Observable<InvaderEntityView> Changed { get; }
    public ReadOnlyReactiveProperty<bool> Any { get; }

    public void Add(InvaderEntityView entityView)
    {
        _invaderEntityViews.Add(entityView);
        _added.OnNext(entityView);
    }

    public void Remove(InvaderEntityView entityView)
    {
        _invaderEntityViews.Remove(entityView);
        _removed.OnNext(entityView);
    }

    public IReadOnlyList<T> Get<T>() where T : MonoBehaviour => InvaderEntityViews.Select(e => e.Get<T>()).ToList();
}
