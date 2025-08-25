using R3;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class InvaderShooterMediator : Mediator
{
    private readonly InvaderShooter _invaderShooter;
    private readonly InvaderRegistry _registry;

    private CancellationTokenSource _cts = new();

    public InvaderShooterMediator(InvaderShooter invaderShooter,
        InvaderRegistry registry)
    {
        _invaderShooter = invaderShooter;
        _registry = registry;
    }

    public async override void Initialize()
    {
        _registry.InvaderShooters
            .Subscribe(OnInvadersChange)
            .AddTo(CompositeDisposable);

        _invaderShooter.Shot
            .Subscribe(OnShot)
            .AddTo(CompositeDisposable);

        try
        {
            await _invaderShooter.StartShootingAsync(_cts.Token);
        }
        catch (System.OperationCanceledException)
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

    private void OnInvadersChange(IReadOnlyList<InvaderShooterView> invaders)
    {
        if (invaders.Any())
        {
            _invaderShooter.SetInvadersCount(invaders.Count());
            return;
        }

        _cts.Cancel();
        _cts.Dispose();
        _cts = new CancellationTokenSource();
    }

    private void OnShot(ShotEvent shotEvent)
    {
        IReadOnlyList<InvaderShooterView> invaders = _registry.InvaderShooters.CurrentValue;
        InvaderShooterView invader = invaders[shotEvent.InvaderIndex];
        invader.Shoot(shotEvent.Bullet);
    }
}
