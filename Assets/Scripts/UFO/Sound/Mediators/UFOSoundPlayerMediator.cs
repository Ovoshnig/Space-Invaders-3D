using R3;

public class UFOSoundPlayerMediator : Mediator
{
    private readonly UFOSoundPlayer _ufoSoundPlayer;
    private readonly UFOSoundPlayerView _ufoSoundPlayerView;

    public UFOSoundPlayerMediator(UFOSoundPlayer ufoSoundPlayer,
        UFOSoundPlayerView ufoSoundPlayerView)
    {
        _ufoSoundPlayer = ufoSoundPlayer;
        _ufoSoundPlayerView = ufoSoundPlayerView;
    }

    public override void Initialize()
    {
        _ufoSoundPlayer.Playing
            .Subscribe(_ufoSoundPlayerView.PlayOneShot)
            .AddTo(CompositeDisposable);
    }
}
