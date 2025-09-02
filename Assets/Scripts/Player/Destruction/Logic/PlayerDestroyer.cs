using Cysharp.Threading.Tasks;
using R3;

public class PlayerDestroyer : CollidedDestroyer<PlayerMoverView, InvaderBulletMoverView>
{
    private readonly PlayerSettings _playerSettings;
    private readonly Subject<CollidedDestructionEvent<PlayerMoverView, InvaderBulletMoverView>> _startDestroying = new();

    public PlayerDestroyer(CollisionReporter<PlayerMoverView> collider, PlayerSettings playerSettings)
        : base(collider) => _playerSettings = playerSettings;

    public Observable<CollidedDestructionEvent<PlayerMoverView, InvaderBulletMoverView>> StartDestroying => _startDestroying;

    protected async override void OnCollided(CollidedEvent<PlayerMoverView> collidedEvent)
    {
        if (collidedEvent.Other.TryGetComponent(out InvaderBulletMoverView invaderBulletView))
            _startDestroying.OnNext(new CollidedDestructionEvent<PlayerMoverView, InvaderBulletMoverView>(
                collidedEvent.View, invaderBulletView));

        await UniTask.WaitForSeconds(_playerSettings.ExplosionDuration);

        base.OnCollided(collidedEvent);
    }
}
