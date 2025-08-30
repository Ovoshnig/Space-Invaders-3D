using R3;
using UnityEngine;
using VContainer.Unity;

public readonly struct CollidedDestructionEvent<TCollidedView, TOtherView>
    where TCollidedView : MonoBehaviour
    where TOtherView : MonoBehaviour
{
    public CollidedDestructionEvent(TCollidedView collidedView, TOtherView otherView)
    {
        CollidedView = collidedView;
        OtherView = otherView;
    }

    public TCollidedView CollidedView { get; }
    public TOtherView OtherView { get; }
}

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

    private void OnCollided(CollidedEvent<TCollidedView> collidedEvent)
    {
        if (collidedEvent.Other.TryGetComponent(out TOtherView otherView))
            _destroyed.OnNext(new CollidedDestructionEvent<TCollidedView, TOtherView>(
                collidedEvent.View,
                otherView));
    }
}
