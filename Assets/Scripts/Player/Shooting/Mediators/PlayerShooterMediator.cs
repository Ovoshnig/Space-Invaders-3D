using R3;

public class PlayerShooterMediator : Mediator
{
    private readonly PlayerShooter _playerShooter;
    private readonly PlayerShooterView _playerShooterView;
    private readonly PlayerShooterData _playerShooterData;

    public PlayerShooterMediator(PlayerShooter playerShooter,
        PlayerShooterView playerShooterView,
        PlayerShooterData playerShooterData)
    {
        _playerShooter = playerShooter;
        _playerShooterView = playerShooterView;
        _playerShooterData = playerShooterData;
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
        _playerShooterData.Increment();
        _playerShooterView.Shoot();
    }
}
