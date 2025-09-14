using R3;
using UnityEngine;

public class InvaderDestroyerView : CollidedDestroyerView<PlayerBulletMoverView>
{
    [SerializeField] private InvaderExplosionView _explosionViewPrefab;

    private readonly Subject<Unit> _destroyedFromScript = new();
    private readonly Subject<Unit> _destroyedFromEditor = new();

    private bool _wasDestroyedFromScript = false;
    private bool _isApplicationQuitting = false;

    public Observable<Unit> DestroyedFromScript => _destroyedFromScript;
    public Observable<Unit> DestroyedFromEditor => _destroyedFromEditor;
    public Observable<Unit> Destroyed { get; private set; }

    private void Awake() => Destroyed = Observable.Merge(DestroyedFromScript, DestroyedFromEditor);

    private void OnEnable() => _wasDestroyedFromScript = false;

    private void OnDisable()
    {
        if (!_wasDestroyedFromScript && !_isApplicationQuitting)
            _destroyedFromEditor.OnNext(Unit.Default);
    }

    private void OnApplicationQuit() => _isApplicationQuitting = true;

    public override void Destroy(PlayerBulletMoverView playerBulletView)
    {
        playerBulletView.gameObject.SetActive(false);

        Instantiate(_explosionViewPrefab, transform.position, Quaternion.identity);

        _wasDestroyedFromScript = true;
        _destroyedFromScript.OnNext(Unit.Default);
    }
}
