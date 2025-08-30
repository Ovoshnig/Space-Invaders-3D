using R3;
using UnityEngine;

public abstract class CollisionMediator<TView> : Mediator where TView : MonoBehaviour
{
    private readonly CollisionReporter<TView> _collisionReporter;

    protected CollisionMediator(CollisionReporter<TView> collisionReporter) => 
        _collisionReporter = collisionReporter;

    protected CompositeDisposable BindCompositeDisposable { get; } = new();

    public override void Dispose()
    {
        base.Dispose();

        BindCompositeDisposable.Dispose();
    }

    protected void Subscribe(TView entityView, TriggerColliderView colliderView)
    {
        colliderView.Collided
            .Subscribe(other => _collisionReporter.Report(entityView, other))
            .AddTo(BindCompositeDisposable);
    }
}
