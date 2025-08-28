using R3;

public class PlayerShooterMediator : Mediator
{
    private readonly PlayerShooter _playerShooter;
    private readonly PlayerShooterView _playerShooterView;
    private readonly PlayerShooterModel _playerShooterModel;

    public PlayerShooterMediator(PlayerShooter playerShooter,
        PlayerShooterView playerShooterView,
        PlayerShooterModel playerShooterModel)
    {
        _playerShooter = playerShooter;
        _playerShooterView = playerShooterView;
        _playerShooterModel = playerShooterModel;
    }

    public override void Initialize()
    {
        _playerShooter.Shot
            .Subscribe(OnShot)
            .AddTo(CompositeDisposable);

        _playerShooterView.IsBulletEnabled
            .Subscribe(_playerShooter.SetBulletEnabled)
            .AddTo(CompositeDisposable);
    }

    private void OnShot(Unit _)
    {
        _playerShooterModel.Increment();
        _playerShooterView.Shoot();
    }
}
