using R3;

public class PlayerShooterGamePauserMediator : Mediator
{
    private readonly PlayerShooter _playerShooter;
    private readonly GamePauser _gamePauser;

    public PlayerShooterGamePauserMediator(PlayerShooter playerShooter, GamePauser gamePauser)
    {
        _playerShooter = playerShooter;
        _gamePauser = gamePauser;
    }

    public override void Initialize()
    {
        _gamePauser.IsPause
            .Subscribe(_playerShooter.SetPause)
            .AddTo(CompositeDisposable);
    }
}
