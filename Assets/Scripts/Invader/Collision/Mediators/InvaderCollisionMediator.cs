using R3;
using System;
using System.Collections.Generic;

public class InvaderCollisionMediator : CollisionMediator<InvaderEntityView>
{
    private readonly InvaderRegistry _registry;
    private readonly Dictionary<InvaderEntityView, IDisposable> _disposableByInvader = new();

    public InvaderCollisionMediator(InvaderCollisionReporter collisionReporter, InvaderRegistry registry) 
        : base(collisionReporter) => _registry = registry;

    public override void Initialize()
    {
        _registry.Added
            .Subscribe(OnInvaderAdded)
            .AddTo(CompositeDisposable);
        _registry.Removed
            .Subscribe(OnInvaderRemoved)
            .AddTo(CompositeDisposable);
    }

    private void OnInvaderAdded(InvaderEntityView entityView)
    {
        TriggerColliderView triggerColliderView = entityView.ColliderView;
        Subscribe(entityView, triggerColliderView, out IDisposable disposable);
        _disposableByInvader[entityView] = disposable;
    }

    private void OnInvaderRemoved(InvaderEntityView entityView)
    {
        IDisposable disposable = _disposableByInvader[entityView];
        disposable.Dispose();
        _disposableByInvader.Remove(entityView);
    }
}
