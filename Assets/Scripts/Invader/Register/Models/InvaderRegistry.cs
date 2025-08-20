using R3;
using System.Collections.Generic;

public class InvaderRegistry
{
    private readonly List<InvaderMoverView> _list = new();
    private readonly ReactiveProperty<IReadOnlyList<InvaderMoverView>> _invaders;

    public InvaderRegistry() => 
        _invaders = new ReactiveProperty<IReadOnlyList<InvaderMoverView>>(_list.AsReadOnly());

    public ReadOnlyReactiveProperty<IReadOnlyList<InvaderMoverView>> Invaders => _invaders;

    public void Add(InvaderMoverView invader)
    {
        _list.Add(invader);
        _invaders.Value = _list.AsReadOnly();
    }

    public void Remove(InvaderMoverView invader)
    {
        _list.Remove(invader);
        _invaders.Value = _list.AsReadOnly();
    }
}
