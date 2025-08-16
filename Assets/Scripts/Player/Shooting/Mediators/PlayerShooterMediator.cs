using R3;

public class PlayerShooterMediator : Mediator
{
    private readonly PlayerShooter _playerShooter;
    private readonly PlayerShooterView _playerShooterView;

    public PlayerShooterMediator(PlayerShooter playerShooter, 
        PlayerShooterView playerShooterView)
    {
        _playerShooter = playerShooter;
        _playerShooterView = playerShooterView;
    }

    public override void Initialize()
    {
        _playerShooter.Shot
            .Subscribe(_ => _playerShooterView.Shoot())
            .AddTo(CompositeDisposable);
    }
}
