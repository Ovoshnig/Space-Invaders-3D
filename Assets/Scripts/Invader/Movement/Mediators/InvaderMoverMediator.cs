using Cysharp.Threading.Tasks;
using R3;
using System.Linq;
using System.Threading;

public class InvaderMoverMediator : Mediator
{
    private readonly InvaderMover _invaderMover;
    private readonly InvaderRegistry _registry;
    private readonly CancellationTokenSource _cts = new();

    public InvaderMoverMediator(InvaderMover invaderMover, 
        InvaderRegistry registry)
    {
        _invaderMover = invaderMover;
        _registry = registry;
    }

    public async override void Initialize()
    {
        _registry.Invaders
            .Subscribe(invaders =>
            {
                if (!invaders.Any())
                {
                    _cts.Cancel();
                    return;
                }

                _invaderMover.SetPositions(invaders.Select(i => i.transform.position).ToArray());
            })
            .AddTo(CompositeDisposable);

        _invaderMover.Moved
            .Subscribe(movement =>
            {
                foreach (var invader in _registry.Invaders.CurrentValue)
                    invader.Move(movement);
            })
            .AddTo(CompositeDisposable);

        try
        {
            await _invaderMover.Move(_cts.Token);
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
}
