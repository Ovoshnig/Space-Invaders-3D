using R3;
using UnityEngine;

public readonly struct CollidedEvent<TView> where TView : MonoBehaviour
{
    public CollidedEvent(TView view, Collider other)
    {
        View = view;
        Other = other;
    }

    public TView View { get; }
    public Collider Other { get; }
}

public class CollisionReporter<TView> where TView : MonoBehaviour
{
    private readonly Subject<CollidedEvent<TView>> _collided = new();

    public Observable<CollidedEvent<TView>> Collided => _collided;

    public void Report(TView invaderColliderView, Collider other) =>
        _collided.OnNext(new CollidedEvent<TView>(invaderColliderView, other));
}
