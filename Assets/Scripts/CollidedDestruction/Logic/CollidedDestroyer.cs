using R3;
using UnityEngine;
using VContainer.Unity;

public record CollidedDestructionEvent<TCollidedView, TOtherView>(TCollidedView CollidedView, TOtherView OtherView)
    where TCollidedView : MonoBehaviour
    where TOtherView : MonoBehaviour;

public class CollidedDestroyer<TCollidedView, TOtherView> : IInitializable
    where TCollidedView : MonoBehaviour
    where TOtherView : MonoBehaviour
{
    private readonly CollisionReporter<TCollidedView> _collider;
    private readonly Subject<CollidedDestructionEvent<TCollidedView, TOtherView>> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public CollidedDestroyer(CollisionReporter<TCollidedView> collider) => _collider = collider;

    public Observable<CollidedDestructionEvent<TCollidedView, TOtherView>> Destroyed => _destroyed;

    public void Initialize()
    {
        _collider.Collided
            .Subscribe(OnCollided)
            .AddTo(_compositeDisposable);
    }

    protected virtual void OnCollided(CollidedEvent<TCollidedView> collidedEvent)
    {
        if (collidedEvent.Other.TryGetComponent(out TOtherView otherView))
            _destroyed.OnNext(new CollidedDestructionEvent<TCollidedView, TOtherView>(
                collidedEvent.View, otherView));
    }
}
