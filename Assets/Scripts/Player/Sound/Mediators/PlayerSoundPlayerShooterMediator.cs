using R3;

public class PlayerSoundPlayerShooterMediator : Mediator
{
    private readonly PlayerShooter _playerShooter;
    private readonly PlayerSoundPlayerView _playerSoundPlayerView;

    public PlayerSoundPlayerShooterMediator(PlayerShooter playerShooter, 
        PlayerSoundPlayerView playerSoundPlayerView)
    {
        _playerShooter = playerShooter;
        _playerSoundPlayerView = playerSoundPlayerView;
    }

    public override void Initialize()
    {
        _playerShooter.Shot
            .Subscribe(_ => _playerSoundPlayerView.PlayShootSound())
            .AddTo(CompositeDisposable);
    }
}
