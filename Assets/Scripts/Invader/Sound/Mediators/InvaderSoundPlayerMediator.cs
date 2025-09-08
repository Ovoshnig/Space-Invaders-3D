using R3;

public class InvaderSoundPlayerMediator : Mediator
{
    private readonly InvaderSoundPlayer _invaderSoundPlayer;
    private readonly InvaderSoundPlayerView _invaderSoundPlayerView;

    public InvaderSoundPlayerMediator(InvaderSoundPlayer invaderSoundPlayer,
        InvaderSoundPlayerView invaderSoundPlayerView)
    {
        _invaderSoundPlayer = invaderSoundPlayer;
        _invaderSoundPlayerView = invaderSoundPlayerView;
    }

    public override void Initialize()
    {
        _invaderSoundPlayer.Playing
            .Subscribe(_invaderSoundPlayerView.PlayOneShot)
            .AddTo(CompositeDisposable);
    }
}
