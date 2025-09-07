using R3;

public class PlayerSoundPlayerMediator : Mediator
{
    private readonly PlayerSoundPlayer _playerSoundPlayer;
    private readonly PlayerSoundPlayerView _playerSoundPlayerView;

    public PlayerSoundPlayerMediator(PlayerSoundPlayer playerSoundPlayer,
        PlayerSoundPlayerView playerSoundPlayerView)
    {
        _playerSoundPlayer = playerSoundPlayer;
        _playerSoundPlayerView = playerSoundPlayerView;
    }

    public override void Initialize()
    {
        _playerSoundPlayer.Playing
            .Subscribe(_playerSoundPlayerView.PlayOneShot)
            .AddTo(CompositeDisposable);
    }
}
