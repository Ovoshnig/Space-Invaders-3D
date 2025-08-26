using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvaderRegistry
{
    private readonly List<InvaderEntityView> _list = new();
    private readonly ReactiveProperty<IReadOnlyList<InvaderEntityView>> _entityViews = new();

    public ReadOnlyReactiveProperty<IReadOnlyList<InvaderEntityView>> EntityViews => _entityViews;

    public void Add(InvaderEntityView entityView)
    {
        _list.Add(entityView);
        _entityViews.OnNext(_list);
    }

    public void Remove(InvaderEntityView entityView)
    {
        _list.Remove(entityView);
        _entityViews.OnNext(_list);
    }

    public IReadOnlyList<T> Get<T>() where T : MonoBehaviour => _list.Select(e => e.Get<T>()).ToList();
}
