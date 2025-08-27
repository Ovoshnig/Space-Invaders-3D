using R3;
using UnityEngine;

public readonly struct InvaderCollidedEvent
{
    public InvaderCollidedEvent(InvaderColliderView invaderColliderView,
        Collider other)
    {
        InvaderColliderView = invaderColliderView;
        Other = other;
    }

    public InvaderColliderView InvaderColliderView { get; }
    public Collider Other { get; }
}

public class InvaderCollider
{
    private readonly Subject<InvaderCollidedEvent> _collided = new();

    public Observable<InvaderCollidedEvent> Collided => _collided;

    public void Collide(InvaderColliderView invaderColliderView, Collider other) =>
        _collided.OnNext(new InvaderCollidedEvent(invaderColliderView, other));
}
