using UnityEngine;

public class InvaderDestroyerView : CollidedDestroyerView<PlayerBulletMoverView>
{
    [SerializeField] private InvaderExplosionView _explosionView;

    public override void Destroy(PlayerBulletMoverView playerBulletView)
    {
        Instantiate(_explosionView, transform.position, Quaternion.identity);

        playerBulletView.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
