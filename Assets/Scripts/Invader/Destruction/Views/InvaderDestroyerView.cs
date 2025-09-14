using R3;
using UnityEngine;

public class InvaderDestroyerView : CollidedDestroyerView<PlayerBulletMoverView>
{
    [SerializeField] private InvaderExplosionView _explosionViewPrefab;

    private readonly Subject<Unit> _destroyedFromEditor = new();

    private bool _wasDestroyedFromScript = false;
    private bool _wasApplicationQuit = false;

    public Observable<Unit> DestroyedFromEditor => _destroyedFromEditor;

    public override void Destroy(PlayerBulletMoverView playerBulletView)
    {
        playerBulletView.gameObject.SetActive(false);

        Instantiate(_explosionViewPrefab, transform.position, Quaternion.identity);

        _wasDestroyedFromScript = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!_wasDestroyedFromScript && !_wasApplicationQuit)
            _destroyedFromEditor.OnNext(Unit.Default);
    }

    private void OnApplicationQuit() => _wasApplicationQuit = true;
}
