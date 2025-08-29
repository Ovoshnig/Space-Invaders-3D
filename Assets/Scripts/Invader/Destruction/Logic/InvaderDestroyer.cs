using R3;
using UnityEngine;
using VContainer.Unity;

public class InvaderDestroyer : IInitializable
{
    private readonly CollisionReporter<InvaderEntityView> _collider;
    private readonly Subject<InvaderEntityView> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public InvaderDestroyer(CollisionReporter<InvaderEntityView> collider) => _collider = collider;

    public Observable<InvaderEntityView> Destroyed => _destroyed;

    public void Initialize()
    {
        _collider.Collided
            .Subscribe(OnCollided)
            .AddTo(_compositeDisposable);
    }

    private void OnCollided(CollidedEvent<InvaderEntityView> collidedEvent)
    {
        if (collidedEvent.Other.TryGetComponent(out PlayerBulletMoverView bullet))
        {
            InvaderEntityView invader = collidedEvent.View.GetComponent<InvaderEntityView>();
            _destroyed.OnNext(invader);

            bullet.gameObject.SetActive(false);
            Object.Destroy(invader.gameObject);
        }
    }
}
