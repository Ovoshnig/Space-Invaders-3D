using R3;
using UnityEngine;
using VContainer.Unity;

public class InvaderDestroyer : IInitializable
{
    private readonly InvaderCollider _collider;
    private readonly Subject<InvaderEntityView> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public InvaderDestroyer(InvaderCollider collider) => _collider = collider;

    public Observable<InvaderEntityView> Destroyed => _destroyed;

    public void Initialize()
    {
        _collider.Collided
            .Subscribe(OnCollided)
            .AddTo(_compositeDisposable);
    }

    private void OnCollided(InvaderCollidedEvent collidedEvent)
    {
        if (collidedEvent.Other.TryGetComponent(out PlayerBulletMoverView bullet))
        {
            InvaderEntityView invader = collidedEvent.InvaderColliderView.GetComponent<InvaderEntityView>();
            _destroyed.OnNext(invader);

            bullet.gameObject.SetActive(false);
            Object.Destroy(invader.gameObject);
        }
    }
}
