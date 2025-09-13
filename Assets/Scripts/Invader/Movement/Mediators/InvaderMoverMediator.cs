using Cysharp.Threading.Tasks;
using R3;
using System.Linq;
using System.Threading;
using UnityEngine;

public class InvaderMoverMediator : Mediator
{
    private readonly InvaderMover _invaderMover;
    private readonly InvaderRegistry _registry;
    private readonly InvaderSpawner _spawner;

    private CancellationTokenSource _cts;

    public InvaderMoverMediator(InvaderMover invaderMover,
        InvaderRegistry registry,
        InvaderSpawner spawner)
    {
        _invaderMover = invaderMover;
        _registry = registry;
        _spawner = spawner;
    }

    public override void Initialize()
    {
        _spawner.Ended
            .Subscribe(_ => OnWaveSpawnEnded())
            .AddTo(CompositeDisposable);

        _registry.Any
            .Where(any => !any)
            .Subscribe(_ => OnRegistryEmpty())
            .AddTo(CompositeDisposable);
        _registry.Changed
            .Subscribe(_ => OnRegistryChanged())
            .AddTo(CompositeDisposable);

        _invaderMover.Moved
            .Subscribe(OnMoved)
            .AddTo(CompositeDisposable);
    }

    public override void Dispose()
    {
        StopMovingTask();
        base.Dispose();
    }

    private void OnWaveSpawnEnded()
    {
        StopMovingTask();
        _cts = new CancellationTokenSource();
        _invaderMover.StartMovingAsync(_cts.Token).Forget();
    }

    private void OnRegistryEmpty() => StopMovingTask();

    private void OnRegistryChanged() => UpdateInvaderPositions();

    private void OnMoved(Vector3 movement)
    {
        foreach (var moverView in _registry.Get<InvaderMoverView>())
            moverView.Move(movement);
    }

    private void UpdateInvaderPositions()
    {
        if (!_registry.Any.CurrentValue)
            return;

        Vector3[] positions = _registry.Invaders
            .Select(i => i.transform.position)
            .ToArray();
        _invaderMover.SetPositions(positions);
    }

    private void StopMovingTask()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }
}
