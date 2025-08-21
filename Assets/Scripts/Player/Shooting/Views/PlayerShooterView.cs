using R3;
using UnityEngine;
using VContainer;

public class PlayerShooterView : MonoBehaviour
{
    private BulletMoverView _bulletMoverView;

    [Inject]
    public void Construct(PlayerBulletMoverView bulletMoverView) => _bulletMoverView = bulletMoverView;

    public ReadOnlyReactiveProperty<bool> IsBulletEnabled => _bulletMoverView.IsEnabled;

    public void Shoot()
    {
        _bulletMoverView.transform.position = transform.position;
        _bulletMoverView.gameObject.SetActive(true);
    }
}
