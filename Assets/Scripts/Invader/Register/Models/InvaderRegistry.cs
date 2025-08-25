using R3;
using System.Collections.Generic;

public class InvaderRegistry
{
    private readonly List<InvaderMoverView> _moversList = new();
    private readonly List<InvaderShooterView> _shootersList = new();
    private readonly ReactiveProperty<IReadOnlyList<InvaderMoverView>> _invaderMovers = new();
    private readonly ReactiveProperty<IReadOnlyList<InvaderShooterView>> _invaderShooters = new();

    public ReadOnlyReactiveProperty<IReadOnlyList<InvaderMoverView>> InvaderMovers => _invaderMovers;
    public ReadOnlyReactiveProperty<IReadOnlyList<InvaderShooterView>> InvaderShooters => _invaderShooters;

    public void Add(InvaderMoverView invaderMover)
    {
        _moversList.Add(invaderMover);
        _invaderMovers.Value = _moversList.AsReadOnly();

        _shootersList.Add(invaderMover.GetComponent<InvaderShooterView>());
        _invaderShooters.Value = _shootersList.AsReadOnly();
    }

    public void Remove(InvaderMoverView invaderMover)
    {
        int index = _moversList.IndexOf(invaderMover);

        _moversList.RemoveAt(index);
        _invaderMovers.Value = _moversList.AsReadOnly();

        _shootersList.RemoveAt(index);
        _invaderShooters.Value= _shootersList.AsReadOnly();
    }
}
