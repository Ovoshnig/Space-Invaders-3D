using R3;
using System.Collections.Generic;
using System.Linq;

public class InvaderRegistry
{
    private readonly List<InvaderEntityView> _invaders = new();
    private readonly Subject<InvaderEntityView> _added = new();
    private readonly Subject<InvaderEntityView> _removed = new();

    public InvaderRegistry()
    {
        Changed = Observable.Merge(Added, Removed);
        Observable<bool> updates = Changed.Select(_ => _invaders.Any());

        Any = Observable.Return(_invaders.Any())
            .Concat(updates)
            .DistinctUntilChanged()
            .ToReadOnlyReactiveProperty();
    }

    public IReadOnlyList<InvaderEntityView> Invaders => _invaders;
    public Observable<InvaderEntityView> Added => _added;
    public Observable<InvaderEntityView> Removed => _removed;
    public Observable<InvaderEntityView> Changed { get; }
    public ReadOnlyReactiveProperty<bool> Any { get; }

    public void Add(InvaderEntityView entityView)
    {
        if (entityView == null || _invaders.Contains(entityView))
            return;

        _invaders.Add(entityView);
        _added.OnNext(entityView);
    }

    public void Remove(InvaderEntityView entityView)
    {
        if (entityView == null || !_invaders.Contains(entityView))
            return;

        _invaders.Remove(entityView);
        _removed.OnNext(entityView);
    }
}
