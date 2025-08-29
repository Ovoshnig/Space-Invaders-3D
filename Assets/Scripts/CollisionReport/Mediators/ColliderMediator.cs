using R3;
using UnityEngine;

public abstract class CollisionMediator<TEntityView> : Mediator where TEntityView : MonoBehaviour
{
    private readonly CollisionReporter<TEntityView> _collisionHub;

    protected CollisionMediator(CollisionReporter<TEntityView> collisionHub) => _collisionHub = collisionHub;

    protected CompositeDisposable BindCompositeDisposable { get; } = new();

    public override void Dispose()
    {
        base.Dispose();

        BindCompositeDisposable.Dispose();
    }

    protected void Subscribe(TEntityView entityView, TriggerColliderView colliderView)
    {
        colliderView.Collided
            .Subscribe(other => _collisionHub.Report(entityView, other))
            .AddTo(BindCompositeDisposable);
    }
}
