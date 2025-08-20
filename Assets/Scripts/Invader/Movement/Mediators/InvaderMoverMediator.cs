using Cysharp.Threading.Tasks;
using R3;

public class InvaderMoverMediator : Mediator
{
    private readonly InvaderMover _invaderMover;
    private readonly InvaderRegistry _registry;

    public InvaderMoverMediator(InvaderMover invaderMover, 
        InvaderRegistry registry)
    {
        _invaderMover = invaderMover;
        _registry = registry;
    }

    public override void Initialize()
    {
        foreach (var invader in _registry.Invaders)
            _invaderMover.AddPosition(invader.transform.position);

        _invaderMover.Moved
            .Subscribe(movement =>
            {
                foreach (var invader in _registry.Invaders)
                    invader.Move(movement);
            })
            .AddTo(CompositeDisposable);

        _invaderMover.Move().Forget();
    }
}
