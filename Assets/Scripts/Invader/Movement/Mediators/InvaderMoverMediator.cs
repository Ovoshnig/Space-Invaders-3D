using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Linq;
using System.Threading;
using UnityEngine;

public class InvaderMoverMediator : Mediator
{
    private readonly InvaderMover _invaderMover;
    private readonly InvaderRegistry _registry;

    private CancellationTokenSource _cts = new();

    public InvaderMoverMediator(InvaderMover invaderMover, 
        InvaderRegistry registry)
    {
        _invaderMover = invaderMover;
        _registry = registry;
    }

    public async override void Initialize()
    {
        _registry.Changed
            .Subscribe(OnInvadersChange)
            .AddTo(CompositeDisposable);

        _invaderMover.Moved
            .Subscribe(OnMoved)
            .AddTo(CompositeDisposable);

        try
        {
            await _invaderMover.StartMovingAsync(_cts.Token);
        }
        catch (OperationCanceledException)
        {
            return;
        }
    }

    public override void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();

        base.Dispose();
    }

    private void OnInvadersChange(InvaderEntityView _)
    {
        if (_registry.Any.CurrentValue)
        {
            _invaderMover.SetPositions(_registry.InvaderEntityViews.Select(i => i.transform.position).ToArray());
            return;
        }

        _cts.Cancel();
        _cts.Dispose();
        _cts = new CancellationTokenSource();
    }

    private void OnMoved(Vector3 movement)
    {
        foreach (var invader in _registry.Get<InvaderMoverView>())
            invader.Move(movement);
    }
}
