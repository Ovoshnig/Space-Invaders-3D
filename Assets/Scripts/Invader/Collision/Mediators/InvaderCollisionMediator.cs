using R3;

public class InvaderCollisionMediator : CollisionMediator<InvaderEntityView>
{
    private readonly InvaderRegistry _registry;

    public InvaderCollisionMediator(CollisionReporter<InvaderEntityView> collisionHub,
        InvaderRegistry registry) : base(collisionHub) => _registry = registry;

    public override void Initialize()
    {
        _registry.Changed
            .Subscribe(OnInvadersChange)
            .AddTo(CompositeDisposable);
    }

    private void OnInvadersChange(InvaderEntityView _)
    {
        BindCompositeDisposable.Clear();

        if (!_registry.Any.CurrentValue)
            return;

        foreach (var invader in _registry.InvaderEntityViews)
        {
            TriggerColliderView colliderView = invader.Get<TriggerColliderView>();
            Subscribe(invader, colliderView);
        }
    }
}
