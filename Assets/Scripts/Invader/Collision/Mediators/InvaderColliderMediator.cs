using R3;
using System.Collections.Generic;
using System.Linq;

public class InvaderColliderMediator : Mediator
{
    private readonly InvaderCollider _invaderCollider;
    private readonly InvaderRegistry _registry;
    private readonly CompositeDisposable _compositeDisposable = new();

    public InvaderColliderMediator(InvaderCollider invaderCollider,
        InvaderRegistry registry)
    {
        _invaderCollider = invaderCollider;
        _registry = registry;
    }

    public override void Initialize()
    {
        _registry.EntityViews
            .Subscribe(OnInvadersChange)
            .AddTo(CompositeDisposable);
    }

    public override void Dispose()
    {
        base.Dispose();

        _compositeDisposable.Dispose();
    }

    private void OnInvadersChange(IReadOnlyList<InvaderEntityView> invaders)
    {
        _compositeDisposable.Clear();

        if (!invaders.Any())
            return;

        foreach (var invader in invaders)
        {
            InvaderColliderView invaderColliderView = invader.Get<InvaderColliderView>();
            invaderColliderView.Collided
                .Subscribe(other => _invaderCollider.Collide(invaderColliderView, other))
                .AddTo(_compositeDisposable);
        }
    }
}
