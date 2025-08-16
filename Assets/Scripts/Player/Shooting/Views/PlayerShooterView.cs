using UnityEngine;
using VContainer;

public class PlayerShooterView : MonoBehaviour
{
    private IObjectResolver _container;

    [Inject]
    public void Construct(IObjectResolver container) => _container = container;

    public void Shoot()
    {
        BulletMoverView bulletMoverView = _container.Resolve<BulletMoverView>();
        bulletMoverView.transform.position = transform.position;
    }
}
