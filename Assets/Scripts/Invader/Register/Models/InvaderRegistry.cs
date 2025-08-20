using System.Collections.Generic;

public class InvaderRegistry
{
    private readonly List<InvaderMoverView> _invaders = new();

    public IReadOnlyList<InvaderMoverView> Invaders => _invaders;

    public void Add(InvaderMoverView invader) => _invaders.Add(invader);

    public void Remove(InvaderMoverView invader) => _invaders.Remove(invader);
}
