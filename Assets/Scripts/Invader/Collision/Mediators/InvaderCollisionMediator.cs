using R3;
using System.Collections.Generic;
using System.Linq;

public class InvaderCollisionMediator : CollisionMediator<InvaderEntityView>
{
    private readonly InvaderRegistry _registry;

    public InvaderCollisionMediator(CollisionReporter<InvaderEntityView> collisionHub,
        InvaderRegistry registry) : base(collisionHub) => _registry = registry;

    public override void Initialize()
    {
        _registry.EntityViews
            .Subscribe(OnInvadersChange)
            .AddTo(CompositeDisposable);
    }

    private void OnInvadersChange(IReadOnlyList<InvaderEntityView> invaders)
    {
        BindCompositeDisposable.Clear();

        if (!invaders.Any())
            return;

        foreach (var invader in invaders)
        {
            TriggerColliderView colliderView = invader.Get<TriggerColliderView>();
            Subscribe(invader, colliderView);
        }
    }
}
