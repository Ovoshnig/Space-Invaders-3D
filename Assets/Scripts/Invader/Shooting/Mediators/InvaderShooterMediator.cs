using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class InvaderShooterMediator : Mediator
{
    private readonly InvaderShooter _invaderShooter;
    private readonly InvaderRegistry _registry;
    private readonly InvaderSpawner _spawner;

    private CancellationTokenSource _cts = new();

    public InvaderShooterMediator(InvaderShooter invaderShooter,
        InvaderRegistry registry,
        InvaderSpawner spawner)
    {
        _invaderShooter = invaderShooter;
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
            .Subscribe(OnRegistryChanged)
            .AddTo(CompositeDisposable);

        _invaderShooter.Shot
            .Subscribe(OnShot)
            .AddTo(CompositeDisposable);
    }

    public override void Dispose()
    {
        StopShootingTask();
        base.Dispose();
    }

    private void OnWaveSpawnEnded()
    {
        StopShootingTask();
        _cts = new CancellationTokenSource();
        _invaderShooter.StartShootingAsync(_cts.Token).Forget();
    }

    private void OnRegistryEmpty() => StopShootingTask();

    private void OnRegistryChanged(InvaderEntityView _)
    {
        if (!_registry.Any.CurrentValue)
            return;

        int[] frontInvaderIndices = _registry.Invaders
            .Select((invader, index) => (Invader: invader, Index: index))
            .GroupBy(item => item.Invader.transform.position.x)
            .Select(group => group.OrderBy(item => item.Invader.transform.position.z).First())
            .Select(item => item.Index)
            .ToArray();

        _invaderShooter.SetInvaderIndices(frontInvaderIndices, _registry.Invaders.Count);
    }

    private void OnShot(ShotEvent shotEvent)
    {
        InvaderEntityView entityView = _registry.Invaders[shotEvent.InvaderIndex];
        InvaderShooterView shooterView = entityView.ShooterView;
        shooterView.Shoot(shotEvent.Bullet);
    }

    private void StopShootingTask()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }
}
